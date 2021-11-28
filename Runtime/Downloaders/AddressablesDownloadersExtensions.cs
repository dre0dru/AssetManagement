using System;

namespace AddressableAssets.Downloaders
{
    public static class AddressablesDownloadersExtensions
    {
        public static bool IsDownloadSuccessful(this AssetsDownloadResult assetsDownloadResult, out Exception failureException)
        {
            failureException = assetsDownloadResult.FailureException;

            return assetsDownloadResult.IsSuccess;
        }
    }
}
