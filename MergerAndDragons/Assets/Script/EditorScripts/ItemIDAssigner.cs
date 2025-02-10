using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class ItemIDAssigner : EditorWindow
{
    
    [MenuItem("Tools/Assign Item IDs")]
    public static void AssignIDs()
    {
        string[] guids = AssetDatabase.FindAssets("t:Mergables");
        int maxID = 0;

        // Find the highest existing ID
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Mergables item = AssetDatabase.LoadAssetAtPath<Mergables>(path);

            if (item != null && item.ID > maxID)
            {
                maxID = item.ID;
            }
        }

        int newID = maxID + 1;

        // Assign new IDs only to items without one
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Mergables item = AssetDatabase.LoadAssetAtPath<Mergables>(path);

            if (item != null && item.ID == 0) // Only assign if ID is not set
            {
                item.ID = newID++;
                EditorUtility.SetDirty(item);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("New Item IDs assigned successfully!");
    }
    
}
