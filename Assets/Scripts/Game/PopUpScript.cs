using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PopUpScript : MonoBehaviour
{
    #region Public Class
    [System.Serializable]
    public class SpritePopUp
    {
        public Sprite SpriteToSee;
        public Sprite SpriteToPopUp;
    }
    #endregion

    #region Public Attributs
    public Image ImagePopUp;
    public List<SpritePopUp> SpritesPopUp;
    #endregion

    #region Private Attributs
    private static List<Sprite> SpritesDisplay;
    private LevelScript _LevelScript;
    #endregion

    // Use this for initialization
    void Start ()
    {
        ImagePopUp.enabled = false;
        SpritesDisplay = new List<Sprite>();
        _LevelScript = GetComponentInParent<LevelScript>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    IEnumerator WaitingDescrutionObject(GameObject parGameObject)
    {
        while(parGameObject != null)
        {
            yield return 0.0f;
        }
        _LevelScript.DefilementStop = false;
        ImagePopUp.enabled = false;
    }

    void OnTriggerEnter(Collider parCollider)
    {
        SpriteRenderer spr = parCollider.gameObject.GetComponent<SpriteRenderer>();
        if (spr != null && parCollider.gameObject.layer != Const.LAYER_LEVEL)
        {
            if (!SpritesDisplay.Contains(spr.sprite))
            {
                SpritePopUp spu = SpritesPopUp.Where(s => s.SpriteToSee == spr.sprite).First<SpritePopUp>();
                ImagePopUp.sprite = spu.SpriteToPopUp;
                ImagePopUp.enabled = true;
                SpritesDisplay.Add(spr.sprite);
                _LevelScript.DefilementStop = true;
                StartCoroutine(WaitingDescrutionObject(parCollider.gameObject));
            }
        }
    }
}
