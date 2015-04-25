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
        rowHeight = camera.pixelHeight / (numberOfRows);


        Vector2 cellScale = new Vector2(5, 5);

        pre_ExampleCell.transform.localScale = cellScale;

        for(int x = 0; x < numberOfRows; x++) {
            for (int y = 0; y < totalCells; y++)
            {
                Instantiate(pre_ExampleCell, camera.ScreenToWorldPoint(new Vector3(cellWidth * y + cellWidth - (cellWidth / 2), (rowHeight * x) + rowHeight - (rowHeight / 2), 1)), pre_ExampleCell.transform.localRotation);
            }
        }

	}
	
	void Update () {
	
	}
}
