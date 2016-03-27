using UnityEngine;
using UnityEngine.UI;

public class DisplayScoreScript : MonoBehaviour
{
    #region Public Attributs
    #endregion

    #region Private Attributs
    #endregion

    // Use this for initialization
    void Start ()
    {
        GetComponent<Text>().text = CharacterScript.GetScore().ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
	}
}
