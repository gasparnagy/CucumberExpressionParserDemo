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
            ReceivedToken = receivedToken ?? throw new ArgumentNullException(nameof(receivedToken));
            ExpectedTokenTypes = expectedTokenTypes ?? throw new ArgumentNullException(nameof(expectedTokenTypes));
            StateComment = stateComment;
        }

        private static string GetMessage(Token receivedToken, string[] expectedTokenTypes)
        {
            if (receivedToken == null) throw new ArgumentNullException(nameof(receivedToken));
            if (expectedTokenTypes == null) throw new ArgumentNullException(nameof(expectedTokenTypes));

            return $"expected: {string.Join(", ", expectedTokenTypes)}, got '{receivedToken.Text}'" 
                   + GetPositionText(receivedToken);
        }

        private static string GetPositionText(Token token)
        {
            return
                Environment.NewLine +
                token.SourceLine +
                Environment.NewLine +
                new string(' ', token.StartPosition) + "^";
        }
    }
}
