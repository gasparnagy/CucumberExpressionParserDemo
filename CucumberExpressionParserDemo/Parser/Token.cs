using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Cucumber
{
    public class Token
    {
        public Token(TokenType tokenType, string text, string sourceLine, int startPosition)
        {
            TokenType = tokenType;
            Text = text;
            SourceLine = sourceLine;
            StartPosition = startPosition;
        }

        public TokenType TokenType { get; }
        public string Text { get; }
        public string SourceLine { get; }
        public int StartPosition { get; }

        public bool IsEOF => TokenType == TokenType.EOF;

        public void Detach()
        {
            //nop
        }

        public override string ToString()
        {
            return $"#{TokenType}: [{Text}]";
        }
    }
}
