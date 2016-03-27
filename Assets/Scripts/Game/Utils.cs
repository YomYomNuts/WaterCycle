using UnityEngine;
using System.Collections;

public class Utils : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            CreatePlane();
        }
	}

    public static void CreatePlane(float width = 10.0f, float height = 10.0f)
    {
        Mesh m = new Mesh();
        m.name = "SimplePlane";
        m.vertices = new Vector3[]
        {
            new Vector3(-width, 0.0f, -height),
            new Vector3(-width, 0.0f, height),
            new Vector3(width, 0.0f, height),
            new Vector3(width, 0.0f, -height),
        };
        m.uv = new Vector2[]
        {
            new Vector2(0.0f, 0.0f),
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
        };
        m.triangles = new int[] { 0, 1, 2, 2, 3, 0};
        /*AssetDatabase.CreateAsset(m, "Assets/Models/" + m.name + ".asset");
        AssetDatabase.SaveAssets();*/
        Debug.Log("Save");
    }
}
