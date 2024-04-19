using Minsk.CodeAnalysis.Syntax;
using System;
using Minsk.CodeAnalysis.Binding;

namespace Minsk.CodeAnalysis
{
    public sealed class Compilation()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Compilation"/> class with the specified syntax tree.
        /// </summary>
        /// <param name="syntax">The syntax tree to compile.</param>
        public Compilation(SyntaxTree syntax) : this()
        {
            Syntax = syntax;
        }

        /// <summary>
        /// Gets the syntax tree associated with this compilation.
        /// </summary>
        public SyntaxTree Syntax { get; }

        /// <summary>
        /// Evaluates the compiled syntax tree and returns the result.
        /// </summary>
        /// <returns>The evaluation result.</returns>
        public EvaluationResult Evaluate()
        {
            var binder = new Binder();
            var boundExpression = binder.BindExpression(Syntax.Root);

            var diagnostics = Syntax.Diagnostics.Concat(binder.Diagnostics).ToArray();
            if (diagnostics.Any())
                return new EvaluationResult(diagnostics, null);

            var evaluator = new Evaluator(boundExpression);
            var value = evaluator.Evaluate();
            return new EvaluationResult(Array.Empty<Diagnostic>(), value);
        }
    }
}