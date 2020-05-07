using System;

namespace Cucumber
{
    public class UnexpectedEOFException : ParserException
    {
        public string StateComment { get; private set; }
        public string[] ExpectedTokenTypes { get; private set; }
        public UnexpectedEOFException(Token receivedToken, string[] expectedTokenTypes, string stateComment)
            : base(GetMessage(expectedTokenTypes))
        {
            ExpectedTokenTypes = expectedTokenTypes ?? throw new ArgumentNullException("expectedTokenTypes");
            StateComment = stateComment;
        }

        private static string GetMessage(string[] expectedTokenTypes)
        {
            if (expectedTokenTypes == null) throw new ArgumentNullException("expectedTokenTypes");

            return $"unexpected end of file, expected: {string.Join(", ", expectedTokenTypes)}";
        }
    }
}
