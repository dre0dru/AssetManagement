using AddressableAssets.AssetReferences;
using Shared.Sources.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetReferencesDatabasesTest : MonoBehaviour
{
    [SerializeField]
    private AssetReferencesUDictionarySo<Sprite> _assetReferencesScriptableDatabase;
    
    [SerializeField]
    private AssetReferencesSpriteDatabase _assetReferencesSpriteDatabase;

    [SerializeField]
    private UDictionarySo<string, AssetReferenceT<Sprite>> _generic;
}
