using UnityEngine;
using System.Collections;

public class CenterNode : MonoBehaviour {
    Player playerOne = GameObject.Find("PlayerOne").GetComponent<Player>();
    Player playerTwo = GameObject.Find("PlayerTwo").GetComponent<Player>();

	void Start () {
	
	}
	
	void Update () {
	   
	}

    public void OnTouched()
    {
        if(playerOne.getTurn())
        {
            //Player one discard
            playerOne.setTurn(false);
            playerTwo.setTurn(true);
            playerOne.resetPlayer();
        }
        else
        {
            //Player two discard
            playerTwo.setTurn(false);
            playerOne.setTurn(true);
            playerTwo.resetPlayer();
        }
    }
}
