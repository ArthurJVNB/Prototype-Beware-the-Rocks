using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Terrain))]
public class CustomTerrainEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Terrain terrain = (Terrain)target;
        if (GUILayout.Button("See terrain"))
        {
            terrain.ChangeSprite(0);
        }
    }
}
