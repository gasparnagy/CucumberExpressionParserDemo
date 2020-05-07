using System;
using System.Collections.Generic;
using System.Linq;

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

        private static readonly Dictionary<TokenType, string> ExpectedTokenTypeDisplayTexts = new Dictionary<TokenType, string>
        {
            { TokenType.LCurl, "{" },
            { TokenType.RCurl, "}" },
            { TokenType.LParen, "(" },
            { TokenType.RParen, ")" },
            { TokenType.Slash, "/" },
        };

        private static string GetMessage(Token receivedToken, string[] expectedTokenTypes)
        {
            if (receivedToken == null) throw new ArgumentNullException(nameof(receivedToken));
            if (expectedTokenTypes == null) throw new ArgumentNullException(nameof(expectedTokenTypes));

            return $"expected: {GetTokenList(expectedTokenTypes)}, got '{receivedToken.Text}'" 
                   + GetPositionText(receivedToken);
        }

        internal static string GetTokenList(string[] expectedTokenTypes)
        {
            return string.Join(", ", expectedTokenTypes.Select(GetTokenTypeDisplayText));
        }

        private static string GetTokenTypeDisplayText(string tokenTypeName)
        {
            var tokenType = Enum.Parse<TokenType>(tokenTypeName.TrimStart('#'));
            return ExpectedTokenTypeDisplayTexts.TryGetValue(tokenType, out var text) ? $"'{text}'" : tokenTypeName;
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
