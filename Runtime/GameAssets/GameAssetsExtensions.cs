using UnityEngine;

namespace Dre0Dru.GameAssets
{
    public static class GameAssetsExtensions
    {
        public static TData ExtractData<TDataAsset, TData>(this TDataAsset asset)
            where TDataAsset : IDataAsset<TData> =>
            asset.Data;

        public static TDataAsset ExtractData<TDataAsset, TData>(this TDataAsset asset, out TData data)
            where TDataAsset : IDataAsset<TData>
        {
            data = asset.Data;
            return asset;
        }

        public static TData CreateCopyAndExtractData<TDataAsset, TData>(this TDataAsset asset)
            where TDataAsset : ScriptableObject, IDataAsset<TData> =>
            Object.Instantiate(asset).ExtractData<TDataAsset, TData>();

        public static TDataAsset CreateCopyAndExtractData<TDataAsset, TData>(this TDataAsset asset, out TData data)
            where TDataAsset : ScriptableObject, IDataAsset<TData>
        {
            Object.Destroy(Object.Instantiate(asset).ExtractData(out data));
            return asset;
        }
    }
}
