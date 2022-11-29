using System;

namespace Dre0Dru.AddressableAssets.Downloaders
{
    public interface ITrackableProgress<out T> : IDisposable
    {
        void TrackProgress(IProgress<T> progress);
    }
}
