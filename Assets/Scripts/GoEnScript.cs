using UnityEngine;
using UnityEngine.SceneManagement;

public class GoEnScript : MonoBehaviour {
    public string NextScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider parCollider)
    {
        switch (parCollider.gameObject.layer)
        {
            case Const.LAYER_WATERDROP:
                SceneManager.LoadScene(NextScene);
                break;
        }
    }
}
