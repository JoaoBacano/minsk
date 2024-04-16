namespace Minsk.CodeAnalysis.Syntax
{
    public sealed class ParenthesizedExpressionSyntax : ExpressionSyntax
    {

        public ParenthesizedExpressionSyntax(SyntaxToken openParenthesisToken, ExpressionSyntax expression, SyntaxToken closeParenthesisToken)
        {
            OpenParenthesisToken = openParenthesisToken;
            Expression = expression;
            CloseParenthesisToken = closeParenthesisToken;
        }
        public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;
        public SyntaxToken CloseParenthesisToken { get; }
        public SyntaxToken OpenParenthesisToken { get; }
        public ExpressionSyntax Expression { get; }



        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return OpenParenthesisToken;
            yield return Expression;
            yield return CloseParenthesisToken;
        }
    }
}