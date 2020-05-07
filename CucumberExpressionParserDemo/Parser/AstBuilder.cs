using System.Linq;
using Cucumber.Ast;

namespace Cucumber
{
    public class AstBuilder : IAstBuilder
    {
        private CucumberExpressionAstNode _node;

        private static readonly TokenType[] PreservedTokenTypes = {TokenType.Word, TokenType.Separator};

        public void Build(Token token)
        {
            if (PreservedTokenTypes.Contains(token.TokenType))
                _node.Tokens.Add(token);
        }

        public void StartRule(RuleType ruleType)
        {
            if (ruleType == RuleType.Separator)
                ruleType = RuleType.Text;

            _node = new CucumberExpressionAstNode(ruleType, _node);
        }

        public void EndRule(RuleType ruleType)
        {
            if (ruleType == RuleType.CucumberExpression)
                return; // keep last node in _node

            if (ruleType == RuleType.Alternation && _node.SubNodes.Count == 1)
            {
                foreach (var subNode in _node.SubNodes.Single().SubNodes)
                {
                    AddToParent(subNode, _node.ParentNode);
                }
            }
            else
            {
                AddToParent(_node, _node.ParentNode);
            }

            _node = _node.ParentNode;
        }

        private void AddToParent(CucumberExpressionAstNode node, CucumberExpressionAstNode parentNode)
        {
            if (node.RuleType == RuleType.Text && parentNode.SubNodes.LastOrDefault()?.RuleType == RuleType.Text)
            {
                parentNode.SubNodes.Last().Tokens.AddRange(node.Tokens);
                return;
            }

            parentNode.SubNodes.Add(node);
        }

        public CucumberExpression GetResult()
        {
            return new CucumberExpression(_node);
        }
    }
}