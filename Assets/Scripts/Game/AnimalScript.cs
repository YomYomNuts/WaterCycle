using UnityEngine;
using System;

public class AnimalScript : MonoBehaviour
{
    #region Public Attributes
    public static float ForcePowerSwap = 60.0f;
    #endregion

    #region Private Attributes
    private Rigidbody _Rigidbody;
    private int DirectionSwap;
    #endregion

    // Use this for initialization
    void Start ()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        DirectionSwap = Math.Sign(transform.position.x);
        gameObject.layer = Const.LAYER_ANIMALS;
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnMouseDrag()
    {
        Vector3 inputPosition = Input.mousePosition;
        inputPosition.z = -Camera.main.transform.position.z;
        Vector3 positionInSceen = new Vector3(Camera.main.ScreenToWorldPoint(inputPosition).x, 0.0f, 0.0f);

        if (Math.Sign((positionInSceen - transform.position).x) == DirectionSwap)
        {
            _Rigidbody.AddForce(positionInSceen * ForcePowerSwap);
        }
    }
}
