using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetWeb.Core
{
    public class ExpressionType : IEquatable<ExpressionType>
    {
        public string Lexeme { get; private set; }
        public TokenType TokenType { get; set; }

        public ExpressionType(string lexeme, TokenType tokenType)
        {
            Lexeme = lexeme;
            TokenType = tokenType;
        }

        public static ExpressionType Int => new ExpressionType("int", DotNetWeb.Core.TokenType.BasicType);
        public static ExpressionType Float => new ExpressionType("float", DotNetWeb.Core.TokenType.BasicType);
        public static ExpressionType String => new ExpressionType("string", DotNetWeb.Core.TokenType.BasicType);
        public static ExpressionType Bool => new ExpressionType("bool", DotNetWeb.Core.TokenType.BasicType);

        public bool Equals(ExpressionType? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return false;
            }

            return Lexeme == other.Lexeme && TokenType == other.TokenType;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return false;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((Type)obj);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Lexeme, TokenType).GetHashCode();
        }

        public static bool operator ==(ExpressionType a, ExpressionType b) => a.Equals(b);
        public static bool operator !=(ExpressionType a, ExpressionType b) => !a.Equals(b);

        public override string ToString()
        {
            return Lexeme;
        }
    }
}
