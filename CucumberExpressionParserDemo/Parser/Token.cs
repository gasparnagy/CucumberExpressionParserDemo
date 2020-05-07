using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Cucumber
{
    public class Token
    {
        public Token(TokenType tokenType, string text)
        {
            TokenType = tokenType;
            Text = text;
        }

        public TokenType TokenType { get; }
        public string Text { get; }

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
