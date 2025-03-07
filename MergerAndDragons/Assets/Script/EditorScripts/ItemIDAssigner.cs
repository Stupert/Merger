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

            if (item != null && int.TryParse(item.UID, out int parsedID) && parsedID > maxID)
            {
                maxID = parsedID;
            }
        }

        int newID = maxID + 1;

        // Assign new IDs only to items without one
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Mergables item = AssetDatabase.LoadAssetAtPath<Mergables>(path);

            if (item != null && item.UID == "000") // Only assign if UID is unset
            {
                item.UID = newID.ToString("D3"); // Convert ID to string with leading zeros
                newID++;
                EditorUtility.SetDirty(item);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("New Item IDs assigned successfully!");
    }
}
