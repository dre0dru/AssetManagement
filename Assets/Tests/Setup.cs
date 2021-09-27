using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace AddressablesServices.Tests
{
    public static class Setup
    {
        public static AssetReferences AssetReferences => Resources.Load<AssetReferences>("AssetReferences");

        public static IAssetReferenceLoader<TAsset> CreateAssetReferenceLoader<TAsset>()
            where TAsset : Object
        {
            return new AssetReferenceLoader<TAsset>();
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