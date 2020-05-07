using System;

namespace Cucumber
{
    public class UnexpectedTokenException : ParserException
    {
        public string StateComment { get; private set; }

        public Token ReceivedToken { get; private set; }
        public string[] ExpectedTokenTypes { get; private set; }

        public UnexpectedTokenException(Token receivedToken, string[] expectedTokenTypes, string stateComment)
            : base(GetMessage(receivedToken, expectedTokenTypes))
        {
            ReceivedToken = receivedToken ?? throw new ArgumentNullException("receivedToken");
            ExpectedTokenTypes = expectedTokenTypes ?? throw new ArgumentNullException("expectedTokenTypes");
            StateComment = stateComment;
        }

        private static string GetMessage(Token receivedToken, string[] expectedTokenTypes)
        {
            if (receivedToken == null) throw new ArgumentNullException("receivedToken");
            if (expectedTokenTypes == null) throw new ArgumentNullException("expectedTokenTypes");

            return $"expected: {string.Join(", ", expectedTokenTypes)}, got '{receivedToken.Text}'";
        }
    }
}
