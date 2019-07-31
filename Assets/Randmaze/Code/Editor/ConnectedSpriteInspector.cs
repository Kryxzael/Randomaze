using UnityEditor;
using UnityEngine;

[System.Obsolete("This file may end up breaking ConnectedTile instances. Do not use")]
//[CustomEditor(typeof(ConnectedTile))]
public class ConnectedTileInspector : Editor
{
    private const int WIDTH = 70;
    private const int HEIGHT = WIDTH;

    public override void OnInspectorGUI()
    {
        ConnectedTile _ = target as ConnectedTile;

        EditorGUILayout.BeginHorizontal();
        {
            CreateObjectField(ref _.TopLeft);
            CreateObjectField(ref _.Top);
            CreateObjectField(ref _.TopRight);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            CreateObjectField(ref _.Left);
            CreateObjectField(ref _.Middle);
            CreateObjectField(ref _.Right);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            CreateObjectField(ref _.BottomLeft);
            CreateObjectField(ref _.Bottom);
            CreateObjectField(ref _.BottomRight);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        CreateObjectField(ref _.TopVertical);
        CreateObjectField(ref _.Vertical);
        CreateObjectField(ref _.BottomVertical);

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        {
            CreateObjectField(ref _.LeftHorizontal);
            CreateObjectField(ref _.Horizontal);
            CreateObjectField(ref _.RightHorizontal);
        }

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        CreateObjectField(ref _.Single);
    }

    private void CreateObjectField(ref SpriteAnimation field)
    {
        field = EditorGUILayout.ObjectField(
            field, 
            typeof(SpriteAnimation), 
            false, 
                GUILayout.Width(WIDTH), 
                GUILayout.Height(HEIGHT)
        ) as SpriteAnimation;
    }
}