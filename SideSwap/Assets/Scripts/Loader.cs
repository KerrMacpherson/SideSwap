using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used to instantiate GameManager

public class Loader : MonoBehaviour {

    public GameObject gameManager;

	void Awake () {
        if (GameManager.instance == null)
            Instantiate(gameManager);
	}
}
