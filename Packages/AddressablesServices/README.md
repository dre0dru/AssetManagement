[![openupm](https://img.shields.io/npm/v/com.dre0dru.addressables.services?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.dre0dru.addressables.services/)
# Description
A set of classes to convert Unity `Addressables` callbacks/coroutine workflow to async/await with UniTask.
Unity 2020.1+

## Features
- Static `AddressablesService` for common `Addressables` operations.
- Generic `IAddressablesLoader` for all asset types loadable from `Addressables`.
- Type-safe API with `AssetReference`, `AssetReferenceT<T>` and `AssetLabelReference`.  
- `AssetReferenceComponent<T>` and extensions to load `Component` from `Addressables`.
- Sync assets loading during gameplay after async preload phase.

# Installation
This package can be installed as unity module directly from git url in two ways:
- By adding following line in `Packages/manifest.json`:
```
"com.dre0dru.addressables.services": "https://github.com/dre0dru/AddressablesServices.git#upm",
```
- By using `Window/Package Manager/Add package from git URL...` in Unity:
```
https://github.com/dre0dru/AddressablesServices.git#upm
```
- The package is also available on the [openupm registry](https://openupm.com). You can install it via [openupm-cli](https://github.com/openupm/openupm-cli):
```
openupm add com.dre0dru.addressables.services
```
  This also will install all required dependencies.

If package is installed via openupm then no additional steps are required, otherwise see [dependencies](#dependencies).
  
# Dependencies
This package has following dependencies that must be present for package to compile:
- [UniTask](https://github.com/Cysharp/UniTask)
- [ScriptingDefineUtility](https://github.com/Thundernerd/Unity3D-ScriptingDefineUtility)

# Usage
All API has several overloads and accepts following arguments:
- `AddressablesService` and `IAddressablesLoader`:
  - `AssetReferenceT<T>` (and its inheritors)
  - `IEnumerable<AssetReferenceT<T>>`
  - `params AssetReferenceT<T>[]`
- `AddressablesService`:
  - `AssetReference`
  - `IEnumerable<AssetReference>`
  - `params AssetReference[]`
  - `AssetLabelReference`
  - `IEnumerable<AssetLabelReference>`
  - `params AssetLabelReference[]`

Important! `Addressables` will throw exceptions if error occurs during any async operation (e.g. no internet connection when loading remote assets or passing corrupted `AssetReference` as an argument). These exceptions are not handled by package so it is best to surround any API calls with `try catch` blocks. 

## AddressableService
```c#
//Example collection of AssetReference and AssetLabelReference
public class ExampleAssets
{
    public AssetReference AssetReference;
    public AssetReferenceSprite AssetReferenceSprite;
    public AssetReferenceGameObject AssetReferenceGameObject;
    public AssetReferenceT<AudioClip> AssetReferenceAudioClip;
    public AssetReferenceComponent<Camera> AssetReferenceComponent;
    public IEnumerable<AssetReference> AssetReferences;

    public AssetLabelReference AssetLabelReference1;
    public AssetLabelReference AssetLabelReference2;
    public IEnumerable<AssetLabelReference> AssetLabelReferences;
}

var assets = new ExampleAssets();

//Mirrors Addressables.InitializeAsync
await AddressablesService.InitializeAsync();

//Gets download size in bytes
long downloadSize = await AddressablesService.GetDownloadSizeAsync(assets.AssetReference);

//Checks if content is downloaded by comparing download size to 0
bool isContentDownloaded = await AddressablesService.IsContentDownloaded(assets.AssetReferenceSprite);

Action<DownloadStatus> onDownloadProgressUpdate = status =>
{
    Debug.Log($"Downloaded {status.DownloadedBytes}/{status.TotalBytes} ({status.Percent * 100}%)");
};
//Downloads remote content with callback for download progress
await AddressablesService.DownloadContentAsync(onDownloadProgressUpdate, assets.AssetLabelReference1,
    assets.AssetLabelReference2);

//Mirrors Addressables.CheckForCatalogUpdates
bool hasUpdates = await AddressablesService.CheckForCatalogUpdatesAsync();

//Mirrors Addressables.UpdateCatalogs
await AddressablesService.UpdateCatalogAsync();
```
## IAddressablesLoader
```c#
var assets = new ExampleAssets();

//Create loader for specific asset type
IAddressablesLoader<AudioClip> audioClipLoader =
    new AddressablesLoader<AudioClip>();

//Preload assets
await audioClipLoader.PreloadAssetAsync(assets.AssetReferenceAudioClip);

//Safely extract preloaded asset synchronously
if (audioClipLoader.TryGetAsset(assets.AssetReferenceAudioClip, out AudioClip audioClip))
{
    Debug.Log($"Asset loaded successfully: {audioClip}");
}
else
{
    Debug.Log($"No asset was for preloaded for {assets.AssetReferenceAudioClip.RuntimeKey}");
}

//Check if asset was preloaded
bool isAssetPreloaded = audioClipLoader.IsAssetPreloaded(assets.AssetReferenceAudioClip);

//Or use unsafe asset extraction, will throw exception if no asset was preloaded
AudioClip audioClip = audioClipLoader.GetAsset(assets.AssetReferenceAudioClip);

//Unload unused assets to reduce memory usage
audioClipLoader.UnloadAsset(assets.AssetReferenceAudioClip);
//Or unload all assets that was loaded
audioClipLoader.UnloadAllAssets();
```
## AssetReferenceComponent<T>
`AssetReferenceComponent<T>` allows to filter Addressables assets by specific component in Unity Editor. It must be used with `IAddressablesLoader<GameObject>`.
```c#
var assets = new ExampleAssets();

//Create loader for specific asset type
IAddressablesLoader<GameObject> gameObjectLoader =
    new AddressablesLoader<GameObject>();

//AssetReferenceComponent<T> can be passed to IAddressablesLoader<GameObject>
//since it is inherited from AssetReferenceGameObject
await gameObjectLoader.PreloadAssetAsync(assets.AssetReferenceComponent);

//Safely extract preloaded GameObject with Camera component on it
if(gameObjectLoader.TryGetComponent(assets.AssetReferenceComponent, out Camera camera))
{
   var cameraComponent = Object.Instantiate(camera);
}

//Or use unsafe asset extraction, will throw exception if no asset was preloaded
var cameraComponent = gameObjectLoader.GetComponent(assets.AssetReferenceComponent);
```
# License
The software released under the terms of the [MIT license](./LICENSE.md).