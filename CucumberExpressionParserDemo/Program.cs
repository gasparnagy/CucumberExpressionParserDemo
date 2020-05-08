using System;
using System.Linq;
using Cucumber;
using Cucumber.Ast;

namespace CucumberExpressionParserDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string expressionString = args[0];

            var parser = new CucumberExpressionsParser<CucumberExpression>();
            try
            {
                var expression = parser.Parse(new TokenScanner(expressionString));
                Console.WriteLine(expression);
            }
            catch (ParserException ex)
            {
                Console.WriteLine("ERROR:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
