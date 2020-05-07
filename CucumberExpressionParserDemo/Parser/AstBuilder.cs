using Cucumber.Ast;

namespace Cucumber
{
    public class AstBuilder : IAstBuilder
    {
        private CucumberExpressionAstNode _rootNode;
        private CucumberExpressionAstNode _node;

        public void Build(Token token)
        {
            if (!token.IsEOF)
                _node.Tokens.Add(token);
        }

        public void StartRule(RuleType ruleType)
        {
            CucumberExpressionAstNode newNode = new CucumberExpressionAstNode(ruleType, _node);
            if (_rootNode == null)
            {
                _rootNode = newNode;
            }
            else
            {
                _node.SubNodes.Add(newNode);
            }

            _node = newNode;
        }

        public void EndRule(RuleType ruleType)
        {
            _node = _node.ParentNode;
        }

        public CucumberExpression GetResult()
        {
            return new CucumberExpression(_rootNode);
        }
    }
}