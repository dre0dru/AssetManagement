using System;
using AddressableAssets.Loaders;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace AddressablesServices.Tests
{
    public static class Setup
    {
        public static AssetReferences AssetReferences => Resources.Load<AssetReferences>("AssetReferences");

        public static IAssetsReferenceLoader<TAsset> CreateAssetReferenceLoader<TAsset>()
            where TAsset : Object
        {
            return new AssetsReferenceLoader<TAsset>();
        }

        public static async UniTask InitializeAddressablesAsync()
        {
            await Addressables.InitializeAsync();
            await Delay(2000);
        }

        public static UniTask Delay(int milliseconds = 500)
        {
            return UniTask.Delay(TimeSpan.FromMilliseconds(milliseconds));
        }
    }
}