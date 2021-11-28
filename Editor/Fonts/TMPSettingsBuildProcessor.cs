#if TEXTMESHPRO

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
            buildProcessorSettings = AssetDatabaseUtils.FindAsset<TMPBuildProcessorSettings>();

            if (buildProcessorSettings == null || buildProcessorSettings.Enabled == false)
            {
                return false;
            }

            return true;
        }

        private string GetBuildFolderPath(TMPBuildProcessorSettings buildProcessorSettings)
        {
            return Path.Combine(buildProcessorSettings.DestinationFolder.Path, GetAssetName(buildProcessorSettings));
        }

        private string GetIgnoreFolderPath(TMPBuildProcessorSettings buildProcessorSettings)
        {
            return Path.Combine(buildProcessorSettings.SourceFolder.Path, GetAssetName(buildProcessorSettings));
        }

        private string GetAssetName(TMPBuildProcessorSettings buildProcessorSettings)
        {
            return $"{buildProcessorSettings.TMPSettingsAsset.name}.asset";
        }
    }
}

#endif
