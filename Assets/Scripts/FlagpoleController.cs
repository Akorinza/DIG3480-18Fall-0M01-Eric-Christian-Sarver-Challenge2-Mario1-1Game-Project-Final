using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagpoleController : MonoBehaviour {

    public Text winText;

	// Use this for initialization
	void Start () {

        winText.text = "";
		
	}
	
	// Update is called once per frame
	void Update () {}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Flagpole"))
            winText.text = "You Win!";

    }

}
