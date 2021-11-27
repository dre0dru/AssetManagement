using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace AddressableAssets.Downloaders
{
    public static class AddressablesDownloadersExtensions
    {
        internal static int GetLabelsHashCode(this IEnumerable<AssetLabelReference> assetLabelReferences)
        {
            int hashCode = 0;

            foreach (var assetLabelReference in assetLabelReferences)
            {
                hashCode += assetLabelReference.GetHashCode();
            }

            return hashCode;
        }

        public static bool IsDownloadSuccessful(this AssetsDownloadResult assetsDownloadResult, out Exception failureException)
        {
            failureException = assetsDownloadResult.FailureException;

            return assetsDownloadResult.IsSuccess;
        }
    }
}
