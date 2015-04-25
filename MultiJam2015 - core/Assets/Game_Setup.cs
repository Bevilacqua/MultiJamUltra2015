using UnityEngine;
using System.Collections;

public class Game_Setup : MonoBehaviour {

    public int numberOfRows = 5;
    public int cellsPerRow = 5;

    private int totalCells;
    private GameObject[,] nodes;

    public GameObject pre_Node, pre_CenterBlock, pre_FinishLine;

    private float cellWidth, rowHeight;
    private Camera camera;
    private Player playerOne;
    private Player playerTwo;

    //Color Definitions
    private Color red = Color.red;
    private Color green = Color.green;
    private Color blue = Color.blue;
    private Color yellow = Color.yellow;
    private Color cyan = Color.cyan;
    private Color inactive = new Color(Color.white.r, Color.white.g, Color.white.b, .25f);

    private Color[] colors; 

	void Start () {
        colors = new Color[] { red, green, blue, yellow, cyan };
        camera = Camera.main;

        playerOne = GameObject.Find("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.Find("PlayerTwo").GetComponent<Player>();

        totalCells = ((cellsPerRow * 2) + 3);
        //In pixels (screen)
        cellWidth = camera.pixelWidth / totalCells; //Additional three account for left and right finish line and center deadspace
        rowHeight = (camera.pixelHeight - 100) / (numberOfRows);

        nodes = new GameObject[numberOfRows, totalCells];

        Vector2 cellScale = new Vector2(5, 5);

        pre_Node.transform.localScale = cellScale;

        for(int x = 0; x < numberOfRows; x++) {
            for (int y =  0; y < totalCells; y++)
            {
                if (y == 6)
                {
                    nodes[x, y] = (GameObject)Instantiate(pre_CenterBlock, camera.ScreenToWorldPoint(new Vector3(cellWidth * y + cellWidth - (cellWidth / 2), (rowHeight * x) + rowHeight - (rowHeight / 2), 1)), pre_CenterBlock.transform.localRotation);
                    continue;
                }
                if (y == 0 || y == 12)
                {
                    nodes[x, y] = (GameObject)Instantiate(pre_FinishLine, camera.ScreenToWorldPoint(new Vector3(cellWidth * y + cellWidth - (cellWidth / 2), (rowHeight * x) + rowHeight - (rowHeight / 2), 1)), pre_CenterBlock.transform.localRotation);
                    continue;
                }
                GameObject node = (GameObject) Instantiate(pre_Node, camera.ScreenToWorldPoint(new Vector3(cellWidth * y + cellWidth - (cellWidth / 2), (rowHeight * x) + rowHeight - (rowHeight / 2), 1)), pre_Node.transform.localRotation);
               
                if (y == 1 || y == 11)
                {
                    if (x == 0) node.GetComponent<SpriteRenderer>().color = new Color(Color.red.r, Color.red.g, Color.red.b, .25f);
                    if (x == 1) node.GetComponent<SpriteRenderer>().color = new Color(Color.green.r, Color.green.g, Color.green.b, .25f); ;
                    if (x == 2) node.GetComponent<SpriteRenderer>().color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, .25f);
                    if (x == 3) node.GetComponent<SpriteRenderer>().color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, .25f);
                    if (x == 4) node.GetComponent<SpriteRenderer>().color = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, .25f);
                }

                nodes[x, y] = node;
            }
        }

        playerOne.setTurn(true);
        playerTwo.setTurn(false);
	}
	
	void Update () {
        if (playerOne.getCurrentTurn())
        {
            for (int i = 0; i < 5; i++)
            {
                nodes[i, 0].GetComponent<SpriteRenderer>().color = inactive;
            }
            nodes[playerOne.getPosition(), 0].GetComponent<SpriteRenderer>().color = red;

            if (playerOne.getChosen())
            {
                //Algorithm step 2
                //2.5/ 2.5a
                //3
                //4
                //5
                playerOne.resetPlayer(); //6
                playerOne.setTurn(false); //6
                playerTwo.setTurn(true); //6
                for (int i = 0; i < 5; i++)
                {
                    nodes[i, 0].GetComponent<SpriteRenderer>().color = inactive;
                }
            }
        } 
        
        else if(playerTwo.getCurrentTurn())
        {
            for (int i = 0; i < 5; i++)
            {
                nodes[i, this.totalCells -1].GetComponent<SpriteRenderer>().color = inactive;
            }

            nodes[playerTwo.getPosition(), this.totalCells - 1].GetComponent<SpriteRenderer>().color = red;

            if (playerOne.getChosen())
            {
                //Algorithm step 2
                //2.5/ 2.5a
                //3
                //4
                //5
                playerTwo.resetPlayer(); //6
                playerTwo.setTurn(false); //6
                playerOne.setTurn(true); //6

                for (int i = 0; i < 5; i++)
                {
                    nodes[i, this.totalCells - 1].GetComponent<SpriteRenderer>().color = inactive;
                }
            }
        }
	}
}
