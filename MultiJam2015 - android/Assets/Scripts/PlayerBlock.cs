using UnityEngine;
using System.Collections;

public class PlayerBlock : MonoBehaviour {
    private bool isPlayerOne;
    public int position;

	// Use this for initialization
	void Start () {
        if (Camera.main.WorldToScreenPoint(gameObject.transform.position).x < (Camera.main.pixelWidth / 2)) isPlayerOne = true;
        else isPlayerOne = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnTouched() {
        if(isPlayerOne)
        {
            if (GameObject.Find("PlayerOne").GetComponent<Player>().getTurn())
            {
                GameObject.Find("PlayerOne").GetComponent<Player>().setPosition(position);
                GameObject.Find("PlayerOne").GetComponent<Player>().setChosen(true);
            }
        }
        else
        {
            if (GameObject.Find("PlayerTwo").GetComponent<Player>().getTurn())
            {
                GameObject.Find("PlayerTwo").GetComponent<Player>().setPosition(position);
                GameObject.Find("PlayerTwo").GetComponent<Player>().setChosen(true);
            }
        }
    }


    public void setPosition(int position)
    {
        this.position = position;
    }

}
