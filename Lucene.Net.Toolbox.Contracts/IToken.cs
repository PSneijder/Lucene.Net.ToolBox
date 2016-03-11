
namespace Lucene.Net.Toolbox.Contracts
{
    public interface IToken
    {
        string Term { get; }
        string Type { get; }
        object Payload { get; }
        int Position { get; }
        int StartOffset { get; }
        int EndOffset { get; }
    }
}