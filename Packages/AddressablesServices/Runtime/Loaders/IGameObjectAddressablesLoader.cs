using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices
{
    public interface IGameObjectAddressablesLoader: IAddressablesLoader<AssetReferenceGameObject, GameObject>
    {
        
    }
}