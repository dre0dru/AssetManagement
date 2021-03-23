using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices.Loaders
{
    public interface ISpritesAddressablesLoader: IAddressablesLoader<AssetReferenceT<Sprite>, Sprite>
    {
        
    }
    
    public class SpriteAddressablesLoader : BaseAddressablesObjectLoader<AssetReferenceT<Sprite>, Sprite>, ISpritesAddressablesLoader
    {
    }
}