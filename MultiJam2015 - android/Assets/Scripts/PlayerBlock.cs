using UnityEngine;
using System.Collections;

public class PlayerBlock : MonoBehaviour {
    private int position;
    private Player player;
    public bool playerDefined = false;

	void Start () {
        
	}
	
	void Update () {
        if(!playerDefined)
        {
            if (Camera.main.WorldToScreenPoint(gameObject.transform.position).x < (Camera.main.pixelWidth / 2))
            {
                player = GameObject.Find("PlayerOne").GetComponent<Player>();
            }
            else
            {
                player = GameObject.Find("PlayerTwo").GetComponent<Player>();
            }

            playerDefined = true;
        }
	}

    public void OnTouched() {
        if(playerDefined)
        {
            if(player.getTurn())
            {
                Debug.Log(this.position);
                player.setPosition(this.position);
                player.setChosen(true);
            }
        }
    }


    public void setPosition(int position)
    {
        this.position = position;
        Debug.Log(position);
    }

}
