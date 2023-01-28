using UnityEngine;
using UnityEditor;
using Dollars;

[CustomEditor(typeof(BlendShapesController))]
public class BlendShapesControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BlendShapesController controller = (BlendShapesController)target;
        if (GUILayout.Button("Calibrate"))
        {
            controller.Calibrate();
        }
    }
}