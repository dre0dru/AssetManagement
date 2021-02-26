using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices
{
    public interface IComponentAddressablesLoader<T> : IAddressablesLoader<AssetReferenceComponent<T>, T>
        where T : Component
    {
    }
}