using System.IO;
using Lucene.Net.Analysis;

namespace Lucene.Net.Toolbox.PlugIn
{
    public class CustomKeywordAnalyzer
        : KeywordAnalyzer
    {
        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            TokenStream result = new KeywordTokenizer(reader);

            result = new LowerCaseFilter(result);

            return result;
        }
    }
}