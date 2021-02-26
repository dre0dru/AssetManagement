using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace AddressablesServices
{
    public interface IAddressablesService
    {
        UniTask InitializeAsync();

        UniTask<bool> IsContentDownloaded(AssetLabelReference assetLabelReference);

        UniTask<bool> IsContentDownloaded(IEnumerable<AssetLabelReference> assetLabelReferences);
        
        UniTask<long> GetDownloadSizeAsync(AssetLabelReference assetLabelReference);

        UniTask<long> GetDownloadSizeAsync(IEnumerable<AssetLabelReference> assetLabelReferences);

        UniTask<bool> DownloadContentAsync(AssetLabelReference assetLabelReference,
            Action<float> onDownloadProgressUpdate);

        UniTask<bool> DownloadContentAsync(IEnumerable<AssetLabelReference> assetLabelReferences,
            Action<float> onDownloadProgressUpdate);

        UniTask<bool> CheckForCatalogUpdatesAsync();

        UniTask UpdateCatalogAsync();
    }
}