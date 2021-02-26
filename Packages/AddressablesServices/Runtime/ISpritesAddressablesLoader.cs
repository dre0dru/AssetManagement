using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices
{
    public interface ISpritesAddressablesLoader: IAddressablesLoader<AssetReferenceT<Sprite>, Sprite>
    {
        
    }
}