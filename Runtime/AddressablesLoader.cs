using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;

namespace AddressablesServices
{
    public sealed class AddressablesLoader<TAsset> : IAddressablesLoader<TAsset> 
        where TAsset : Object
    {
        private readonly Dictionary<object, AsyncOperationHandle<TAsset>> _preloadedAssets;

        [RequiredMember]
        public AddressablesLoader() =>
            _preloadedAssets = new Dictionary<object, AsyncOperationHandle<TAsset>>();

        public UniTask PreloadAssetsAsync(IEnumerable<AssetReferenceT<TAsset>> assetReferences) =>
            UniTask.WhenAll(assetReferences.Select(PreloadAssetAsync));

        public async UniTask PreloadAssetAsync(AssetReferenceT<TAsset> assetReference)
        {
            if (_preloadedAssets.ContainsKey(assetReference.RuntimeKey))
            {
                return;
            }

            var handle = Addressables.LoadAssetAsync<TAsset>(assetReference);

            try
            {
                await handle;
                lock (_preloadedAssets)
                {
                    //BUG? It is possible to bypass ContainsKey check
                    //when preload started simultaneously for the same key two or more times
                    _preloadedAssets.Add(assetReference.RuntimeKey, handle);
                }
            }
            catch
            {
                Addressables.Release(handle);
                throw;
            }
        }

        public bool IsAssetLoaded(AssetReferenceT<TAsset> assetReference)
        {
            return _preloadedAssets.ContainsKey(assetReference.RuntimeKey);
        }

        public TAsset GetAsset(AssetReferenceT<TAsset> assetReference)
        {
            return _preloadedAssets[assetReference.RuntimeKey].Result;
        }

        public bool TryGetAsset(AssetReferenceT<TAsset> assetReference, out TAsset asset)
        {
            if (_preloadedAssets.TryGetValue(assetReference.RuntimeKey, out var handle))
            {
                asset = handle.Result;
                return true;
            }

            asset = default;
            return false;
        }

        public void UnloadAsset(AssetReferenceT<TAsset> assetReference)
        {
            var key = assetReference.RuntimeKey;

            if (_preloadedAssets.TryGetValue(key, out var handle))
            {
                Addressables.Release(handle);
                _preloadedAssets.Remove(key);
            }
        }

        public void UnloadAssets(IEnumerable<AssetReferenceT<TAsset>> assetReferences)
        {
            foreach (var assetKey in assetReferences)
            {
                UnloadAsset(assetKey);
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
    }
}