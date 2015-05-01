using UnityEngine;
using System.Collections;

public class GameBlock : MonoBehaviour {

    private Color color;
    private bool finalBlock = false;

	void Start () {
        color = gameObject.GetComponent<SpriteRenderer>().color;
	}
	
	void Update () {
	
	}

    public void setColor(Color color)
    {
        this.color = color;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    public Color getColor()
    {
        return this.color;
    }

    public void setFinal(bool final)
    {
        this.finalBlock = final;           
    }

    public bool getFinal()
    {
        return this.finalBlock;
    }
}
