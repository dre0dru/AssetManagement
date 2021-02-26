using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace AddressablesServices.Editor
{
    internal static class ScriptingDefineSymbolsUtilities
    {
        internal static void AddDefineSymbol(string symbol)
        {
            var defines = GetCurrentDefineSymbols();
            defines.Add(symbol);
            SetDefineSymbols(defines);
        }

        internal static void RemoveDefineSymbols(string symbol)
        {
            var defines = GetCurrentDefineSymbols();
            defines.Remove(symbol);
            SetDefineSymbols(defines);
        }

        private static HashSet<string> GetCurrentDefineSymbols()
        {
            string definesString =
                PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            HashSet<string> defines = new HashSet<string>(definesString.Split(';'));

            return defines;
        }

        private static void SetDefineSymbols(IEnumerable<string> defines)
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(
                EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join(";", defines.ToArray()));
        }
    }
}