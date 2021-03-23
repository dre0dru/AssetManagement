using UnityEngine;

namespace AddressablesServices.Loaders
{
    public interface IComponentAddressablesLoader<T> : IAddressablesLoader<AssetReferenceComponent<T>, T>
        where T : Component
    {
    }
    
    public class
        ComponentAddressablesLoader<T> : BaseAddressablesLoader<AssetReferenceComponent<T>, T, GameObject>,
            IComponentAddressablesLoader<T>
        where T : Component
    {
        protected override bool TryExtractResult(GameObject loadedAsset, out T result)
        {
            result = loadedAsset.GetComponent<T>();

            if (result == false)
            {
                Debug.LogWarning(
                    $"{Constants.LogsTag} Trying to get game object without component: {nameof(T)} from loader of type: {GetType()}");
                return false;
            }

            return true;
        }
    }
}