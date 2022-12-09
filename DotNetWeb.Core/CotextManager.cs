using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetWeb.Core
{
    public static class ContextManager
    {
        //Cada que encuentro llaves, encuentro un contexto
        private static readonly List<Context> _contexts = new List<Context>();
        private static readonly List<Context> _contextsForCodeGeneration = new();

        //Crear un nuevo contexto
        public static Context Push()
        {
            var context = new Context();
            _contexts.Add(context);
            //Lo agrego pero nunca lo voy a borrar, porque lo ocupo para la generación de código
            _contextsForCodeGeneration.Add(context);
            return context;
        }

        //Una vez se termine el contexto hay que quitarlo.
        public static Context Pop()
        {
            var lastContext = _contexts.Last();
            _contexts.Remove(lastContext);
            return lastContext;
        }
        //Pone simbolos en el último contexto
        public static void Put(string lexeme, IdExpression id) => _contexts.Last().Put(lexeme, id);
        public static void UpdateSymbol(string lexeme, dynamic value)
        {
            for (var i = _contextsForCodeGeneration.Count - 1; i >= 0; i--)
            {
                var symbol = _contextsForCodeGeneration.ElementAt(i).Get(lexeme);
                if (symbol != null)
                {
                    _contextsForCodeGeneration[i].UpdateSymbolValue(lexeme, value);
                }
            }
        }
        public static Symbol Get(string lexeme)
        {
            //ir revisando los contextos, para ver si debe aceptar el símbolo o no
            //Empezamos del último hacia arriba
            for (var i = _contexts.Count - 1; i >= 0; i--)
            {
                var symbol = _contexts.ElementAt(i).Get(lexeme);
                if (symbol != null)
                {
                    return symbol;
                }
            }

            throw new ApplicationException($"Symbol {lexeme} was not found in current context");
        }

        //Como esta es la clase que sabe todos los contextos, aquí hacemos un método para obtener los símbolos del
        //contexto actual
        public static Symbol GetSymbolForInterpretation(string lexeme)
        {
            for (var i = _contextsForCodeGeneration.Count - 1; i >= 0; i--)
            {
                var symbol = _contextsForCodeGeneration.ElementAt(i).Get(lexeme);
                if (symbol != null)
                {
                    return symbol;
                }
            }
            throw new ApplicationException($"Symbol {lexeme} was not found in current context");
        }
        public static IEnumerable<Symbol> GetSymbolsForCurrentContext()
        {
            if (!_contextsForCodeGeneration.Any())
            {
                return Enumerable.Empty<Symbol>();
            }

            var last = _contextsForCodeGeneration.First();
            var symbols = last.GetSymbolsForCurrentContext();
            _contextsForCodeGeneration.Remove(last);
            return symbols;
        }
    }
    //CLASE CONTEXT
    public class Context
    {
        //Se usa un diccionario para definir la tabla de símbolos
        private readonly Dictionary<string, Symbol> _symbolTable;
        public Context()
        {
            _symbolTable = new Dictionary<string, Symbol>();
        }

        //Tenemos que permitir que se agreguen datos y se obtengan datos de esta tabla
        public void Put(string lexeme, IdExpression id)
        {
            if (_symbolTable.ContainsKey(lexeme))
            {
                throw new ApplicationException(
                    $"A local symbol named '{lexeme}' cannot be declared in this scope because a symbol with the same already exists");
            }

            if (id.Type is ArrayType arrayType)
            {
                _symbolTable.Add(lexeme, new Symbol(id, new dynamic[arrayType.Size]));
            }
            else
            {
                _symbolTable.Add(lexeme, new Symbol(id));
            }
        }

        public Symbol Get(string lexeme)
        {
            return _symbolTable.TryGetValue(lexeme, out var value) ? value : null;
        }

        public IEnumerable<Symbol> GetSymbolsForCurrentContext() =>
            _symbolTable.Select(x => x.Value);

        public void UpdateSymbolValue(string lexeme, dynamic value)
        {
            var symbol = Get(lexeme);
            symbol.Value = value;
            _symbolTable[lexeme] = symbol;
        }
    }
}
