using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;
using Lucene.Net.Util;

namespace Lucene.Net.Toolbox.PlugIn.Identifier
{
    public sealed class ParameterFilter
        : TokenFilter
    {
        public ParameterFilter(TokenStream input) : base(input)
        {
        }

        public override bool IncrementToken()
        {
            throw new System.NotImplementedException();
        }
    }
}