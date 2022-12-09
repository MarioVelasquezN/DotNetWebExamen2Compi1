using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetWeb.Core
{
    public class EqualExpression : BinaryExpression
    {
        private readonly Dictionary<(ExpressionType, ExpressionType), ExpressionType> _typeRules;
        private readonly Token _operation;

        public EqualExpression(Expression leftExpression, Expression rightExpression, Token operation) :
            base(leftExpression, rightExpression)
        {
            _operation = operation;
            _typeRules = new Dictionary<(ExpressionType, ExpressionType), ExpressionType>
            {
                { (ExpressionType.Int, ExpressionType.Int), ExpressionType.Bool },
                { (ExpressionType.Float, ExpressionType.Float), ExpressionType.Bool },
                { (ExpressionType.Int, ExpressionType.Float), ExpressionType.Bool },
                { (ExpressionType.Float, ExpressionType.Int), ExpressionType.Bool },
                { (ExpressionType.String, ExpressionType.String), ExpressionType.Bool },
       


            };
        }

        public override ExpressionType GetType()
        {
            var leftType = LeftExpression.GetType();
            var rightType = RightExpression.GetType();
            if (_typeRules.TryGetValue((leftType, rightType), out var resultType))
            {
                return resultType;
            }
            throw new ApplicationException($"Cannot apply operators '=' to operands of type {leftType} and {rightType} ");
        }

        public override string GenerateCode() => $"{LeftExpression?.GenerateCode()} {_operation.Lexeme} {RightExpression.GenerateCode()}";

        public override dynamic Evaluate()
        {
            if (_operation.TokenType == TokenType.Equal)
            {
                return LeftExpression.Evaluate() == RightExpression.Evaluate();
            }

            return LeftExpression.Evaluate() != RightExpression.Evaluate();
        }
    }
}
