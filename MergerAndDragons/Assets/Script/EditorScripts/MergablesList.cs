using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class MergablesList : EditorWindow
{
    private Vector2 scrollPosition;
    private List<Mergables> items = new List<Mergables>();

    [MenuItem("Tools/Item List Viewer")]
    public static void ShowWindow()
    {
        GetWindow<MergablesList>("Item List");
    }

    private void OnEnable()
    {
        LoadItems();
    }

    private void LoadItems()
    {
        items.Clear();
        string[] guids = AssetDatabase.FindAssets("t:Mergables");

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Mergables item = AssetDatabase.LoadAssetAtPath<Mergables>(path);
            if (item != null)
            {
                items.Add(item);
            }
        }

        // Sort by UID numerically (001, 002, 003, etc.)
        items = items.OrderBy(item =>
        {
            if (int.TryParse(item.UID, out int numericID))
            {
                return numericID;
            }
            return int.MaxValue; // Put invalid UIDs at the end
        }).ToList();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Refresh List"))
        {
            LoadItems();
        }

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        foreach (var mergable in items)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField(mergable.name, GUILayout.Width(150));
            EditorGUILayout.LabelField("UID: " + mergable.UID, GUILayout.Width(90)); // Display UID instead of ID

            if (GUILayout.Button("Select", GUILayout.Width(70)))
            {
                Selection.activeObject = mergable;
                EditorGUIUtility.PingObject(mergable);
            }

            EditorGUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
    }
}