using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
public class MakeScriptableObject
{
    [MenuItem("Assets/Create/ScriptableObject/NumberFont")]
    public static void CreateMyAsset()
    {
        ScriptableNumberFont asset = ScriptableObject.CreateInstance<ScriptableNumberFont>();

        AssetDatabase.CreateAsset(asset, "Assets/ScriptableObjects/NumberFonts/NewNumberFont.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}
#endif