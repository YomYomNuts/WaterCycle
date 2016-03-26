using System.Collections.Generic;
using UnityEngine;

public class TrashZoneScript : MonoBehaviour
{
    #region Public Attributs
    public float avrgTime = 0.5f;
    public float peakLevel = 0.6f;
    public float endCountTime = 0.2f;
    public int NumberShakeNeed = 20;
    #endregion

    #region Private Attributs
    private int shakeDir;
    private int shakeCount;
    private Vector3 avrgAcc = Vector3.zero;
    private int countPos;
    private int countNeg;
    private int lastPeak;
    private int firstPeak;
    private bool counting;
    private float timer;
    #endregion

    #region Private Attributs
    private List<GameObject> ListObjectsActif = new List<GameObject>();
    #endregion

    // Use this for initialization
    void Start ()
    {
    }

    // Update is called once per frame
    void Update ()
    {
        ShakeDetector();
        if (counting)
        {
            if (countPos + countNeg > NumberShakeNeed)
            {
                foreach (GameObject go in ListObjectsActif)
                {
                    if (go != null)
                        Destroy(go);
                }
            }
        }
    }

    void OnTriggerEnter(Collider parCollider)
    {
        switch (parCollider.gameObject.layer)
        {
            case Const.LAYER_WASTE:
                ListObjectsActif.Add(parCollider.gameObject);
                break;
        }
    }

    void OnTriggerExit(Collider parCollider)
    {
        switch (parCollider.gameObject.layer)
        {
            case Const.LAYER_WASTE:
                ListObjectsActif.Remove(parCollider.gameObject);
                break;
        }
    }

    bool ShakeDetector()
    {
        Vector3 curAcc = Input.acceleration;
        avrgAcc = Vector3.Lerp(avrgAcc, curAcc, avrgTime * Time.deltaTime);
        curAcc -= avrgAcc;
        int peak = 0;
        if (curAcc.y > peakLevel)
            peak = 1;
        if (curAcc.y < -peakLevel)
            peak = -1;
        if (peak != lastPeak)
            lastPeak = peak;
        if (peak != 0)
        {
            timer = 0;
            if (peak > 0)
                countPos++;
            else
                countNeg++;
            if (!counting)
            {
                counting = true;
                firstPeak = peak;
            }
        }
        else if (counting)
        {
            timer += Time.deltaTime;
            if (timer > endCountTime)
            {
                counting = false;
                shakeDir = firstPeak;
                if (countPos > countNeg)
                    shakeCount = countPos;
                else
                    shakeCount = countNeg;
                countPos = 0;
                countNeg = 0;
                return true;
            }
        }
        return false;
    }
}
