using System;

namespace Dre0Dru.AddressableAssets.Downloaders
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
