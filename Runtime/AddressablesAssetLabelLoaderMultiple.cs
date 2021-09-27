using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AddressablesServices
{
    public class
        AddressablesAssetLabelLoaderMultiple<TAsset> :
            IAddressablesLoaderMultiple<AssetLabelReference, TAsset, IList<TAsset>>
        where TAsset : Object
    {
        private readonly Dictionary<object, AsyncOperationHandle<IList<TAsset>>> _operationHandles;

        #if UNITY_2020_3_OR_NEWER
        [UnityEngine.Scripting.RequiredMember]
        #endif
        public AddressablesAssetLabelLoaderMultiple()
            => _operationHandles = new Dictionary<object, AsyncOperationHandle<IList<TAsset>>>();

        public UniTask PreloadAssetsAsync(AssetLabelReference key)
            => LoadAssetsAsync(key);

        public async UniTask<IList<TAsset>> LoadAssetsAsync(AssetLabelReference key)
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

        public bool AreAssetsLoaded(AssetLabelReference key)
        {
            if (_operationHandles.TryGetValue(key.RuntimeKey, out var handle))
            {
                return handle.IsDone;
            }

            return false;
        }

        public IList<TAsset> GetAssets(AssetLabelReference key)
            => _operationHandles[key.RuntimeKey].Result;

        public bool TryGetAssets(AssetLabelReference key, out IList<TAsset> assets)
        {
            assets = default;

            if (_operationHandles.TryGetValue(key.RuntimeKey, out var handle))
            {
                if (handle.IsDone)
                {
                    assets = handle.Result;
                }

                return handle.IsDone;
            }

            return false;
        }

        public void UnloadAssets(AssetLabelReference key)
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

        private AsyncOperationHandle<IList<TAsset>> GetLoadHandle(AssetLabelReference key)
        {
            if (!_operationHandles.TryGetValue(key.RuntimeKey, out var handle))
            {
                handle = Addressables.LoadAssetsAsync<TAsset>(key, null, true);
                _operationHandles.Add(key, handle);
            }

            return handle;
        }
    }
}