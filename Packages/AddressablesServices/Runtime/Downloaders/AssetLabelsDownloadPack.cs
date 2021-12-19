using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.Exceptions;

namespace AddressableAssets.Downloaders
{
    public class AssetLabelsDownloadPack : IStartableDownload<AssetsDownloadResult>,
        IAssetsDownloadStatus<AssetsDownloadStatus>, ITrackableProgress<AssetsDownloadStatus>
    {
        private readonly AssetLabelReference[] _assetLabelReferences;
        private readonly List<IProgress<AssetsDownloadStatus>> _trackedProgress;

        private AsyncOperationHandle _downloadOperationHandle;
        private UniTaskCompletionSource<AssetsDownloadResult> _downloadTaskCompletionSource;
        private AssetsDownloadStatus _downloadStatus;

        public AssetsDownloadStatus DownloadStatus => _downloadStatus;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public AssetLabelsDownloadPack(params AssetLabelReference[] assetLabelReferences)
        {
            _assetLabelReferences = assetLabelReferences;
            _downloadStatus = CreateInitialDownloadStatus();
            _trackedProgress = new List<IProgress<AssetsDownloadStatus>>(1);
        }

        public async UniTask<AssetsDownloadResult> StartDownloadAsync()
        {
            if (_downloadStatus.DownloadOperationStatus == DownloadOperationStatus.InProgress)
            {
                return await _downloadTaskCompletionSource.Task;
            }
            
            _downloadTaskCompletionSource = new UniTaskCompletionSource<AssetsDownloadResult>();

            _downloadStatus.DownloadOperationStatus = DownloadOperationStatus.InProgress;

            _downloadOperationHandle = Addressables.DownloadDependenciesAsync((IEnumerable)_assetLabelReferences,
                Addressables.MergeMode.Union, true);

            var result = await DownloadAsyncInternal();

            if (result.IsDownloadSuccessful(out var failureException))
            {
                _downloadStatus.DownloadSizeBytes = 0;
                _downloadStatus.DownloadOperationStatus = DownloadOperationStatus.Succeeded;
                _downloadStatus.PercentProgress = 1.0f;
            }
            else
            {
                _downloadStatus.DownloadOperationStatus = DownloadOperationStatus.Failed;
                _downloadStatus.FailureException = failureException;
                _downloadStatus.PercentProgress = 0.0f;
            }

            ReportProgressUpdate();

            if (_downloadTaskCompletionSource.TrySetResult(result) == false)
            {
                throw new OperationException("Failed to complete download task");
            }

            return await _downloadTaskCompletionSource.Task;
        }

        public void TrackProgress(IProgress<AssetsDownloadStatus> progress)
        {
            _trackedProgress.Add(progress);
        }

        public void Dispose()
        {
            _trackedProgress.Clear();
        }

        private async UniTask<AssetsDownloadResult> DownloadAsyncInternal()
        {
            try
            {
                await _downloadOperationHandle.ToUniTask(Progress.Create<float>(OnUniTaskProgress));
                return new AssetsDownloadResult()
                {
                    IsSuccess = true
                };
            }
            catch (OperationException e)
            {
                return new AssetsDownloadResult()
                {
                    IsSuccess = false,
                    FailureException = e
                };
            }
            catch (InvalidKeyException e)
            {
                return new AssetsDownloadResult()
                {
                    IsSuccess = false,
                    FailureException = e
                };
            }
        }

        private void OnUniTaskProgress(float _)
        {
            UpdateDownloadStatusProgress();
            ReportProgressUpdate();
        }

        private void UpdateDownloadStatusProgress()
        {
            var downloadStatus = _downloadOperationHandle.GetDownloadStatus();
            _downloadStatus.DownloadedBytes = downloadStatus.DownloadedBytes;
            _downloadStatus.PercentProgress = downloadStatus.Percent;
        }

        // TODO Won't work on WebGL
        // Call Addressables.InitializeAsync() before creating instance of this class
        // https://docs.unity3d.com/Packages/com.unity.addressables@1.19/manual/SynchronousAddressables.html#webgl
        private long GetDownloadSizeBytes()
        {
            return Addressables.GetDownloadSizeAsync((IEnumerable)_assetLabelReferences).WaitForCompletion();
        }

        private AssetsDownloadStatus CreateInitialDownloadStatus()
        {
            var downloadSize = GetDownloadSizeBytes();
            return new AssetsDownloadStatus()
            {
                DownloadSizeBytes = downloadSize,
                DownloadedBytes = 0,
                DownloadOperationStatus = DownloadOperationStatus.NotStarted,
                PercentProgress = downloadSize == 0 ? 0.0f : 1.0f,
                FailureException = null,
            };
        }

        private void ReportProgressUpdate()
        {
            foreach (var progress in _trackedProgress)
            {
                progress.Report(_downloadStatus);
            }
        }
    }
}
