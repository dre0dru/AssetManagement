using UnityEngine;
using UnityEngine.AddressableAssets;

namespace AddressablesServices.Loaders
{
    public static class AddressablesLoaderExtensions
    {
        public static bool TryGetComponent<T>(
            this IAddressablesLoader<GameObject> addressablesLoader,
            AssetReferenceGameObject assetReference, out T component)
            where T : Component
        {
            if (addressablesLoader.TryGetAsset(assetReference, out var gameObject))
            {
                return gameObject.TryGetComponent(out component);
            }

            component = default;
            return false;
        }

        public static T GetComponent<T>(
            this IAddressablesLoader<GameObject> addressablesLoader,
            AssetReferenceGameObject assetReference)
            where T : Component
        {
            return addressablesLoader.GetAsset(assetReference).GetComponent<T>();
        }
    }
}