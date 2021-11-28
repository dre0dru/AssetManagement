using AddressableAssets.Downloaders;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class AssetsDownloadTest : MonoBehaviour
{
    [SerializeField]
    private AssetLabelReference _assetLabelReference;

    [SerializeField]
    private Text _downloadSize;

    [SerializeField]
    private Text _downloadedBytes;

    [SerializeField]
    private Text _percentProgress;

    [SerializeField]
    private Text _isDownloaded;

    [SerializeField]
    private Text _status;

    private AssetLabelsDownloadPack _downloadPack;

    public void GetDownloadProgress()
    {
        _downloadPack = new AssetLabelsDownloadPack(_assetLabelReference);
        Debug.Log($"Got download pack");
    }

    public async void StartDownload()
    {
        Debug.Log($"Download started");
        _downloadPack.TrackProgress(Progress.Create<AssetsDownloadStatus>(DisplayStatus));
        var result = await _downloadPack.StartDownloadAsync();
        Debug.Log($"Download ended with result: {result.IsSuccess}, {result.FailureException}");
    }

    private void DisplayStatus(AssetsDownloadStatus status)
    {
        _downloadSize.text = $"Size {status.DownloadSizeBytes.ToString()}";
        _percentProgress.text = $"Percent: {status.PercentProgress:F}";
        _isDownloaded.text = $"Is downloaded {status.IsDownloaded}";
        _status.text = $"Status {status.DownloadOperationStatus.ToString()}";
        _downloadedBytes.text = $"Downloaded: {status.DownloadedBytes.ToString()}";
    }
}
