﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderScript : MonoBehaviour
{
    #region Public Attributs
    #endregion

    #region Private Attributs
    #endregion

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void LoadScene(string parSceneName)
    {
        SceneManager.LoadScene(parSceneName);
    }
}
