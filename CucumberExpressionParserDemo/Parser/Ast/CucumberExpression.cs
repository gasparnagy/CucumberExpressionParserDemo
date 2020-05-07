namespace Cucumber.Ast
{
    public class CucumberExpression
    {
        public CucumberExpression(CucumberExpressionAstNode node)
        {
            Node = node;
        }

        public CucumberExpressionAstNode Node { get; }

        public override string ToString()
        {
            return Node?.ToString();
        }
    }
}