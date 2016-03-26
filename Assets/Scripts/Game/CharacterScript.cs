using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    #region Public Attributs
    public int NumberLife;
    #endregion

    #region Private Attributs
    public int CurrentLife;
    #endregion

    #region Static Attributs
    private static CharacterScript _instance;
    public static CharacterScript Instance
    {
        get
        {
            if (CharacterScript._instance == null)
                CharacterScript._instance = new CharacterScript();
            return CharacterScript._instance;
        }
    }
    #endregion

    void Awake()
    {
        if (CharacterScript._instance == null)
            CharacterScript._instance = this;
        else if (CharacterScript._instance != this)
            Destroy(this.gameObject);
    }

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
        CurrentLife = CurrentLife + 1 > NumberLife ? NumberLife : CurrentLife + 1;
    }
}
