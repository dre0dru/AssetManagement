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
            return CheckIfContentIsDownloaded(await GetDownloadSizeAsync(assetReference));
        }

        public static async UniTask<bool> IsContentDownloaded(IEnumerable<AssetReference> assetReferences)
        {
            return CheckIfContentIsDownloaded(await GetDownloadSizeAsync(assetReferences));
        }
        
        public static async UniTask<bool> IsContentDownloaded(params AssetReference[] assetReferences)
        {
            return CheckIfContentIsDownloaded(await GetDownloadSizeAsync(assetReferences));
        }
        
        public static async UniTask<bool> IsContentDownloaded(AssetLabelReference assetLabelReference)
        {
            return CheckIfContentIsDownloaded(await GetDownloadSizeAsync(assetLabelReference));
        }

        public static async UniTask<bool> IsContentDownloaded(
            IEnumerable<AssetLabelReference> assetLabelReferences)
        {
            return CheckIfContentIsDownloaded(await GetDownloadSizeAsync(assetLabelReferences));
        }
        
        public static async UniTask<bool> IsContentDownloaded(
            params AssetLabelReference[] assetLabelReferences)
        {
            return CheckIfContentIsDownloaded(await GetDownloadSizeAsync(assetLabelReferences));
        }

        private static bool CheckIfContentIsDownloaded(long downloadSize)
        {
            return downloadSize == 0;
        }

        public static UniTask<long> GetDownloadSizeAsync(AssetReference assetReference)
        {
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

        public static UniTask DownloadContentAsync(Action<DownloadStatus> onDownloadProgressUpdate,
            AssetReference assetReference)
        {
            return DownloadContentAsyncInternal(onDownloadProgressUpdate, GetDownloadContentAsyncHandle(assetReference));
        }

        public static UniTask DownloadContentAsync(Action<DownloadStatus> onDownloadProgressUpdate,
            IEnumerable<AssetReference> assetReferences)
        {
            return DownloadContentAsyncInternal(onDownloadProgressUpdate, GetDownloadContentAsyncHandle(assetReferences));
        }
        
        public static UniTask DownloadContentAsync(Action<DownloadStatus> onDownloadProgressUpdate,
            params AssetReference[] assetReferences)
        {
            return DownloadContentAsyncInternal(onDownloadProgressUpdate, GetDownloadContentAsyncHandle(assetReferences));
        }
        
        public static UniTask DownloadContentAsync(Action<DownloadStatus> onDownloadProgressUpdate,
            AssetLabelReference assetLabelReference)
        {
            return DownloadContentAsyncInternal(onDownloadProgressUpdate, GetDownloadContentAsyncHandle(assetLabelReference));
        }
        
        public static UniTask DownloadContentAsync(Action<DownloadStatus> onDownloadProgressUpdate,
            IEnumerable<AssetLabelReference> assetLabelReferences)
        {
            return DownloadContentAsyncInternal(onDownloadProgressUpdate, GetDownloadContentAsyncHandle(assetLabelReferences));
        }
        
        public static UniTask DownloadContentAsync(Action<DownloadStatus> onDownloadProgressUpdate,
            params AssetLabelReference[] assetLabelReferences)
        {
            return DownloadContentAsyncInternal(onDownloadProgressUpdate, GetDownloadContentAsyncHandle(assetLabelReferences));
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

        private static async UniTask DownloadContentAsyncInternal(Action<DownloadStatus> onDownloadProgressUpdate,
            AsyncOperationHandle handle)
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