#if TEXTMESHPRO

using Shared.Sources.Editor;
using TMPro;
using UnityEngine;

namespace AddressableAssets.Editor.Fonts
{
    [CreateAssetMenu(fileName = "TMPBuildProcessorSettings", menuName = "AddressableAssets/Fonts/TMP Build Processor Settings")]
    public class TMPBuildProcessorSettings : ScriptableObject
    {
        [SerializeField]
        private bool _enabled;

        [SerializeField]
        private FolderReference _sourceFolder;
        
        [SerializeField]
        private FolderReference _destinationFolder;

        [SerializeField]
        private TMP_Settings _tmpSettingsAsset;

        public bool Enabled => _enabled;

        public FolderReference SourceFolder => _sourceFolder;

        public FolderReference DestinationFolder => _destinationFolder;

        public TMP_Settings TMPSettingsAsset => _tmpSettingsAsset;
    }
}

#endif
