using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Scripting;

namespace AddressablesServices.Loaders
{
    public sealed class AddressablesLoader<TAssetReference, TResult> : IAddressablesLoader<TAssetReference, TResult>
        where TAssetReference : AssetReference where TResult : Object
    {
        private readonly Dictionary<object, AsyncOperationHandle<TResult>> _preloadedAssets;

        [RequiredMember]
        public AddressablesLoader()
        {
            _preloadedAssets = new Dictionary<object, AsyncOperationHandle<TResult>>();
        }

        public UniTask PreloadAssetsAsync(IEnumerable<TAssetReference> assetReferences)
        {
            return PreloadAssetsInternal(assetReferences);
        }

        public UniTask PreloadAssetsAsync(params TAssetReference[] assetReferences)
        {
            return PreloadAssetsInternal(assetReferences);
        }

        private UniTask PreloadAssetsInternal(IEnumerable<TAssetReference> assetReferences)
        {
            var tasks = new List<UniTask>();

            foreach (var assetKey in assetReferences)
            {
                tasks.Add(PreloadAssetAsync(assetKey));
            }

            return UniTask.WhenAll(tasks);
        }

        public async UniTask PreloadAssetAsync(TAssetReference assetReference)
        {
            if (_preloadedAssets.ContainsKey(assetReference.RuntimeKey))
            {
                Debug.LogWarning($"{Constants.LogsTag} Trying to load already loaded asset: {assetReference.RuntimeKey}");
                return;
            }

            var handle = Addressables.LoadAssetAsync<TResult>(assetReference);

            await handle;

            lock (_preloadedAssets)
            {
                _preloadedAssets.Add(assetReference.RuntimeKey, handle);
            }
        }

        public void UnloadAssets(IEnumerable<TAssetReference> assetReferences)
        {
            UnloadAssetsInternal(assetReferences);
        }

        public void UnloadAssets(params TAssetReference[] assetReferences)
        {
            UnloadAssetsInternal(assetReferences);
        }

        private void UnloadAssetsInternal(IEnumerable<TAssetReference> assetReferences)
        {
            foreach (var assetKey in assetReferences)
            {
                UnloadAsset(assetKey);
            }
        }

        public void UnloadAsset(TAssetReference assetReference)
        {
            var key = assetReference.RuntimeKey;

            if (_preloadedAssets.TryGetValue(key, out var handle))
            {
                Addressables.Release(handle);
                _preloadedAssets.Remove(key);
            }
            else
            {
                Debug.LogWarning($"{Constants.LogsTag} Trying to unload not loaded asset: {assetReference.RuntimeKey}");
            }
        }

        public void UnloadAllAssets()
        {
            foreach (var handle in _preloadedAssets.Values)
            {
                Addressables.Release(handle);
            }

            _preloadedAssets.Clear();
        }

        public bool TryGetAsset(TAssetReference assetReference, out TResult asset)
        {
            if (_preloadedAssets.TryGetValue(assetReference.RuntimeKey, out var handle))
            {
                asset = handle.Result;
                return true;
            }

            Debug.LogWarning($"{Constants.LogsTag} Trying to get not loaded asset: {assetReference.RuntimeKey}");

            asset = default;
            return false;
        }

        public TResult GetAsset(TAssetReference assetReference)
        {
            return _preloadedAssets[assetReference.RuntimeKey].Result;
        }
    }
}