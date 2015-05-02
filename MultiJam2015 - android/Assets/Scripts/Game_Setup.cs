using UnityEngine;
using System.Collections;

public class Game_Setup : MonoBehaviour {

    public int numberOfRows = 5;
    public int cellsPerRow = 5;

    private int nodesPerRow;
    private GameObject[,] nodes;

    public GameObject pre_Node, pre_CenterBlock, pre_FinishLine;

    private float cellWidth, rowHeight;
    private Camera camera;
    private Player playerOne;
    private Player playerTwo;

    public static bool winnerOne = false;

    //Color Definitions
    public static Color red = Color.red;
    public static Color green = Color.green;
    public static Color blue = Color.blue;
    public static Color yellow = Color.yellow;
    public static Color cyan = Color.cyan;
    public static Color inactive = new Color(Color.white.r, Color.white.g, Color.white.b, .25f);
    public static Color completed = new Color(Color.gray.r, Color.gray.g, Color.gray.b, .40f);
    public static Color unplaced = Color.white;

    public static Color[] colors;

	void Start () {
        colors = new Color[] { red, green, blue, yellow, cyan };

        camera = Camera.main;

        playerOne = GameObject.Find("PlayerOne").GetComponent<Player>();
        playerTwo = GameObject.Find("PlayerTwo").GetComponent<Player>();

        nodesPerRow = ((cellsPerRow * 2) + 3);
        //In pixels (screen)
        cellWidth = camera.pixelWidth / nodesPerRow; //Additional three account for left and right finish line and center deadspace
        rowHeight = (camera.pixelHeight * .85f) / (numberOfRows);

        nodes = new GameObject[nodesPerRow, numberOfRows];

        Vector2 cellScale = new Vector2(5, 5);

        pre_Node.transform.localScale = cellScale;

        for(int y = 0; y < numberOfRows; y++) {
            for (int x =  0; x < nodesPerRow; x++)
            {
                if (x == 6)
                {
                    nodes[x, y] = (GameObject)Instantiate(pre_CenterBlock, camera.ScreenToWorldPoint(new Vector3(cellWidth * x + cellWidth - (cellWidth / 2), (rowHeight * y) + rowHeight - (rowHeight / 2), 1)), pre_CenterBlock.transform.localRotation);
                    continue;
                }
                if (x == 0 || x == 12)
                {
                    nodes[x, y] = (GameObject)Instantiate(pre_FinishLine, camera.ScreenToWorldPoint(new Vector3(cellWidth * x + cellWidth - (cellWidth / 2), (rowHeight * y) + rowHeight - (rowHeight / 2), 1)), pre_CenterBlock.transform.localRotation);
                    nodes[x, y].GetComponent<PlayerBlock>().setPosition(y);
                    continue;
                }
                GameObject node = (GameObject) Instantiate(pre_Node, camera.ScreenToWorldPoint(new Vector3(cellWidth * x + cellWidth - (cellWidth / 2), (rowHeight * y) + rowHeight - (rowHeight / 2), 1)), pre_Node.transform.localRotation);
               
                if (x == 1 || x == 11)
                {
                    node.GetComponent<GameBlock>().setFinal(true);
                    if (y == 0) node.GetComponent<SpriteRenderer>().color = new Color(Color.red.r, Color.red.g, Color.red.b, .25f);
                    if (y == 1) node.GetComponent<SpriteRenderer>().color = new Color(Color.green.r, Color.green.g, Color.green.b, .25f); ;
                    if (y == 2) node.GetComponent<SpriteRenderer>().color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, .25f);
                    if (y == 3) node.GetComponent<SpriteRenderer>().color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, .25f);
                    if (y == 4) node.GetComponent<SpriteRenderer>().color = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, .25f);
                }
                else
                {
                    node.GetComponent<SpriteRenderer>().color = unplaced;
                }

                nodes[x, y] = node;
            }
        }

        //Player one starts first
        playerOne.setTurn(true);
        playerTwo.setTurn(false);
        playerOne.setColor(colors[Random.Range(0, 5)]);
        playerTwo.setColor(colors[Random.Range(0, 5)]);
	}
	
	void Update () {

        GameObject.Find("Score").GetComponent<TextMesh>().text = ("" + playerOne.getPoints() + " | " + playerTwo.getPoints());

        if (playerOne.getCurrentTurn())
        {
            for (int i = 0; i < 5; i++)
            {
                nodes[0, i].GetComponent<SpriteRenderer>().color = inactive;
            }

            nodes[0, playerOne.getPosition()].GetComponent<SpriteRenderer>().color = playerOne.getColor();

            if (playerOne.getChosen())
            {
                for (int i = 5; i >= 1; i--)
                {
                    GameBlock node = nodes[i, playerOne.getPosition()].GetComponent<GameBlock>(); 
                    
                    //Algorithm step2                                                                                                                                                                                                                                                                                                                                                                                                           //Algorithm step 2
                    if(node.getColor() == unplaced && i > 1)
                    {
                        node.setColor(nodes[0, playerOne.getPosition()].GetComponent<SpriteRenderer>().color); //3
                        GameBlock opponentNode = nodes[12 - i, playerOne.getPosition()].GetComponent<GameBlock>();
                        if (node.getColor() == opponentNode.getColor()) //4
                        {
                            //5
                            node.setColor(unplaced);
                            opponentNode.setColor(unplaced);

                            ///
                            Color colorOne = node.getColor();
                            Color colorTwo = node.getColor();
                            for (int w = 10; w >= 7; w--)
                            {
                                if (w == 12 - i)
                                    break;
                                if (nodes[w, playerOne.getPosition()].GetComponent<GameBlock>().getColor() != unplaced) //1
                                {
                                    colorOne = nodes[w, playerOne.getPosition()].GetComponent<GameBlock>().getColor(); //2
                                    nodes[w, playerOne.getPosition()].GetComponent<GameBlock>().setColor(unplaced);

                                    for (int z = w - 1; z >= 12 - i; z--) //3
                                    {
                                        if (nodes[z, playerOne.getPosition()].GetComponent<GameBlock>().getColor() == unplaced) //4
                                        {
                                            nodes[z, playerOne.getPosition()].GetComponent<GameBlock>().setColor(colorOne);
                                            break;
                                        }
                                        else
                                        {
                                            colorTwo = nodes[z, playerOne.getPosition()].GetComponent<GameBlock>().getColor();
                                            nodes[z, playerOne.getPosition()].GetComponent<GameBlock>().setColor(colorOne);
                                            colorOne = colorTwo;
                                        }
                                    }

                                    break;
                                }
                            }
                            ///

                        }
                        break;
                    }
                    else if(nodes[i, playerOne.getPosition()].GetComponent<SpriteRenderer>().color.a == .25f && i == 1) //2.5
                    {
                        if (nodes[i, playerOne.getPosition()].GetComponent<SpriteRenderer>().color.r != playerOne.getColor().r || nodes[i, playerOne.getPosition()].GetComponent<SpriteRenderer>().color.g != playerOne.getColor().g || nodes[i, playerOne.getPosition()].GetComponent<SpriteRenderer>().color.b != playerOne.getColor().b)
                        {
                            break;
                        }
                        Debug.Log("Row to p1");
                        //Add point P1
                        playerOne.addPoint();

                        //Grey out whole row (completed color)
                        for (int x = 1; x <= 11; x++)
                        {
                            if (x == 6) continue;
                            nodes[x, playerOne.getPosition()].GetComponent<GameBlock>().setColor(completed);
                        }

                        //2.5.a
                        if (playerOne.getPoints() >= 3)
                        {
                            //End game
                            winnerOne = true;
                            Application.LoadLevel(2);
                            Debug.Log("Player one has won.");
                        }
                    }
                    else if(node.getColor() == completed && i == 1)
                    {
                        playerOne.setChosen(false);
                        return;
                    }

                }
                   
                playerOne.resetPlayer(); //6
                playerOne.setTurn(false); //6
                playerTwo.setTurn(true); //6
                for (int i = 0; i < 5; i++)
                {
                    nodes[0, i].GetComponent<SpriteRenderer>().color = inactive;
                }
            }
        } 
        


        else if(playerTwo.getCurrentTurn())
        {
            for (int i = 0; i < 5; i++)
            {
                nodes[12, i].GetComponent<SpriteRenderer>().color = inactive;
            }
            nodes[12, playerTwo.getPosition()].GetComponent<SpriteRenderer>().color = playerTwo.getColor();

            if (playerTwo.getChosen())
            {
                for (int i = 7; i <= 11; i++)
                {
                    GameBlock node = nodes[i, playerTwo.getPosition()].GetComponent<GameBlock>();

                    //Algorithm step2                                                                                                                                                                                                                                                                                                                                                                                                           //Algorithm step 2
                    if (node.getColor() == unplaced && i < 11)
                    {
                        node.setColor(nodes[12, playerTwo.getPosition()].GetComponent<SpriteRenderer>().color); //3
                        GameBlock opponentNode = nodes[12 - i, playerTwo.getPosition()].GetComponent<GameBlock>();
                        if (node.getColor() == opponentNode.getColor()) //4
                        {
                            //5
                            node.setColor(unplaced); 
                            opponentNode.setColor(unplaced);

                            ///
                            Color colorOne = node.getColor();
                            Color colorTwo = node.getColor();
                            for (int w = 2; w <= 5; w++)
                            {
                                if (w == 12 - i)
                                    break;
                                if (nodes[w, playerTwo.getPosition()].GetComponent<GameBlock>().getColor() != unplaced) //1
                                {
                                    colorOne = nodes[w, playerTwo.getPosition()].GetComponent<GameBlock>().getColor(); //2
                                    nodes[w, playerTwo.getPosition()].GetComponent<GameBlock>().setColor(unplaced);

                                    for (int z = w + 1; z <= 12 - i; z++) //3
                                    {
                                        if (nodes[z, playerTwo.getPosition()].GetComponent<GameBlock>().getColor() == unplaced) //4
                                        {
                                            nodes[z, playerTwo.getPosition()].GetComponent<GameBlock>().setColor(colorOne);
                                            break;
                                        }
                                        else
                                        {
                                            colorTwo = nodes[z, playerTwo.getPosition()].GetComponent<GameBlock>().getColor();
                                            nodes[z, playerTwo.getPosition()].GetComponent<GameBlock>().setColor(colorOne);
                                            colorOne = colorTwo;
                                        }
                                    }

                                    break;
                                }
                            }
                            ///
                        }
                        break;
                    }
                    else if (nodes[i, playerTwo.getPosition()].GetComponent<SpriteRenderer>().color.a == .25f && i == 11) //2.5
                    {
                        if (nodes[i, playerTwo.getPosition()].GetComponent<SpriteRenderer>().color.r != playerTwo.getColor().r || nodes[i, playerTwo.getPosition()].GetComponent<SpriteRenderer>().color.g != playerTwo.getColor().g || nodes[i, playerTwo.getPosition()].GetComponent<SpriteRenderer>().color.b != playerTwo.getColor().b)
                        {
                            break;
                        }
                        Debug.Log("Row to p2");
                        //Add point P2
                        playerTwo.addPoint();

                        //Grey out whole row (completed color)
                        for (int x = 1; x <= 11; x++)
                        {
                            if (x == 6) continue;
                            nodes[x, playerTwo.getPosition()].GetComponent<GameBlock>().setColor(completed);
                        }

                        //2.5.a
                        if (playerTwo.getPoints() >= 3)
                        {
                            //End game
                            winnerOne = false;
                            Application.LoadLevel(2);
                            Debug.Log("Player two has won.");
                        }
                    }
                    else if (node.getColor() == completed && i == 11)
                    {
                        playerTwo.setChosen(false);
                        return;
                    }

                }

                playerTwo.resetPlayer(); //6
                playerTwo.setTurn(false); //6
                playerOne.setTurn(true); //6
                for (int i = 0; i < 5; i++)
                {
                    nodes[12, i].GetComponent<SpriteRenderer>().color = inactive;
                }
            }       
        }   
	}

 
}
