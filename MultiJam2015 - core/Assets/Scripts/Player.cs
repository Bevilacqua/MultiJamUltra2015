using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private int position = 2; //0 == bottom

    private bool currentTurn = false;
    private bool locationChosen = false;

    private Color color;

    public KeyCode up;
    public KeyCode down;
    public KeyCode shoot;

    void Start () {

	}

	void Update () {
        if (currentTurn)
        {
            if (Input.GetKeyDown(up) && position < 4)
            {
                position++;
            } else if(Input.GetKeyDown(down) && position > 0)
            {
                position--;
            }
            
            if(Input.GetKeyDown(shoot))
            {
                locationChosen = true;
            }
        }
	}

    public bool getCurrentTurn()
    {
        return this.currentTurn;
    }

    public void setTurn(bool turn)
    {
        this.currentTurn = turn;
    }

    public bool getChosen()
    {
        return this.locationChosen;
    }

    public void resetPlayer()
    {
        this.color = Game_Setup.colors[Random.Range(0, 5)];
        this.position = 2;
        this.locationChosen = false;
    }

    public int getPosition()
    {
        return this.position;
    }

    public Color getColor()
    {
        return this.color;
    }

    public void setColor(Color color)
    {
        this.color = color;
    }
}
