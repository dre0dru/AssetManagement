using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AddressablesServices
{
    public class AddressablesService : IAddressablesService
    {
        public async UniTask InitializeAsync()
        {
            await Addressables.InitializeAsync();
        }

        public async UniTask<bool> IsContentDownloaded(AssetLabelReference assetLabelReference)
        {
            var downloadSize = await GetDownloadSizeAsync(assetLabelReference);
            return downloadSize == 0;
        }

        public async UniTask<bool> IsContentDownloaded(IEnumerable<AssetLabelReference> assetLabelReferences)
        {
            var downloadSize = await GetDownloadSizeAsync(assetLabelReferences);
            return downloadSize == 0;
        }

        public UniTask<long> GetDownloadSizeAsync(AssetLabelReference assetLabelReference)
        {
            var handle = Addressables.GetDownloadSizeAsync(assetLabelReference);
            return GetDownloadSizeAsyncInternal(handle);
        }

        public UniTask<long> GetDownloadSizeAsync(IEnumerable<AssetLabelReference> assetLabelReferences)
        {
            var handle = Addressables.GetDownloadSizeAsync(assetLabelReferences);
            return GetDownloadSizeAsyncInternal(handle);
        }

        private async UniTask<long> GetDownloadSizeAsyncInternal(AsyncOperationHandle<long> handle)
        {
            var downloadSize = await handle;
            Addressables.Release(handle);
            return downloadSize;
        }

        public UniTask<bool> DownloadContentAsync(AssetLabelReference assetLabelReference,
            Action<float> onDownloadProgressUpdate)
        {
            var handle = Addressables.DownloadDependenciesAsync(assetLabelReference, false);

            return DownloadContentAsyncInternal(handle, onDownloadProgressUpdate);
        }

        public UniTask<bool> DownloadContentAsync(IEnumerable<AssetLabelReference> assetLabelReferences,
            Action<float> onDownloadProgressUpdate)
        {
            var handle = Addressables.DownloadDependenciesAsync(assetLabelReferences, false);

            return DownloadContentAsyncInternal(handle, onDownloadProgressUpdate);
        }

        private async UniTask<bool> DownloadContentAsyncInternal(AsyncOperationHandle handle,
            Action<float> onDownloadProgressUpdate)
        {
            while (handle.IsDone == false)
            {
                await UniTask.WaitForEndOfFrame();
                onDownloadProgressUpdate(handle.GetDownloadStatus().Percent);
            }

            bool isSucceeded = handle.Status == AsyncOperationStatus.Succeeded;

            Addressables.Release(handle);

            return isSucceeded;
        }

        public async UniTask<bool> CheckForCatalogUpdatesAsync()
        {
            var handle = Addressables.CheckForCatalogUpdates(true);
            var result = await handle;
            return result.Count > 0;
        }

        public async UniTask UpdateCatalogAsync()
        {
            var handle = Addressables.UpdateCatalogs(null, true);
            await handle;
        }
    }
}