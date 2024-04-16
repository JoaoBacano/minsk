namespace Minsk.CodeAnalysis
{
    public sealed class LiteralEmpressionSyntax : ExpressionSyntax
    {
        public LiteralEmpressionSyntax(SyntaxToken literalToken)
        {
            LiteralToken = literalToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;
        public SyntaxToken LiteralToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return LiteralToken;
        }
    }
}