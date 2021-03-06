﻿using System;
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

        private bool IsAccumulatedToken(TokenType type) =>
            type == TokenType.Word || type == TokenType.Separator;

        public IEnumerator<Token> Scan(string expressionString)
        {
            var tokenType = TokenType.None;
            var tokenText = new StringBuilder(expressionString.Length);
            int startPosition = 0;

            int position = -1;
            bool treatNextAsText = false;
            foreach (var c in expressionString)
            {
                position++;
                if (!treatNextAsText && c == '\\')
                {
                    treatNextAsText = true;
                    continue;
                }

                var type = GetTokenType(c, treatNextAsText);
                treatNextAsText = false;
                if (type != tokenType || !IsAccumulatedToken(tokenType))
                {
                    if (tokenType != TokenType.None)
                        yield return new Token(tokenType, tokenText.ToString(), expressionString, startPosition);
                    tokenType = type;
                    tokenText.Clear();
                    startPosition = position;
                }

                tokenText.Append(c);
            }
            if (tokenType != TokenType.None)
                yield return new Token(tokenType, tokenText.ToString(), expressionString, startPosition);
            yield return new Token(TokenType.EOF, string.Empty, expressionString, expressionString.Length);
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

            if (char.IsLetterOrDigit(c) || treatAsText)
                return TokenType.Word;
            return TokenType.Separator;
        }

        public Token Read()
        {
            if (!_scanner.MoveNext())
                return null;
            return _scanner.Current;
        }
    }
}
