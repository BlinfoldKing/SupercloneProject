using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGenEdittor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MapGenerator map = target as MapGenerator;
        if (GUILayout.Button("Generate Map"))
        {
            map.GenerateMap();
        }
    }

}
