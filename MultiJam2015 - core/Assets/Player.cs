using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private int position; //0 == bottom
    private Color color;

    //Color Definitions
    private Color red = Color.red;
    private Color green = Color.green;
    private Color blue = Color.blue;
    private Color yellow = Color.yellow;
    private Color cyan = Color.cyan;
    private Color inactive = new Color(Color.white.r, Color.white.g, Color.white.b, .25f);

    private Color[] colors;

    private bool currentTurn = false;
    private bool locationChosen = false;

    public KeyCode up;
    public KeyCode down;
    public KeyCode shoot;

    void Start () {
	    colors = new Color[] {red, green, blue, yellow, cyan};
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
        this.position = 0;
        this.locationChosen = false;
    }

    public int getPosition()
    {
        return this.position;
    }

    public void setColor(Color color)
    {
        this.color = color;
    }
}
