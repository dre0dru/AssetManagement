using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices.Tests
{
    [CreateAssetMenu(fileName = "AssetReferences", menuName = "ScriptableObjects/Tests/AssetReferences")]
    public class AssetReferences : ScriptableObject
    {
        [SerializeField]
        private AssetReferenceT<Object> _invalidReference;
        
        [SerializeField]
        private AssetReferenceComponent<TestComponent> _assetReferenceComponent;

        [SerializeField]
        private AssetReferenceGameObject _assetReferenceGameObject;

        [SerializeField]
        private AssetReferenceTexture _assetReferenceTexture0;
        
        [SerializeField]
        private AssetReferenceTexture _assetReferenceTexture1;

        [SerializeField]
        private AssetReferenceSprite _assetReferenceSprite;
        
        public AssetReferenceT<Object> InvalidReference => _invalidReference;

        public AssetReferenceComponent<TestComponent> AssetReferenceComponent => _assetReferenceComponent;

        public AssetReferenceGameObject AssetReferenceGameObject => _assetReferenceGameObject;

        public AssetReferenceTexture AssetReferenceTexture0 => _assetReferenceTexture0;
        
        public AssetReferenceTexture AssetReferenceTexture1 => _assetReferenceTexture1;

        public AssetReferenceSprite AssetReferenceSprite => _assetReferenceSprite;
    }
}
