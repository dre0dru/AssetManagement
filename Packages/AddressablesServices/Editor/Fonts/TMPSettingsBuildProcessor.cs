#if TEXTMESHPRO && SHARED_SOURCES

using System.IO;
using Shared.Sources.Editor;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace AddressableAssets.Editor.Fonts
{
    public class TMPSettingsBuildProcessor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (IsEnabled(out var settings) == false)
            {
                return;
            }

            AssetDatabase.MoveAsset(GetIgnoreFolderPath(settings), GetBuildFolderPath(settings));

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            if (IsEnabled(out var settings) == false)
            {
                return;
            }

            AssetDatabase.MoveAsset(GetBuildFolderPath(settings), GetIgnoreFolderPath(settings));

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private bool IsEnabled(out TMPBuildProcessorSettings buildProcessorSettings)
        {
            buildProcessorSettings = AssetDatabaseUtils.FindSingleAsset<TMPBuildProcessorSettings>();

            if (buildProcessorSettings == null || buildProcessorSettings.Enabled == false)
            {
                return false;
            }

            return true;
        }

        private string GetBuildFolderPath(TMPBuildProcessorSettings buildProcessorSettings)
        {
            return Path.Combine(AssetDatabase.GUIDToAssetPath(buildProcessorSettings.DestinationFolder.GUID), GetAssetName(buildProcessorSettings));
        }

        private string GetIgnoreFolderPath(TMPBuildProcessorSettings buildProcessorSettings)
        {
            return Path.Combine(AssetDatabase.GUIDToAssetPath(buildProcessorSettings.SourceFolder.GUID), GetAssetName(buildProcessorSettings));
        }

        private string GetAssetName(TMPBuildProcessorSettings buildProcessorSettings)
        {
            return $"{buildProcessorSettings.TMPSettingsAsset.name}.asset";
        }
    }
}

#endif
