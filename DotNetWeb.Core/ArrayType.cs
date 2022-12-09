using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetWeb.Core
{
    public class ArrayType : ExpressionType
    {
        public ExpressionType Of { get; }
        public int Size { get; }

        public ArrayType(string lexeme, TokenType tokenType, ExpressionType of, int size) : base(lexeme, tokenType)
        {
            Of = of;
            Size = size;
        }
    }
}
