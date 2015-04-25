using UnityEngine;
using System.Collections;

public class Game_Setup : MonoBehaviour {

    public int numberOfRows = 5;
    public int cellsPerRow = 5;

    private int totalCells;

    public GameObject pre_ExampleCell;

    private float cellWidth, rowHeight;
    private Camera camera;

	void Start () {
        camera = Camera.main;

        totalCells = ((cellsPerRow * 2) + 3);
        //In pixels (screen)
        cellWidth = camera.pixelWidth / totalCells; //Additional three account for left and right finish line and center deadspace
        rowHeight = (camera.pixelHeight - 100) / (numberOfRows);


        Vector2 cellScale = new Vector2(5, 5);

        pre_ExampleCell.transform.localScale = cellScale;

        for(int x = 0; x < numberOfRows; x++) {
            for (int y = 0; y < totalCells; y++)
            {
                GameObject node = (GameObject) Instantiate(pre_ExampleCell, camera.ScreenToWorldPoint(new Vector3(cellWidth * y + cellWidth - (cellWidth / 2), (rowHeight * x) + rowHeight - (rowHeight / 2), 1)), pre_ExampleCell.transform.localRotation);
                if(y == 6)
                    node.GetComponent<SpriteRenderer>().color = Color.gray;
                if (y == 1 || y == 11)
                {
                    if (x == 0) node.GetComponent<SpriteRenderer>().color = new Color(Color.red.r, Color.red.g, Color.red.b, .25f);
                    if (x == 1) node.GetComponent<SpriteRenderer>().color = new Color(Color.green.r, Color.green.g, Color.green.b, .25f); ;
                    if (x == 2) node.GetComponent<SpriteRenderer>().color = new Color(Color.blue.r, Color.blue.g, Color.blue.b, .25f);
                    if (x == 3) node.GetComponent<SpriteRenderer>().color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, .25f);
                    if (x == 4) node.GetComponent<SpriteRenderer>().color = new Color(Color.cyan.r, Color.cyan.g, Color.cyan.b, .25f);
                }
                if(y == 0 || y == 12)
                {
                    node.GetComponent<SpriteRenderer>().color = new Color(Color.white.r, Color.white.g, Color.white.b, .25f);
                }
            }
        }

	}
	
	void Update () {
	
	}
}
