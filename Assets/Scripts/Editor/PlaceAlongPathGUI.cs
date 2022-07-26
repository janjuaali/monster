using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(RoadPositioning)), CanEditMultipleObjects]
public class PlaceAlongPathGUI : Editor
{
    private RoadPositioning _roadPositioning;

    private void Awake()
    {
        _roadPositioning = (RoadPositioning)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();        

        if (GUILayout.Button("Place"))
        {
            foreach (RoadPositioning roadPositioning in targets)
            {
                roadPositioning.Place();
            }
            
        }


        if(GUILayout.Button("Place without rotation"))
        {
            foreach (RoadPositioning roadPositioning in targets)
            {
                roadPositioning.PlaceWithoutRotation();
            }
        }


        if(GUILayout.Button("Place by curve"))
        {
            foreach (RoadPositioning roadPositioning in targets)
            {
                roadPositioning.PlaceByCurve();
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(_roadPositioning);
            EditorSceneManager.MarkSceneDirty(_roadPositioning.gameObject.scene);
        }
    }
}
