using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;

namespace Lucene.Net.Toolbox.PlugIn
{
    public class CustomStandardAnalyzer
        : StandardAnalyzer
    {
        public CustomStandardAnalyzer(Version matchVersion)
            : base(matchVersion) { }
    }
}