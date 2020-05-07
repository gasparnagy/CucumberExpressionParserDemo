using System.Linq;
using Cucumber.Ast;

namespace Cucumber
{
    public class AstBuilder : IAstBuilder
    {
        private CucumberExpressionAstNode _rootNode;
        private CucumberExpressionAstNode _node;

        private static readonly TokenType[] PreservedTokenTypes = {TokenType.Word, TokenType.Separator};

        public void Build(Token token)
        {
            if (PreservedTokenTypes.Contains(token.TokenType))
                _node.Tokens.Add(token);
        }

        public void StartRule(RuleType ruleType)
        {
            if (ruleType == RuleType.Text && _node?.SubNodes.LastOrDefault()?.RuleType == RuleType.Text)
            {
                _node = _node.SubNodes.Last();
                return;
            }

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