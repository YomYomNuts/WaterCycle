using UnityEngine;

public class OGMScript : MonoBehaviour
{
    #region Public Attributs
    public int NumberTap = 3;
    #endregion

    #region Private Attributs
    public int CurrentNumberTap;
    #endregion

    // Use this for initialization
    void Start ()
    {
        gameObject.layer = Const.LAYER_OGM;
        CurrentNumberTap = NumberTap;
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnMouseDown()
    {
        --CurrentNumberTap;
        if (CurrentNumberTap == 0)
            Destroy(gameObject);
    }
}
