using UnityEngine;

namespace AddressableAssets.Downloaders.Queue
{
    
    //TODO move to queue settings
    public enum DownloadFailureBehaviour
    {
        None,
        Restart
    }
    
    //TODO restart behaviour as an extension that gets the result of the queue?

    
    //TODO queue, parallel downloads, configurable
    //TODO works with asset labels?
    public class AssetsDownloadQueue : IAssetsDownloadQueue<AssetsDownloadResult>
    {

        private readonly Dequeue<IStartableDownload<AssetsDownloadResult>> _dequeue;

        private bool _isProcessingQueue;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public AssetsDownloadQueue()
        {
            _dequeue = new Dequeue<IStartableDownload<AssetsDownloadResult>>();
            _isProcessingQueue = false;
        }

        public void Enqueue(IStartableDownload<AssetsDownloadResult> downloadable)
        {
            
        }

        //TODO maybe add some IProgress<TQueueProgress> that can report progress and errors
        private async void ProcessDownloadQueue()
        {
            _isProcessingQueue = true;

            while (_dequeue.Count > 0)
            {
                var downloadProgress = _dequeue.PeekFirst();

                var result = await downloadProgress.StartDownloadAsync();

                if (result.IsDownloadSuccessful(out var exception) == false)
                {
                    Debug.LogError($"UNSUCCESS");
                    return;
                }
            }

            if (_dequeue.Count > 0)
            {
                ProcessDownloadQueue();
            }
            else
            {
                _isProcessingQueue = false;
            }
        }
    }
}
