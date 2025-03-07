using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Mergables", order = 1)]
public class Mergables : ScriptableObject
{
    public int ID = 000;
    public string UID = "000";
    public string name;
    public Sprite image;
    public Mergables mergeOutcome;
    public float currencyPerTick;
    public string itemDescription = "This is a temporary descrition of this item";
    public float scaleFactor = 0.334f;
    [HideInInspector] public bool isGenerator = false;
    [HideInInspector] public Mergables[] generativeItems;

}

[CustomEditor(typeof(Mergables))]

public class Mergables_Editor : Editor
{
    SerializedProperty isGeneratorProp;
    SerializedProperty generativeItemsProp;

    private void OnEnable()
    {
        isGeneratorProp = serializedObject.FindProperty("isGenerator");
        generativeItemsProp = serializedObject.FindProperty("generativeItems");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); 
        DrawDefaultInspector(); 

        EditorGUILayout.PropertyField(isGeneratorProp, new GUIContent("Is Generator"));

        if (isGeneratorProp.boolValue)
        {
            EditorGUILayout.PropertyField(generativeItemsProp, new GUIContent("Generative Items"), true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
