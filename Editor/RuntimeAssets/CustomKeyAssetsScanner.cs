using System.Collections.Generic;
using Dre0Dru.EditorExtensions.Editor;
using UnityEditor;
using UnityEngine;

namespace Dre0Dru.RuntimeAssets.Editor
{
    public abstract class CustomKeyAssetsScanner<TKey, TRuntimeAsset> : AssetsScanner
        where TRuntimeAsset : Object
    {
        [SerializeField]
        protected List<ScannerTarget<TKey, TRuntimeAsset>> _scannerTargets;

        public override void Scan()
        {
            foreach (var scannerTarget in _scannerTargets)
            {
                var folderPaths = scannerTarget.ScanFolders.GetFoldersPaths();

                var assets = AssetDatabaseUtils.LoadAssetsAtPaths<TRuntimeAsset>(folderPaths);

                scannerTarget.Target.Clear();

                foreach (var runtimeAsset in assets)
                {
                    scannerTarget.Target.Add(GetKeyFromAsset(runtimeAsset), runtimeAsset);
                }

                AssetDatabaseUtils.SetDirtyAndSave(scannerTarget.Target);
            }

            AssetDatabase.Refresh();
        }

        public abstract TKey GetKeyFromAsset(TRuntimeAsset runtimeAsset);
    }
}
