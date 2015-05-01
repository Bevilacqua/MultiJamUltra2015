using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void restart()
    {
        Application.LoadLevel(1);
    }

    public void help()
    {
        Application.LoadLevel(3);
    }

    public void home()
    {
        Application.LoadLevel(0);
    }

}
