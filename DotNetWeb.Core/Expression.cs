using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetWeb.Core
{
    public abstract class Expression
    {
        public abstract ExpressionType GetType();
        public abstract string GenerateCode();
        
        public abstract dynamic Evaluate();
    }
}
