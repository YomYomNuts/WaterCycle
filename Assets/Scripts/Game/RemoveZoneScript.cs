using UnityEngine;
using System.Collections;

public class RemoveZoneScript : MonoBehaviour
{
    #region Public Attributes
    #endregion

    #region Private Attributes
    #endregion

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnTriggerEnter (Collider parCollider)
    {
        switch (parCollider.gameObject.layer)
        {
            case Const.LAYER_ANIMALS:
                Debug.Log("Animal");
                Destroy(parCollider.gameObject);
                break;
        }
    }
}
