using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
    public Material material;

    public void SetMaterials()
    {
        foreach (Transform child in transform)
        {
            MeshRenderer MR = child.gameObject.GetComponent<MeshRenderer>();
            if (MR)
            {
                Material[] materials = new Material[1] { material };
                MR.materials = materials;
            }
            else
            {
                continue;
            }
        }
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(MaterialSetter)), CanEditMultipleObjects]
class EditorButtonMaterialSetter : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MaterialSetter BaseClass = (MaterialSetter)target;
        if (GUILayout.Button("Set Material"))
        {
            BaseClass.SetMaterials();
        }
    }
}
#endif