using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindCorrectBlockScript : MonoBehaviour
{
    #region Public Attributs
    public List<GameObject> Levels;
    public CharacterScript _CharacterScript;
    #endregion

    #region Private Attributs
    private List<float> ObjectsChangePos;
    private List<GameObject> ObjectsChange;
    #endregion

    // Use this for initialization
    void Start ()
    {
        ObjectsChangePos = new List<float>();
        ObjectsChange = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnTriggerEnter (Collider parCollider)
    {
        Transform parentCollider = parCollider.transform.parent;
        if (parentCollider.parent.gameObject.name == "Current")
        {
            int levelIndex = _CharacterScript.CurrentLife > 0 ? _CharacterScript.CurrentLife - 1 : 0;
            GameObject levelSelected = Levels[levelIndex];
            for (int i = 0; i < levelSelected.transform.childCount; ++i)
            {
                Transform block = levelSelected.transform.GetChild(i);
                if (block.position.z == parentCollider.position.z)
                {
                    ObjectsChangePos.Add(parentCollider.position.z);
                    ObjectsChange.Add(parentCollider.gameObject);
                    ObjectsChange.Add(block.gameObject);
                    parentCollider.gameObject.SetActive(false);
                    block.position = parentCollider.position;
                    break;
                }
            }
        }
    }
}
