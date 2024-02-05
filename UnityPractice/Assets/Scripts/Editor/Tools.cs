using UnityEditor;
using UnityEngine;

namespace UnityPractice.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}