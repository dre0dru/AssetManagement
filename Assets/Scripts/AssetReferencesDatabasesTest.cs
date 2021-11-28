using System.Collections;
using System.Collections.Generic;
using AddressableAssets.AssetReferencesDatabases;
using AddressableAssets.SpriteAtlases;
using DefaultNamespace;
using Shared.Sources.ScriptableDatabase;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetReferencesDatabasesTest : MonoBehaviour
{
    [SerializeField]
    private AssetReferencesScriptableDatabase<Sprite> _assetReferencesScriptableDatabase;
    
    [SerializeField]
    private AssetReferencesSpriteDatabase _assetReferencesSpriteDatabase;

    [SerializeField]
    private ScriptableDatabase<string, AssetReferenceT<Sprite>> _generic;


    
    

}
