using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGrid : MonoBehaviour
{

    public int columns = 0;
    public int rows = 0;

    public float squareOffset = 0.0f;
    public float squareScale = 1.0f;

    public GameObject gridSquare;

    public Vector2 startPosition = new Vector2(0.0f, 0.0f);

    private List<GameObject> gridSquares = new List<GameObject> { };
    private int selectedGridData = -1;

    private void SpawnGridSquares()
    {
        for (int row = 0; row < rows; row++)
        {
            for (int collumn = 0; collumn < columns; collumn++)
            {
                gridSquares.Add(Instantiate(gridSquare) as GameObject);
                gridSquares[gridSquares.Count - 1].transform.parent = this.transform;
                gridSquares[gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
            }
        }
    } 

    private void SetSquarePos()
    {
        var squareRect = gridSquares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        offset.x = squareRect.rect.width * squareRect.transform.localScale.x + squareOffset;
        offset.y = squareRect.rect.height * squareRect.transform.localScale.y + squareOffset;

        int columnNum = 0;
        int rowNum = 0;

        foreach (GameObject square in gridSquares)
        {
            if (columnNum + 1 > columns)
            {
                rowNum++;
                columnNum = 0;
            }

            var posXoffset = offset.x * columnNum;
            var posYoffset = offset.y * rowNum;


            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPosition.x + posXoffset, startPosition.y - posYoffset);
            columnNum++;
        }
    }

    private void SetGridNumber(string level)
    {
        /*foreach (var square in gridSquares)
        {
            square.GetComponent<GridSquare>().SetNumber(Random.Range(0, 10));
        }*/
        selectedGridData = Random.Range(0, SudokuData.Instance.sudokuGame[level].Count);

        var data = SudokuData.Instance.sudokuGame[level][selectedGridData];
        SetGridSquareData(data);
    }

    private void SetGridSquareData(SudokuData.SudokuBoardData data)
    {
        for (int i = 0; i < gridSquares.Count; i++)
            gridSquares[i].GetComponent<GridSquare>().SetNumber(data.unsolvedData[i]);
    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquarePos();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(gridSquare.GetComponent<GridSquare>() == null)
            Debug.LogError("This game object needs to have the GridSquare script attached!");

        CreateGrid();
        SetGridNumber(DifficultySettings.difInstance.GetGameMode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
