using UnityEngine.AddressableAssets;

namespace AddressablesServices
{
    //TODO make basic implementation that creates DownloadProgress
    //TODO then make DownloadQueue, that inherits this implementation and also
    //implements queue interface
    public interface IAssetsDownloader
    {
        IDownloadProgress GetDownloadProgress(AssetLabelReference assetLabelReference);
        
        IDownloadProgress GetDownloadProgress(params AssetLabelReference[] assetLabelReference);
    }
}