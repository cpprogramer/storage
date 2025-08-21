using StorageTest.GamePlay;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelView))]
public class LevelViewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var level = target as LevelViewDebug;

        if (level != default)
            if (GUILayout.Button("Create Test"))
                level.CreateView();
    }
}