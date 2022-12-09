using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetWeb.Core
{
    public class SequenceStatement:Statement
    {
        public Statement Current { get; set; }
        public Statement Next { get; set; }

        public SequenceStatement(Statement current, Statement next)
        {
            Current = current;
            Next = next;
        }

     

        //Tiene dos statements, entonces genero el código de cada uno de los statements
        public override string GenerateCode() => $"{Current?.GenerateCode()} {Environment.NewLine} {Next?.GenerateCode()}";
        public override void Interpret()
        {
            this.Current?.Interpret();
            this.Next?.Interpret();
        }
    }
}
