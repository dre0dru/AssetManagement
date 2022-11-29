using System;
using Dre0Dru.Collections;
using Dre0Dru.EditorExtensions;
using UnityEngine;

namespace Dre0Dru.RuntimeAssets.Editor
{
    [Serializable]
    public struct ScannerTarget<TKey, TRuntimeAsset>
    {
        [SerializeField]
        public UDictionarySo<TKey, TRuntimeAsset> Target;

        [SerializeField]
        public FolderReference[] ScanFolders;
    }
}
