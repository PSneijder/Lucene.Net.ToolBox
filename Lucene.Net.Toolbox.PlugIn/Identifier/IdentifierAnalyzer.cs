using System.IO;
using Lucene.Net.Analysis;

namespace Lucene.Net.Toolbox.PlugIn.Identifier
{
    public sealed class IdentifierAnalyzer
        : Analyzer
    {
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            var result = new IdentifierTokenizer(reader);

            return result;
        }
    }
}