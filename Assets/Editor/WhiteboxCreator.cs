using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class WhiteboxCreator : EditorWindow
{
    private GameObject floor;

    private Vector2Int LevelSize;

    [MenuItem("Tools/LevelCreator %t")]
    public static void GenerateLevel()
    {
        EditorWindow.GetWindow(typeof(WhiteboxCreator));
    }

    private void GenerateLevel(GameObject floor)
    {
        for (int i = 0; i < LevelSize.x; i++)
        {
            for (int k = 0; k < LevelSize.y; k++)
            {
                GameObject go = PrefabUtility.InstantiatePrefab(floor) as GameObject;

                go.transform.SetPositionAndRotation(new Vector3(i*10, 0, k*10), go.transform.rotation);
            }
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Floor Creator");
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Tile Template: ");
        floor = (GameObject)EditorGUILayout.ObjectField(floor, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(" Grid Size: ");
        LevelSize = EditorGUILayout.Vector2IntField("", LevelSize);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate Grid", GUILayout.ExpandWidth(false)))
        {
            if (floor == null)
            {
                Debug.LogError("There's no floor Template!");
                return;
            }
            GenerateLevel(floor);
        }
        EditorGUILayout.EndHorizontal();
    }

}
