using UnityEngine;

public class PurifyingScript : MonoBehaviour
{
    #region Public Attributs
    public int NumberLife;
    #endregion

    #region Private Attributs
    private int CurrentLife;
    #endregion

    // Use this for initialization
    void Start ()
    {
        gameObject.layer = Const.LAYER_PURIFYING;
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnMouseDown()
    {
        CharacterScript.Instance.LifeUp();
        Destroy(gameObject);
    }
}
