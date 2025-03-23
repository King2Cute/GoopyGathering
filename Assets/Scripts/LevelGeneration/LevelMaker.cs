using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
public class LevelMaker : MonoBehaviour
{
    public GameObject BlockToAdd;

    public GameObject MiddleTop;

    public GameObject CenterTop;
    public GameObject CenterCenter;
    public GameObject CenterBottom;

    public GameObject CornerTop;
    public GameObject CornerMiddle;
    public GameObject CornerBottom;

    public Vector3 offset;
    public Vector3 cube;

    public int CubeMovement = 2;
    private Vector3 rotationToApply = new Vector3();
    public void AddCube(bool useBlockToAdd)
    {
        GameObject parentObject = new GameObject("Chunk");
        parentObject.transform.SetParent(transform, false);
        parentObject.transform.position = parentObject.transform.position + offset;
        parentObject.layer = LayerMask.NameToLayer("Ground");
        parentObject.tag = "ground";
        for (int x = 0; x < cube.x; x++)
        {
            for (int y = 0; y < cube.y; y++)
            {
                for (int z = 0; z < cube.z; z++)
                {
                    GameObject CurrentEditingBlock = (GameObject)PrefabUtility.InstantiatePrefab(useBlockToAdd ? BlockToAdd : FigureOutPrefab(new Vector3(x, y, z)));
                    CurrentEditingBlock.layer = LayerMask.NameToLayer("Ground");
                    CurrentEditingBlock.tag = "ground";
                    CurrentEditingBlock.gameObject.transform.SetParent(parentObject.transform, false);
                    CurrentEditingBlock.gameObject.transform.position = CurrentEditingBlock.gameObject.transform.position + new Vector3(x * CubeMovement, y * CubeMovement, z * CubeMovement);
                    CurrentEditingBlock.transform.Rotate(rotationToApply);
                    rotationToApply = new Vector3();
                }
            }
        }
    }

    private GameObject FigureOutPrefab(Vector3 position)
    {
        //Top
        if (position.y == (cube.y - 1) && position.x == 0 || position.y == (cube.y - 1) && position.z == 0 || position.y == (cube.y - 1) && position.z == (cube.z - 1) || position.y == (cube.y - 1) && position.x == (cube.x - 1))
        {
            //Top Corners
            if (position.x == 0 && position.z == 0)
            {
                rotationToApply = new Vector3(0, 180, 0);
                return CornerTop;
            }
            if (position.x == 0 && position.z == (cube.z - 1))
            {
                rotationToApply = new Vector3(0, -90, 0);
                return CornerTop;
            }
            if (position.z == 0 && position.x == (cube.x - 1))
            {
                rotationToApply = new Vector3(0, 90, 0);
                return CornerTop;
            }
            if (position.x == (cube.x - 1) && position.z == (cube.z - 1))
            {
                return CornerTop;
            }
            //BottomEdges
            if (position.x == 0)
            {
                rotationToApply = new Vector3(0, -90, 0);
                return CenterTop;
            }
            if (position.x == (cube.x - 1))
            {
                rotationToApply = new Vector3(0, 90, 0);
                return CenterTop;
            }
            if (position.z == 0)
            {
                rotationToApply = new Vector3(0, 180, 0);
                return CenterTop;
            }
            if (position.z == (cube.z - 1))
            {
                return CenterTop;
            }
        }
        //Bottom
        if (position.y == 0 && position.x == 0 || position.y == 0 && position.z == 0 || position.y == 0 && position.z == (cube.z - 1) || position.y == 0 && position.x == (cube.x - 1))
        {
            //Bottom Corners
            if (position.x == 0 && position.z == 0)
            {
                rotationToApply = new Vector3(0, 180, 0);
                return CornerBottom;
            }
            if (position.x == 0 && position.z == (cube.z - 1))
            {
                rotationToApply = new Vector3(0, -90, 0);
                return CornerBottom;
            }
            if (position.z == 0 && position.x == (cube.x - 1))
            {
                rotationToApply = new Vector3(0, 90, 0);
                return CornerBottom;
            }
            if (position.x == (cube.x - 1) && position.z == (cube.z - 1))
            {
                return CornerBottom;
            }
            //BottomEdges
            if (position.x == 0)
            {
                rotationToApply = new Vector3(0, -90, 0);
                return CenterBottom;
            }
            if (position.x == (cube.x - 1))
            {
                rotationToApply = new Vector3(0, 90, 0);
                return CenterBottom;
            }
            if (position.z == 0)
            {
                rotationToApply = new Vector3(0, 180, 0);
                return CenterBottom;
            }
            if (position.z == (cube.z - 1))
            {
                return CenterBottom;
            }
        }
        //EdgesUp
        if (position.x == 0 && position.z == 0)
        {
            rotationToApply = new Vector3(0, 180, 0);
            return CornerMiddle;
        }
        if (position.x == 0 && position.z == (cube.z - 1))
        {
            rotationToApply = new Vector3(0, -90, 0);
            return CornerMiddle;
        }
        if (position.z == 0 && position.x == (cube.x - 1))
        {
            rotationToApply = new Vector3(0, 90, 0);
            return CornerMiddle;
        }
        if (position.x == (cube.x - 1) && position.z == (cube.z - 1))
        {
            return CornerMiddle;
        }
        //TopMiddle
        if(position.y == (cube.y - 1))
        {
            return MiddleTop;
        }
        return CenterCenter;
    }

}

[System.Serializable]
public class BlocksAdded
{
    public GameObject blockToAdd;

    public BlocksAdded(GameObject blockToAddPass)
    {
        blockToAdd = blockToAddPass;
    }
}


[CustomEditor(typeof(LevelMaker))]
class CreateNewRecipeInBase : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        LevelMaker BaseClass = (LevelMaker)target;
        if (GUILayout.Button("Add Cube"))
        {
            BaseClass.AddCube(false);
        }
        if (GUILayout.Button("Add Cube (Only use blockToAdd)"))
        {
            BaseClass.AddCube(true);
        }
    }
}
#endif