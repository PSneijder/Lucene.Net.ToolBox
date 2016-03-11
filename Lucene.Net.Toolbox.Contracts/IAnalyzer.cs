using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lucene.Net.Toolbox.Contracts
{
    public interface IAnalyzer
    {
        string Name { get; set; }
        Type Type { get; set; }

        Task<IEnumerable<IToken>> AnalyzeAsync(string text);
    }
}