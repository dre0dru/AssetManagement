using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices.Loaders
{
    public class GameObjectAddressablesLoader : 
        BaseAddressablesObjectLoader<AssetReferenceGameObject, GameObject>, 
        IGameObjectAddressablesLoader
    {
    }
}