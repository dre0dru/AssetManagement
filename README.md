[![openupm](https://img.shields.io/npm/v/com.dre0dru.assetmanagement?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.dre0dru.assetmanagement/)
# Addressables Services
A set of classes to convert Unity `Addressables` callbacks/coroutine workflow to async/await with UniTask.
Unity 2020.1+


## Installation
This package can be installed as a Unity package from git url.

### Latest release
- Add following line in `Packages/manifest.json`:
```
"com.dre0dru.assetmanagement": "https://github.com/dre0dru/AssetManagement.git",
```
- Use `Window/Package Manager/Add package from git URL...` in Unity:
```
https://github.com/dre0dru/AssetManagement.git
```

## Dependencies
This package has following dependencies that must be present for package to compile:
- [UniTask](https://github.com/Cysharp/UniTask)

### Optional
- [ScriptingDefineUtility](https://github.com/Thundernerd/Unity3D-ScriptingDefineUtility) enables full logs switch for `Addressables` under `Window->Asset Management->Addressables->Logs`
- [VContainer](https://github.com/hadashiA/VContainer) - optional extension methods for bindings
- [Shared.Sources](https://github.com/dre0dru/Shared.Sources) used for implementations of  interfaces in several modules, interfaces are not dependent on this package

## Loaders
Generic type-safe assets loader.
Can be used for both async assets loading on demand or async preloading and sync usage during gameplay.
```csharp
[SerializeField]  
private AssetReferenceT<AudioClip> _clipAssetReference01;  
[SerializeField]  
private AssetReferenceT<AudioClip> _clipAssetReference02;

IAssetsReferenceLoader<AudioClip> loader = new AssetsReferenceLoader<AudioClip>();  
  
//Or more generic IAssetsLoader<TKey, TAsset>  
IAssetsLoader<AssetReferenceT<AudioClip>, AudioClip> loader =  
 new AssetsReferenceLoader<AudioClip>();  
  
//Preload assets  
await loader.PreloadAssetAsync(_clipAssetReference01);  
await loader.PreloadAssetsAsync(_clipAssetReference01, _clipAssetReference02);  
  
//Safely extract preloaded asset synchronously  
if (loader.TryGetAsset(_clipAssetReference01, out AudioClip audioClip))  
{  
 Debug.Log($"Asset loaded successfully: {audioClip}");  
}  
else  
{  
 Debug.Log($"No asset was for preloaded for {_clipAssetReference01.RuntimeKey}");  
}  
  
//Check if asset was loaded  
bool isAssetPreloaded = loader.IsAssetLoaded(_clipAssetReference01);  
  
//Or use unsafe asset extraction, will throw exception if no asset was preloaded  
AudioClip audioClip = loader.GetAsset(_clipAssetReference01);  
  
//Load asynchronously on demand, will be completed immediately if asset is already loaded
AudioClip audioClip = await loader.LoadAssetAsync(_clipAssetReference01);  
AudioClip[] audioClips = await loader.LoadAssetsAsync(_clipAssetReference01, _clipAssetReference02);  
  
//Unload unused assets  
loader.UnloadAsset(_clipAssetReference01);  
//Or unload all assets that were loaded  
loader.UnloadAllAssets();
```


### AssetReferenceComponent
`AssetReferenceComponent<T>` allows to filter Addressables assets by specific component in Unity Editor. Must be used with `IAssetsReferenceLoader<GameObject>`.
```csharp
[SerializeField]  
private AssetReferenceComponent<Camera> _cameraReference;

IAssetsReferenceLoader<GameObject> gameObjectLoader =  
 new AssetsReferenceLoader<GameObject>();  
  
//AssetReferenceComponent<T> can be passed to IAssetsReferenceLoader<GameObject>  
//since it is inherited from AssetReferenceGameObject  
await gameObjectLoader.PreloadAssetAsync(_cameraReference);  
  
//Safely extract preloaded GameObject with Camera component on it  
if(gameObjectLoader.TryGetComponent(_cameraReference, out Camera camera))  
{  
 Camera cameraInstance = Object.Instantiate(camera);  
}  
  
//Or use unsafe asset extraction, will throw exception if no asset was preloaded  
var cameraComponent = gameObjectLoader.GetComponent(_cameraReference);
```

### VContainer extensions
```csharp
//Will bind IAssetReferenceLoader<T> to AssetRefereceLoader<T> implementation
builder.RegisterAssetsReferenceLoader<TAsset>(Lifetime.Scoped);
```

## Downloaders
Any downloader implements several interfaces: `IStartableDownload<TDownloadResult>`,  
`IAssetsDownloadStatus<TDownloadStatus>` and optional ` ITrackableProgress<TDownloadStatus>`.
Currently only `AssetLabelsDownloadPack` is provided since downloading by `AssetReferenceLabel` is the most common way.
```csharp
//Create download pack
var downloadPack = new AssetLabelsDownloadPack(_assetLabelReference);

//Set method to track download status changes, accepts implementations of IProgress<T>
_downloadPack.TrackProgress(Progress.Create<AssetsDownloadStatus>(DisplayStatus));

//Or get status manually
var status = _downloadPack.DownloadStatus;

//Start download
bool isSuccess = await _downloadPack.StartDownloadAsync();

if(!isSuccess){
 //Download can be restarted anytime
 isSuccess = await _downloadPack.StartDownloadAsync();
}

_downloadPack.Dispose();
```

```csharp
private void DisplayStatus(AssetsDownloadStatus status)  
{  
 _downloadSize.text = $"Size {status.DownloadSizeBytes.ToString()}";  
 _percentProgress.text = $"Percent: {status.PercentProgress:F}";  
 _isDownloaded.text = $"Is downloaded {status.IsDownloaded}";  
 _status.text = $"Status {status.DownloadOperationStatus.ToString()}";  
 _downloadedBytes.text = $"Downloaded: {status.DownloadedBytes.ToString()}";  
}
```

## AssetReferences
Simple Unity `ScriptableObject` Dictionaries for `AssetLabelReference` and `AssetReference<T>`:
- `AssetLabelsUDictionarySo<TKey>`
- `AssetReferencesUDictionarySo<TKey, TAsset>`

## Fonts
> When using TMP with Addressables without initial preparations there will be either duplicated assets or broken Unity Editor workflow.

1. Inherit `TMPAddressableAssets<TLocaleKey, TSpriteAssetKey>`
2. Create `ScriptableObject` instance, set it up with fonts/sprite assets addressable references
3. Create instance of `TMPBuildProcessorSettings`, set it up
```csharp
TMPAddressableAssets<string, string> assets;
IAssetsReferenceLoader<TMP_FontAsset> fontsLoader;
IAssetsReferenceLoader<TMP_SpriteAsset> spriteAssetsLoader;

//Setup/bind service instance
IFontsService<string, string> service = new FontsService<string, string>(
  assets, fontsLoader, spriteAssetsLoader
);

//Load font by locale
await service.LoadFontForLocale("en");

//Unload unused fonts
service.UnloadFontForLocale("ru");

await service.LoadSpriteAsset("CurrencyIcons");

service.UnloadSpriteAsset("CurrencyIcons");

```

## SpriteAtlases
`ISpriteAtlasProvider`  implementation can be used to control Unity `SpriteAtlase` loading/unloading at runtime via `Addressables`.
```csharp
IAssetsReferenceLoader<SpriteAtlas> spriteAtlasLoader;  
ISpriteAtlasAddressableAssets spriteAtlasAddressableAssets;

ISpriteAtlasProvider provider = new SpriteAtlasProvider(
  spriteAtlasLoader, spriteAtlasAddressableAssets
);

//Listen SpriteAtlasManager atlas requests
//Requeted SpriteAtlases will be loaded automatically
provider.SubscribeToAtlasManagerRequests();

//Unload all SpriteAtlases that we loaded by this provider
provider.UnloadSpriteAtlases();

provider.UnsubscribeFromAtlasManagerRequests();
```
### VContainer extensions
Entry point for managing provider lifetime:
 ```csharp
 builder.RegisterEntryPoint<SpriteAtlasesProviderEntryPoint>();
 ```

# License
The software released under the terms of the [MIT license](./LICENSE.md).