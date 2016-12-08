using Lucene.Net.Toolbox.Contracts;

namespace Lucene.Net.Toolbox.Info
{
    class TokenInfo
        : IToken
    {
        public string Term { get; internal set; }
        public string Type { get; internal set; }
        public object Payload { get; internal set; }
        public int Position { get; internal set; }
        public int StartOffset { get; internal set; }
        public int EndOffset { get; internal set; }
    }
}