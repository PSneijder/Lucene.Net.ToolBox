using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Miscellaneous;
using Lucene.Net.Util;

namespace Lucene.Net.Toolbox.PlugIn.Unique
{
    public sealed class UniqueAnalyzer
        : PatternAnalyzer
    {
        private static readonly ISet<string> StopWords = new HashSet<string>();
        private static readonly Regex RegEx = new Regex(@"(\-)|(\s+)", RegexOptions.Compiled);

        public UniqueAnalyzer()
            : this(Version.LUCENE_30, RegEx, true, StopWords) { }

        public UniqueAnalyzer(Version matchVersion, Regex regex, bool toLowerCase, ISet<string> stopWords)
            : base(matchVersion, regex, toLowerCase, stopWords) { }

        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            var result = base.TokenStream(fieldName, reader);

            return result;
        }
    }
}