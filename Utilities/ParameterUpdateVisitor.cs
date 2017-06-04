using System.Linq.Expressions;

namespace Utilities
{
    public static partial class ExpressionCombiner
    {
        private class ParameterUpdateVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly ParameterExpression _newParameter;

            public ParameterUpdateVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter;
                _newParameter = newParameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return ReferenceEquals(node, _oldParameter) ? _newParameter : base.VisitParameter(node);
            }
        }
    }
}