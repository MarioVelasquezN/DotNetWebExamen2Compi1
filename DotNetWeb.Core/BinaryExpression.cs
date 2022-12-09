using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetWeb.Core
{
    public abstract class BinaryExpression : Expression
    {
        public Expression LeftExpression { get; set; }
        public Expression RightExpression { get; set; }

        public BinaryExpression(Expression leftExpression, Expression rightExpression)
        {
            LeftExpression = leftExpression;
            RightExpression = rightExpression;
        }
    }
}
