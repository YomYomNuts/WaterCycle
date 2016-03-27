using UnityEngine;
using System.Collections.Generic;

public class LevelScript : MonoBehaviour
{
    #region Public Attributs
    public List<GameObject> PlanesLevels;
    public float SpeedLevel;
    [HideInInspector]
    public bool DefilementStop = false;
    #endregion

    #region Private Attributs
    #endregion

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!DefilementStop)
        {
            // Level Movement
            Vector3 addPosition = new Vector3(0.0f, 0.0f, -SpeedLevel * Time.deltaTime);
            foreach (GameObject level in PlanesLevels)
                level.transform.position += addPosition;
        }
    }
}
