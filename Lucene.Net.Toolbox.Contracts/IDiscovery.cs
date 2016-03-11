using System;

namespace Lucene.Net.Toolbox.Contracts
{
    public delegate void DiscoverEventHandler(IAnalyzer analyzer, EventArgs e);

    public interface IDiscovery
        : IDisposable
    {
        bool IsRunning { get; }

        void Discover();

        event DiscoverEventHandler Discovered;
    }
}