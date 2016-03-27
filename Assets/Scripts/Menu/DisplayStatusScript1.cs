using UnityEngine;
using UnityEngine.UI;

public class DisplayStatusScript1 : MonoBehaviour
{
    #region Public Attributs
    #endregion

    #region Private Attributs
    #endregion

    // Use this for initialization
    void Start ()
    {
        GetComponent<Text>().text = CharacterScript.GetLife() > 0 ? "Win" : "Lose";
    }
	
	// Update is called once per frame
	void Update ()
    {
	}
}
