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
        items = items.OrderBy(item => item.ID).ToList();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Refresh List"))
        {
            LoadItems();
        }

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        foreach (var Mergables in items)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Name: " + Mergables.name, GUILayout.Width(200));
            EditorGUILayout.LabelField("ID: " + Mergables.ID, GUILayout.Width(50));
            if (GUILayout.Button("Select", GUILayout.Width(70)))
            {
                Selection.activeObject = Mergables;
                EditorGUIUtility.PingObject(Mergables);
            }
            EditorGUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
    }
}
