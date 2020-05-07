using System;
using System.Collections.Generic;
using System.Linq;

namespace Cucumber.Ast
{
    public class CucumberExpressionAstNode
    {
        public RuleType RuleType { get; }
        public CucumberExpressionAstNode ParentNode { get; }
        public List<Token> Tokens = new List<Token>();
        public List<CucumberExpressionAstNode> SubNodes = new List<CucumberExpressionAstNode>();

        public CucumberExpressionAstNode(RuleType ruleType, CucumberExpressionAstNode parentNode)
        {
            RuleType = ruleType;
            ParentNode = parentNode;
        }

        public string ToString(string indent)
        {
            var tokenText = string.Join("", Tokens.Select(t => t.Text));
            return $"{indent}<{RuleType}>: [{tokenText}]{string.Join("", SubNodes.Select(sn => Environment.NewLine + indent + sn.ToString(indent + "  ")))}";
        }

        public override string ToString()
        {
            return ToString("");
        }
    }
}
