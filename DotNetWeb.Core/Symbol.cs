using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetWeb.Core
{
    public class Symbol
    {
        public IdExpression Id { get; set; }

        public dynamic Value { get; set; }

        public Symbol(IdExpression id)
        {
            Id = id;
        }

        public Symbol(IdExpression id, dynamic value)
        {
            Id = id;
            Value = value;
        }
    }
}
