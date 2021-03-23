using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices.Loaders
{
    public interface IGameObjectAddressablesLoader: IAddressablesLoader<AssetReferenceGameObject, GameObject>
    {
        
    }
    
    public class GameObjectAddressablesLoader : 
        BaseAddressablesObjectLoader<AssetReferenceGameObject, GameObject>, 
        IGameObjectAddressablesLoader
    {
    }
}