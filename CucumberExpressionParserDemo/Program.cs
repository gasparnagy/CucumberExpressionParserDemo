using System;
using Cucumber;

namespace CucumberExpressionParserDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string expressionString = args[0];

            var parser = new CucumberExpressionsParser();
            var expression = parser.Parse(new TokenScanner(expressionString));

            Console.WriteLine(expression);
        }
    }
}
