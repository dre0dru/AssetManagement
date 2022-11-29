using UnityEngine;

namespace Dre0Dru.RuntimeAssets.Editor
{
    public abstract class AssetsScanner : ScriptableObject
    {
        #if EASY_BUTTONS_SUPPORT
        [EasyButtons.Button]
        #endif
        public abstract void Scan();
    }
}
