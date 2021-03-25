using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AddressablesServices
{
    public static class AddressablesService
    {
        public static async UniTask InitializeAsync()
        {
            await Addressables.InitializeAsync();
        }

        public static async UniTask<bool> IsContentDownloaded(AssetLabelReference assetLabelReference)
        {
            var downloadSize = await GetDownloadSizeAsync(assetLabelReference);
            return downloadSize == 0;
        }

        public static async UniTask<bool> IsContentDownloaded(IEnumerable<AssetLabelReference> assetLabelReferences)
        {
            var downloadSize = await GetDownloadSizeAsync(assetLabelReferences);
            return downloadSize == 0;
        }

        public static UniTask<long> GetDownloadSizeAsync(AssetLabelReference assetLabelReference)
        {
            var handle = Addressables.GetDownloadSizeAsync(assetLabelReference);
            return GetDownloadSizeAsyncInternal(handle);
        }

        public static UniTask<long> GetDownloadSizeAsync(IEnumerable<AssetLabelReference> assetLabelReferences)
        {
            var handle = Addressables.GetDownloadSizeAsync(assetLabelReferences);
            return GetDownloadSizeAsyncInternal(handle);
        }

        private static async UniTask<long> GetDownloadSizeAsyncInternal(AsyncOperationHandle<long> handle)
        {
            try
            {
                var downloadSize = await handle;
                return downloadSize;
            }
            finally
            {
                Addressables.Release(handle);
            }
        }

        public static UniTask DownloadContentAsync(AssetLabelReference assetLabelReference,
            Action<DownloadStatus> onDownloadProgressUpdate)
        {
            var handle = Addressables.DownloadDependenciesAsync(assetLabelReference, false);

            return DownloadContentAsyncInternal(handle, onDownloadProgressUpdate);
        }

        public static UniTask DownloadContentAsync(IEnumerable<AssetLabelReference> assetLabelReferences,
            Action<DownloadStatus> onDownloadProgressUpdate)
        {
            var handle = Addressables.DownloadDependenciesAsync(assetLabelReferences, false);

            return DownloadContentAsyncInternal(handle, onDownloadProgressUpdate);
        }

        private static async UniTask DownloadContentAsyncInternal(AsyncOperationHandle handle,
            Action<DownloadStatus> onDownloadProgressUpdate)
        {
            try
            {
                do
                {
                    var downloadStatus = handle.GetDownloadStatus();
                    onDownloadProgressUpdate?.Invoke(downloadStatus);
                    await UniTask.WaitForEndOfFrame();
                } while (handle.IsDone == false);

                if (handle.Status == AsyncOperationStatus.Failed)
                {
                    throw handle.OperationException;
                }
            }
            finally
            {
                Addressables.Release(handle);
            }
        }

        public static async UniTask<bool> CheckForCatalogUpdatesAsync()
        {
            var handle = Addressables.CheckForCatalogUpdates(false);
            try
            {
                var result = await handle;
                return result.Count > 0;
            }
            finally
            {
                Addressables.Release(handle);
            }
        }

        public static async UniTask UpdateCatalogAsync(IEnumerable<string> catalogs = null)
        {
            var handle = Addressables.UpdateCatalogs(catalogs, false);

            try
            {
                await handle;
            }
            finally
            {
                Addressables.Release(handle);
            }
        }
    }
}