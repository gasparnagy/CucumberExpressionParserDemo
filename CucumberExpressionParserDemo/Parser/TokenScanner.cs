using System;
using System.Collections.Generic;
using System.Text;

namespace Cucumber
{
    public class TokenScanner : ITokenScanner
    {
        private readonly IEnumerator<Token> _scanner;

        public TokenScanner(string expressionString)
        {
            _scanner = Scan(expressionString);
        }

        public IEnumerator<Token> Scan(string expressionString)
        {
            var tokenType = TokenType.None;
            var tokenText = new StringBuilder(expressionString.Length);

            bool treatNextAsText = false;
            foreach (var c in expressionString)
            {
                if (!treatNextAsText && c == '\\')
                {
                    treatNextAsText = true;
                    continue;
                }

                var type = GetTokenType(c, treatNextAsText);
                if (type != tokenType)
                {
                    if (tokenType != TokenType.None)
                        yield return new Token(tokenType, tokenText.ToString());
                    tokenType = type;
                    tokenText.Clear();
                }

                tokenText.Append(c);
            }
            if (tokenType != TokenType.None)
                yield return new Token(tokenType, tokenText.ToString());
            yield return new Token(TokenType.EOF, string.Empty);
        }

        private TokenType GetTokenType(in char c, in bool treatAsText)
        {
            if (!treatAsText)
            {
                switch (c)
                {
                    case '/':
                        return TokenType.Slash;
                    case '(':
                        return TokenType.LParen;
                    case ')':
                        return TokenType.RParen;
                    case '{':
                        return TokenType.LCurl;
                    case '}':
                        return TokenType.RCurl;
                }
            }

            if (char.IsLetterOrDigit(c))
                return TokenType.Word;
            return TokenType.Separator;
        }

        public Token Read()
        {
            if (!_scanner.MoveNext())
                return null;
            Console.WriteLine(_scanner.Current);
            return _scanner.Current;
        }
    }
}
