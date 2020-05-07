using System;
using System.Collections.Generic;
using System.Linq;

namespace Cucumber
{
    public class CompositeParserException : ParserException
    {
        public IEnumerable<ParserException> Errors { get; private set; }

        public CompositeParserException(ParserException[] errors)
            : base(GetMessage(errors))
        {
            Errors = errors ?? throw new ArgumentNullException(nameof(errors));
        }

        private static string GetMessage(ParserException[] errors)
        {
            if (errors == null) throw new ArgumentNullException(nameof(errors));

            return "Parser errors:" + Environment.NewLine + string.Join(Environment.NewLine, errors.Select(e => e.Message));
        }
    }
}
