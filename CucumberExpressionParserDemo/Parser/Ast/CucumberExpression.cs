namespace Cucumber.Ast
{
    public class CucumberExpression : CucumberExpressionAstNode
    {
        public CucumberExpression(CucumberExpressionAstNode node): base(RuleType.CucumberExpression, null)
        {
            SubNodes.AddRange(node.SubNodes);
        }
    }
}