using System;

namespace Cucumber
{
    public class UnexpectedEOFException : ParserException
    {
        public string StateComment { get; private set; }
        public string[] ExpectedTokenTypes { get; private set; }
        public UnexpectedEOFException(Token receivedToken, string[] expectedTokenTypes, string stateComment)
            : base(GetMessage(receivedToken, expectedTokenTypes))
        {
            ExpectedTokenTypes = expectedTokenTypes ?? throw new ArgumentNullException(nameof(expectedTokenTypes));
            StateComment = stateComment;
        }

        private static string GetMessage(Token receivedToken, string[] expectedTokenTypes)
        {
            if (expectedTokenTypes == null) throw new ArgumentNullException(nameof(expectedTokenTypes));

            return $"unexpected end of expression, expected: {UnexpectedTokenException.GetTokenList(expectedTokenTypes)}" +
                UnexpectedTokenException.GetPositionText(receivedToken);
        }
    }
}
