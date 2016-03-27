using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class LevelScript : MonoBehaviour
{
    #region Public Attributs
    public List<GameObject> PlanesLevels;
    public float SpeedLevel;
    public Vector3 EndLevelsPosition;
    #endregion

    #region Private Attributs
    #endregion

    // Use this for initialization
    void Start ()
    {
        //DiffBetweenLevels = PlanesLevels[1].transform.position - PlanesLevels[0].transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Level Movement
        Vector3 addPosition = new Vector3(0.0f, 0.0f, -SpeedLevel * Time.deltaTime);
        foreach (GameObject level in PlanesLevels)
            level.transform.position += addPosition;
        /*for (int i = 0; i < PlanesLevels.Count; ++i)
        {
            GameObject level = PlanesLevels[i];
            if (level.transform.position.z < EndLevelsPosition.z)
            {
                int previsouIndex = i - 1 < 0 ? PlanesLevels.Count - 1 : i - 1;
                level.transform.position = PlanesLevels[previsouIndex].transform.position + DiffBetweenLevels;
            }
        }*/
    }
}
