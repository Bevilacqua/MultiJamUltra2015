using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class winner : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Game_Setup.winnerOne)
            gameObject.GetComponent<Text>().text = "Player One!";
	    else
            gameObject.GetComponent<Text>().text = "Player Two!";

    }
}
