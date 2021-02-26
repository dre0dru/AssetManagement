using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AddressablesServices.Loaders
{
    public abstract class
        BaseAddressablesLoader<TAssetReference, TResult, THandleType> : IAddressablesLoader<TAssetReference, TResult>
        where TAssetReference : AssetReference where TResult : Object where THandleType : Object
    {
        private readonly Dictionary<object, AsyncOperationHandle<THandleType>> _preloadedAssets;

        protected BaseAddressablesLoader()
        {
            _preloadedAssets = new Dictionary<object, AsyncOperationHandle<THandleType>>();
        }

        public UniTask PreloadAssets(IEnumerable<TAssetReference> assetKeys)
        {
            var tasks = new List<UniTask>();

            foreach (var assetKey in assetKeys)
            {
                tasks.Add(PreloadAsset(assetKey));
            }

            return UniTask.WhenAll(tasks);
        }

        public async UniTask PreloadAsset(TAssetReference assetKey)
        {
            if (_preloadedAssets.ContainsKey(assetKey.RuntimeKey))
            {
                Debug.LogWarning($"{Constants.LogsTag} Trying to load already loaded asset: {assetKey.RuntimeKey}");
                return;
            }

            var handle = Addressables.LoadAssetAsync<THandleType>(assetKey);

            await handle;

            _preloadedAssets.Add(assetKey.RuntimeKey, handle);
        }

        public void UnloadAssets(IEnumerable<TAssetReference> assetKeys)
        {
            foreach (var assetKey in assetKeys)
            {
                UnloadAsset(assetKey);
            }
        }

        public void UnloadAsset(TAssetReference assetKey)
        {
            var key = assetKey.RuntimeKey;
            if (_preloadedAssets.TryGetValue(key, out var handle))
            {
                Addressables.Release(handle);
                _preloadedAssets.Remove(key);
            }
            else
            {
                Debug.LogWarning($"{Constants.LogsTag} Trying to unload no loaded asset: {assetKey.RuntimeKey}");
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

        public bool TryGetAsset(TAssetReference assetKey, out TResult asset)
        {
            if (_preloadedAssets.TryGetValue(assetKey.RuntimeKey, out var handle))
            {
                return TryExtractResult(handle.Result, out asset);
            }

            Debug.LogWarning($"{Constants.LogsTag} Trying to get not loaded asset: {assetKey.RuntimeKey}");

            asset = default;
            return false;
        }

        protected abstract bool TryExtractResult(THandleType loadedAsset, out TResult result);
    }
}