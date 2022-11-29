using UnityEngine;

namespace Dre0Dru.RuntimeAssets.Editor
{
    public class AssetNameAsKeyAssetsScanner<TRuntimeAsset> : CustomKeyAssetsScanner<string, TRuntimeAsset>
        where TRuntimeAsset : Object
    {
        public override string GetKeyFromAsset(TRuntimeAsset runtimeAsset)
        {
            return runtimeAsset.name;
        }
    }
}
