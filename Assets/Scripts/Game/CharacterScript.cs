using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour
{
    #region Public Attributes
    public int NumberLife;
    #endregion

    #region Private Attributes
    private int CurrentLife;
    #endregion

    // Use this for initialization
    void Start ()
    {
        CurrentLife = NumberLife;
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnTriggerEnter(Collider parCollider)
    {
        switch (parCollider.gameObject.layer)
        {
            case Const.LAYER_VAPOR:
            case Const.LAYER_ANIMALS:
                --CurrentLife;
                break;
        }
    }

    public void LifeUp()
    {
        CurrentLife = CurrentLife + 1 > NumberLife ? NumberLife : CurrentLife;
    }
}
