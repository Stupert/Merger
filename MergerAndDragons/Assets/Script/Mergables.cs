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
    [HideInInspector] public int generatorCost;

}

[CustomEditor(typeof(Mergables))]

public class Mergables_Editor : Editor
{
    SerializedProperty isGeneratorProp;
    SerializedProperty generativeItemsProp;
    SerializedProperty generatorIntProp;

    private void OnEnable()
    {
        isGeneratorProp = serializedObject.FindProperty("isGenerator");
        generativeItemsProp = serializedObject.FindProperty("generativeItems");
        generatorIntProp = serializedObject.FindProperty("generatorCost");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update(); 
        DrawDefaultInspector(); 

        EditorGUILayout.PropertyField(isGeneratorProp, new GUIContent("Is Generator"));

        if (isGeneratorProp.boolValue)
        {
            EditorGUILayout.PropertyField(generatorIntProp, new GUIContent("Generator Int Value"));
            EditorGUILayout.PropertyField(generativeItemsProp, new GUIContent("Generative Items"), true);

        }

        serializedObject.ApplyModifiedProperties();
    }
}
