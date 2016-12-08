
namespace Lucene.Net.Toolbox.Discovery
{
    sealed class DefaultDiscovery
        : BaseDiscovery
    {
        public bool IsRunning { get; set; }

        public override void Discover()
        {
            IsRunning = true;

            DiscoverAnalyzers();

            IsRunning = false;
        }
    }
}