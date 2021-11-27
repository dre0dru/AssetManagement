using System.Collections;
using System.Collections.Generic;
using AddressableAssets.Loaders;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

namespace AddressablesServices.Tests
{
    public class AddressablesAssetReferenceLoaderTests
    {
        [UnityTest]
        public IEnumerator PreloadUnloadAsset()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                async UniTask Test<TAsset>(AssetReferenceT<TAsset> assetReference)
                    where TAsset : Object
                {
                    var loader = Setup.CreateAssetReferenceLoader<TAsset>();

                    await loader.PreloadAssetAsync(assetReference);

                    Assert.IsTrue(loader.IsAssetLoaded(assetReference));
                    Assert.IsNotNull(loader.GetAsset(assetReference));

                    loader.UnloadAsset(assetReference);

                    Assert.IsFalse(loader.IsAssetLoaded(assetReference));
                    Assert.Throws<KeyNotFoundException>(() => loader.GetAsset(assetReference));
                    await Setup.Delay();
                }

                await Test(assetReferences.AssetReferenceTexture0);
                await Test(assetReferences.AssetReferenceGameObject);
                await Test(assetReferences.AssetReferenceComponent);
                await Test(assetReferences.AssetReferenceSprite);

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator UseInvalidHandle()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<Object>();
                await CAssert.ThrowsAsync<InvalidKeyException>(async () =>
                    await loader.PreloadAssetAsync(assetReferences.InvalidReference));
                await CAssert.ThrowsAsync<InvalidKeyException>(async () =>
                    await loader.LoadAssetAsync(assetReferences.InvalidReference));

                Assert.IsFalse(loader.IsAssetLoaded(assetReferences.InvalidReference));

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator UseUnloadAfterPreloadAndLoad()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<GameObject>();

                var preloadTask = loader.PreloadAssetAsync(assetReferences.AssetReferenceGameObject);
                loader.UnloadAsset(assetReferences.AssetReferenceGameObject);
                await preloadTask;
                await Setup.Delay();

                var loadTask = loader.LoadAssetAsync(assetReferences.AssetReferenceGameObject);
                loader.UnloadAsset(assetReferences.AssetReferenceGameObject);
                await loadTask;
                await Setup.Delay();

                preloadTask = loader.PreloadAssetAsync(assetReferences.AssetReferenceGameObject);
                loadTask = loader.LoadAssetAsync(assetReferences.AssetReferenceGameObject);
                loader.UnloadAsset(assetReferences.AssetReferenceGameObject);
                await UniTask.WhenAll(preloadTask, loadTask);

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator MultiplePreloads()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<GameObject>();

                List<UniTask> CreatePreloads(int count)
                {
                    var list = new List<UniTask>();

                    for (int i = 0; i < count; i++)
                    {
                        list.Add(loader.PreloadAssetAsync(assetReferences.AssetReferenceGameObject));
                    }

                    return list;
                }

                await UniTask.WhenAll(CreatePreloads(10));

                loader.UnloadAsset(assetReferences.AssetReferenceGameObject);

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator TryGetAsset()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<GameObject>();

                var isSuccess = loader.TryGetAsset(assetReferences.AssetReferenceGameObject, out var asset);

                Assert.IsFalse(isSuccess);
                Assert.IsNull(asset);

                await loader.PreloadAssetAsync(assetReferences.AssetReferenceGameObject);

                isSuccess = loader.TryGetAsset(assetReferences.AssetReferenceGameObject, out asset);

                Assert.IsTrue(isSuccess);
                Assert.IsNotNull(asset);

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator UnloadAllAssets()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<GameObject>();

                await loader.PreloadAssetAsync(assetReferences.AssetReferenceGameObject);
                await loader.PreloadAssetAsync(assetReferences.AssetReferenceComponent);

                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceGameObject));
                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceComponent));

                loader.UnloadAllAssets();

                Assert.IsFalse(loader.IsAssetLoaded(assetReferences.AssetReferenceGameObject));
                Assert.IsFalse(loader.IsAssetLoaded(assetReferences.AssetReferenceComponent));

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator TryGetComponent()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<GameObject>();

                var isSuccess = loader.TryGetComponent(assetReferences.AssetReferenceComponent, out var component);

                Assert.IsFalse(isSuccess);
                Assert.IsNull(component);
                Assert.Throws<KeyNotFoundException>(() =>
                {
                    loader.GetComponent(assetReferences.AssetReferenceComponent);
                });

                await loader.PreloadAssetAsync(assetReferences.AssetReferenceComponent);

                isSuccess = loader.TryGetComponent(assetReferences.AssetReferenceComponent, out component);
                Assert.IsTrue(isSuccess);
                Assert.IsNotNull(component);
                Assert.IsNotNull(loader.GetComponent(assetReferences.AssetReferenceComponent));

                loader.UnloadAsset(assetReferences.AssetReferenceComponent);

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator PreloadUnloadAssetsParams()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<GameObject>();

                await loader.PreloadAssetsAsync(assetReferences.AssetReferenceComponent,
                    assetReferences.AssetReferenceGameObject);

                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceGameObject));
                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceComponent));

                loader.UnloadAssets(assetReferences.AssetReferenceComponent,
                    assetReferences.AssetReferenceGameObject);

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator PreloadUnloadAssetsCollection()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<GameObject>();

                await loader.PreloadAssetsAsync(new List<AssetReferenceT<GameObject>>()
                    { assetReferences.AssetReferenceComponent, assetReferences.AssetReferenceGameObject });

                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceGameObject));
                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceComponent));

                loader.UnloadAssets(new List<AssetReferenceT<GameObject>>()
                    { assetReferences.AssetReferenceComponent, assetReferences.AssetReferenceGameObject });

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator LoadAssetsParams()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<Texture>();

                var result = await loader.LoadAssetsAsync(assetReferences.AssetReferenceTexture0,
                    assetReferences.AssetReferenceTexture1);

                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceTexture0));
                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceTexture1));

                foreach (var texture in result)
                {
                    Assert.IsNotNull(texture);
                }

                loader.UnloadAllAssets();

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator LoadAssetsCollection()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<Texture>();

                var result = await loader.LoadAssetsAsync(new List<AssetReferenceT<Texture>>()
                {
                    assetReferences.AssetReferenceTexture0, assetReferences.AssetReferenceTexture1
                });

                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceTexture0));
                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceTexture1));

                foreach (var texture in result)
                {
                    Assert.IsNotNull(texture);
                }

                loader.UnloadAllAssets();

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator GetAssetsParams()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<Texture>();

                await loader.PreloadAssetsAsync(assetReferences.AssetReferenceTexture0,
                    assetReferences.AssetReferenceTexture1);

                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceTexture0));
                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceTexture1));

                var result = loader.GetAssets(assetReferences.AssetReferenceTexture0,
                    assetReferences.AssetReferenceTexture1);

                foreach (var texture in result)
                {
                    Assert.IsNotNull(texture);
                }

                loader.UnloadAllAssets();

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator GetAssetsCollection()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<Texture>();

                await loader.PreloadAssetsAsync(assetReferences.AssetReferenceTexture0,
                    assetReferences.AssetReferenceTexture1);

                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceTexture0));
                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceTexture1));

                var result = loader.GetAssets(new List<AssetReferenceT<Texture>>()
                {
                    assetReferences.AssetReferenceTexture0,
                    assetReferences.AssetReferenceTexture1
                });

                foreach (var texture in result)
                {
                    Assert.IsNotNull(texture);
                }

                loader.UnloadAllAssets();

                await Setup.Delay();
            });
        
        [UnityTest]
        public IEnumerator TryGetAssetsParams()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<Texture>();

                await loader.PreloadAssetsAsync(assetReferences.AssetReferenceTexture0,
                    assetReferences.AssetReferenceTexture1);

                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceTexture0));
                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceTexture1));

                var isSuccess = loader.TryGetAssets(out var result, assetReferences.AssetReferenceTexture0,
                    assetReferences.AssetReferenceTexture1);

                Assert.IsTrue(isSuccess);
                
                foreach (var texture in result)
                {
                    Assert.IsNotNull(texture);
                }

                loader.UnloadAllAssets();

                await Setup.Delay();
            });

        [UnityTest]
        public IEnumerator TryGetAssetsCollection()
            => UniTask.ToCoroutine(async () =>
            {
                await Setup.InitializeAddressablesAsync();
                var assetReferences = Setup.AssetReferences;

                var loader = Setup.CreateAssetReferenceLoader<Texture>();

                var isSuccess = loader.TryGetAssets(new List<AssetReferenceT<Texture>>()
                {
                    assetReferences.AssetReferenceTexture0,
                    assetReferences.AssetReferenceTexture1
                }, out var result);

                Assert.IsFalse(isSuccess);
                Assert.IsEmpty(result);
                
                await loader.PreloadAssetsAsync(assetReferences.AssetReferenceTexture0,
                    assetReferences.AssetReferenceTexture1);

                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceTexture0));
                Assert.IsTrue(loader.IsAssetLoaded(assetReferences.AssetReferenceTexture1));
                
                isSuccess = loader.TryGetAssets(new List<AssetReferenceT<Texture>>()
                {
                    assetReferences.AssetReferenceTexture0,
                    assetReferences.AssetReferenceTexture1
                }, out result);

                Assert.IsTrue(isSuccess);
                
                foreach (var texture in result)
                {
                    Assert.IsNotNull(texture);
                }

                loader.UnloadAllAssets();

                await Setup.Delay();
            });
    }
}