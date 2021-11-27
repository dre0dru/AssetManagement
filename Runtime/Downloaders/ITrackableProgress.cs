using System;

namespace AddressableAssets.Downloaders
{
    public interface ITrackableProgress<out T> : IDisposable
    {
        void TrackProgress(IProgress<T> progress);
    }
}
