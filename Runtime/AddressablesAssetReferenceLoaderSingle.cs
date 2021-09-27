using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace AddressablesServices
{
    public sealed class
        AddressablesAssetReferenceLoaderSingle<TAsset> : IAddressablesLoaderSingle<AssetReferenceT<TAsset>, TAsset>
        where TAsset : Object
    {
        private readonly Dictionary<object, AsyncOperationHandle<TAsset>> _operationHandles;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public AddressablesAssetReferenceLoaderSingle() =>
            _operationHandles = new Dictionary<object, AsyncOperationHandle<TAsset>>();

        public UniTask PreloadAssetAsync(AssetReferenceT<TAsset> key) =>
            LoadAssetAsync(key);

        public async UniTask<TAsset> LoadAssetAsync(AssetReferenceT<TAsset> key)
        {
            var handle = GetLoadHandle(key);

            try
            {
                return await handle;
            }
            catch
            {
                Addressables.Release(handle);
                _operationHandles.Remove(key.RuntimeKey);
                throw;
            }
        }

        public bool IsAssetLoaded(AssetReferenceT<TAsset> key)
        {
            if (_operationHandles.TryGetValue(key.RuntimeKey, out var handle))
            {
                return handle.IsDone;
            }

            return false;
        }

        public TAsset GetAsset(AssetReferenceT<TAsset> key) =>
            _operationHandles[key.RuntimeKey].Result;

        public bool TryGetAsset(AssetReferenceT<TAsset> key, out TAsset asset)
        {
            asset = default;

            if (_operationHandles.TryGetValue(key.RuntimeKey, out var handle))
            {
                if (handle.IsDone)
                {
                    asset = handle.Result;
                }

                return handle.IsDone;
            }

            return false;
        }

        public void UnloadAsset(AssetReferenceT<TAsset> key)
        {
            if (_operationHandles.TryGetValue(key.RuntimeKey, out var handle))
            {
                Addressables.Release(handle);
                _operationHandles.Remove(key.RuntimeKey);
            }
        }

        public void UnloadAllAssets()
        {
            foreach (var handle in _operationHandles.Values)
            {
                Addressables.Release(handle);
            }

            _operationHandles.Clear();
        }

        private AsyncOperationHandle<TAsset> GetLoadHandle(AssetReferenceT<TAsset> key)
        {
            if (!_operationHandles.TryGetValue(key.RuntimeKey, out var handle))
            {
                handle = Addressables.LoadAssetAsync<TAsset>(key);
                _operationHandles.Add(key.RuntimeKey, handle);
            }

            return handle;
        }
    }
}