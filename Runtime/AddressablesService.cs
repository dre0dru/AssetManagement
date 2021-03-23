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

        public UniTask<bool> DownloadContentAsync(AssetLabelReference assetLabelReference,
            IProgress<float> onDownloadProgressUpdate)
        {
            var handle = Addressables.DownloadDependenciesAsync(assetLabelReference, false);

            return DownloadContentAsyncInternal(handle, onDownloadProgressUpdate);
        }

        public UniTask<bool> DownloadContentAsync(IEnumerable<AssetLabelReference> assetLabelReferences,
            IProgress<float> onDownloadProgressUpdate)
        {
            var handle = Addressables.DownloadDependenciesAsync(assetLabelReferences, false);

            return DownloadContentAsyncInternal(handle, onDownloadProgressUpdate);
        }

        private async UniTask<bool> DownloadContentAsyncInternal(AsyncOperationHandle handle,
            IProgress<float> onDownloadProgressUpdate)
        {
            var downloadTask = handle.ToUniTask(onDownloadProgressUpdate);

            try
            {
                await downloadTask;
                return handle.Status == AsyncOperationStatus.Succeeded;
            }
            finally
            {
                Addressables.Release(handle);
            }
        }

        public async UniTask<bool> CheckForCatalogUpdatesAsync()
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

        public async UniTask UpdateCatalogAsync(IEnumerable<string> catalogs = null)
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