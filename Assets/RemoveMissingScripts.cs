using UnityEditor;
using UnityEngine;

public static class RemoveMissingScripts 
{
    [MenuItem("Tools/Remove Missing Scripts")]
    private static void RemoveMissingScriptsMenuOption()
    {
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();

        int count = 0;

        foreach (var assetPath in allAssetPaths)
        {
            // Skip meta files and other non-asset files
            if (!assetPath.EndsWith(".prefab") && !assetPath.EndsWith(".unity"))
                continue;

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

            if (prefab != null)
            {
                Debug.Log(prefab.gameObject.name);

                SerializedObject serializedObject = new SerializedObject(prefab);

                foreach (Transform trs in prefab.transform)
                {
                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(trs.gameObject);

                }

                serializedObject.ApplyModifiedProperties();
            }
        }

        if (count > 0)
        {
            Debug.Log($"Removed {count} missing scripts from assets.");
        }
        else
        {
            Debug.Log("No missing scripts found in assets.");
        }

        // Refresh the AssetDatabase to apply changes
        AssetDatabase.Refresh();
    }
}