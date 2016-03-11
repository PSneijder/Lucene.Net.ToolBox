
namespace Lucene.Net.Toolbox.Impl.Discovery
{
    public class DefaultDiscovery
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