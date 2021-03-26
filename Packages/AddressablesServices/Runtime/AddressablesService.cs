using System;
using System.Collections;
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

        public static async UniTask<bool> IsContentDownloaded(AssetReference assetReference)
        {
            var downloadSize = await GetDownloadSizeAsync(assetReference);
            return downloadSize == 0;
        }

        public static async UniTask<bool> IsContentDownloaded(AssetLabelReference assetLabelReference)
        {
            var downloadSize = await GetDownloadSizeAsync(assetLabelReference);
            return downloadSize == 0;
        }

        public static async UniTask<bool> IsContentDownloaded(IEnumerable<AssetReference> assetReferences)
        {
            var downloadSize = await GetDownloadSizeAsync(assetReferences);
            return downloadSize == 0;
        }

        public static async UniTask<bool> IsContentDownloaded(
            IEnumerable<AssetLabelReference> assetLabelReferences)
        {
            var downloadSize = await GetDownloadSizeAsync(assetLabelReferences);
            return downloadSize == 0;
        }

        public static UniTask<long> GetDownloadSizeAsync(AssetReference assetReference)
        {
            var handle = Addressables.GetDownloadSizeAsync(assetReference);
            return GetDownloadSizeAsyncInternal(GetDownloadSizeHandle(assetReference));
        }

        public static UniTask<long> GetDownloadSizeAsync(AssetLabelReference assetLabelReference)
        {
            return GetDownloadSizeAsyncInternal(GetDownloadSizeHandle(assetLabelReference));
        }

        public static UniTask<long> GetDownloadSizeAsync(IEnumerable<AssetReference> assetReferences)
        {
            return GetDownloadSizeAsyncInternal(GetDownloadSizeHandle(assetReferences));
        }

        public static UniTask<long> GetDownloadSizeAsync(IEnumerable<AssetLabelReference> assetLabelReference)
        {
            return GetDownloadSizeAsyncInternal(GetDownloadSizeHandle(assetLabelReference));
        }

        private static AsyncOperationHandle<long> GetDownloadSizeHandle(object key)
        {
            return Addressables.GetDownloadSizeAsync(key);
        }

        private static AsyncOperationHandle<long> GetDownloadSizeHandle(IEnumerable keys)
        {
            return Addressables.GetDownloadSizeAsync(keys);
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

        public static UniTask DownloadContentAsync(AssetReference assetReference,
            Action<DownloadStatus> onDownloadProgressUpdate)
        {
            return DownloadContentAsyncInternal(GetDownloadContentAsyncHandle(assetReference),
                onDownloadProgressUpdate);
        }
        
        public static UniTask DownloadContentAsync(AssetLabelReference assetLabelReference,
            Action<DownloadStatus> onDownloadProgressUpdate)
        {
            return DownloadContentAsyncInternal(GetDownloadContentAsyncHandle(assetLabelReference),
                onDownloadProgressUpdate);
        }

        public static UniTask DownloadContentAsync(IEnumerable<AssetReference> assetReferences,
            Action<DownloadStatus> onDownloadProgressUpdate)
        {
            return DownloadContentAsyncInternal(GetDownloadContentAsyncHandle(assetReferences),
                onDownloadProgressUpdate);
        }
        
        public static UniTask DownloadContentAsync(IEnumerable<AssetLabelReference> assetLabelReferences,
            Action<DownloadStatus> onDownloadProgressUpdate)
        {
            return DownloadContentAsyncInternal(GetDownloadContentAsyncHandle(assetLabelReferences),
                onDownloadProgressUpdate);
        }

        private static AsyncOperationHandle GetDownloadContentAsyncHandle(AssetReference assetReference)
        {
            return Addressables.DownloadDependenciesAsync(assetReference, false);
        }

        private static AsyncOperationHandle GetDownloadContentAsyncHandle(IEnumerable<AssetReference> assetReferences)
        {
            return Addressables.DownloadDependenciesAsync(assetReferences, false);
        }
        
        private static AsyncOperationHandle GetDownloadContentAsyncHandle(AssetLabelReference assetLabelReference)
        {
            return Addressables.DownloadDependenciesAsync(assetLabelReference, false);
        }

        private static AsyncOperationHandle GetDownloadContentAsyncHandle(IEnumerable<AssetLabelReference> assetLabelReferences)
        {
            return Addressables.DownloadDependenciesAsync(assetLabelReferences, false);
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