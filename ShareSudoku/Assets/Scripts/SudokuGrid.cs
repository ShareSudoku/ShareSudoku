using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SudokuGrid : MonoBehaviour
{

    public int columns = 0;
    public int rows = 0;

    public float squareOffset = 0.0f;
    public float squareScale = 1.0f;
    public float squareGap = 0.1f;
    public Color lineHighlightCol = Color.green;

    public GameObject gridSquare;

    public Vector2 startPosition = new Vector2(0.0f, 0.0f);

    private List<GameObject> gridSquares = new List<GameObject> { };
    private int selectedGridData = -1;

    private void SpawnGridSquares()
    {
        int squareIndex = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int collumn = 0; collumn < columns; collumn++)
            {
                gridSquares.Add(Instantiate(gridSquare) as GameObject);
                gridSquares[gridSquares.Count - 1].GetComponent<GridSquare>().SetSquareIndex(squareIndex);
                gridSquares[gridSquares.Count - 1].transform.parent = this.transform;
                gridSquares[gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);

                squareIndex++;
            }
        }
    }

    private void SetSquarePos()
    {
        var squareRect = gridSquares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        Vector2 squareGapNum = new Vector2(0.0f, 0.0f);
        bool rowMoved = false;

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
                squareGapNum.x = 0;
                rowMoved = false;
            }

            var posXoffset = offset.x * columnNum + (squareGapNum.x * squareGap);
            var posYoffset = offset.y * rowNum + (squareGapNum.y * squareGap);

            if (columnNum > 0 && columnNum % 3 == 0)
            {
                squareGapNum.x++;
                posXoffset += squareGap;

            }
            if (rowNum > 0 && rowNum % 3 == 0 && rowMoved == false)
            {
                rowMoved = true;
                squareGapNum.y++;
                posYoffset += squareGap;
            }

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
        {
            gridSquares[i].GetComponent<GridSquare>().SetNumber(data.unsolvedData[i]);
            gridSquares[i].GetComponent<GridSquare>().SetCorrectNum(data.solvedData[i]);
            gridSquares[i].GetComponent<GridSquare>().SetSquareHasDefaultValue(data.unsolvedData[i] != 0 && data.unsolvedData[i] == data.solvedData[i]);
        }
    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquarePos();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (gridSquare.GetComponent<GridSquare>() == null)
            Debug.LogError("This game object needs to have the GridSquare script attached!");

        CreateGrid();

        if (GameSettings.gsInstance.GetLoadPrevGame())
            LoadGrid();
        else
            SetGridNumber(GameSettings.gsInstance.GetGameMode());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadGrid()
    {
        string level = GameSettings.gsInstance.GetGameMode();
        selectedGridData = Configuration.ReadGameBoardLevel();
        var data = Configuration.ReadGridData();

        SetGridSquareData(data);
        LoadGridNotes(Configuration.GetGridNotes());
    }

    private void LoadGridNotes(Dictionary<int, List<int>> notes)
    {
        foreach(var note in notes)
        {
            gridSquares[note.Key].GetComponent<GridSquare>().SetGridNotes(note.Value);
        }
    }

    private void OnEnable()
    {
        GameEvents.UpdateSelectedSquare += OnSquareSelected;
        GameEvents.OnCheckBoard += CheckBoardCompleted;
    }

    private void OnDisable()
    {
        GameEvents.UpdateSelectedSquare -= OnSquareSelected;
        GameEvents.OnCheckBoard -= CheckBoardCompleted;

        var solvedData = SudokuData.Instance.sudokuGame[GameSettings.gsInstance.GetGameMode()][selectedGridData].solvedData;
        int[] unsolvedData = new int[81];
        Dictionary<string, List<string>> gridNotes = new Dictionary<string, List<string>>();

        for(int i = 0; i < gridSquares.Count; i++)
        {
            var component = gridSquares[i].GetComponent<GridSquare>();
            unsolvedData[i] = component.GetSqNum();

            string key = "squareNote:" + i.ToString();
            gridNotes.Add(key, component.GetSquareNotes());
        }

        SudokuData.SudokuBoardData currentData = new SudokuData.SudokuBoardData(unsolvedData, solvedData);

        if (GameSettings.gsInstance.GetExitOnWin() == false) //don't save the game data if a player exits the game upon winning
            Configuration.SaveboardData(currentData, 
                GameSettings.gsInstance.GetGameMode(),
                selectedGridData, 
                PlayerLives.lInstance.GetErrorNums(), 
                gridNotes);
        else
            Configuration.DeleteDataFile();

        GameSettings.gsInstance.SetExitOnWin(false);
    }

    private void SetSquareColors(int[] data, Color col)
    {
        foreach(var i in data)
        {
            var comp = gridSquares[i].GetComponent<GridSquare>();
            if (comp.IsSelected() == false && comp.HasWrongValue() == false)
            {
                comp.SetSquareColour(col);
            }
        }
    }

    public void OnSquareSelected(int squareIndex)
    {
        var horizLine = HighlightLine.instanceH.GetHorizontalLine(squareIndex);
        var verticalLine = HighlightLine.instanceH.GetVerticalLine(squareIndex);
        var square = HighlightLine.instanceH.GetSquare(squareIndex);

        SetSquareColors(HighlightLine.instanceH.GetAllSquareIndexes(), Color.white);
        SetSquareColors(horizLine, lineHighlightCol);
        SetSquareColors(verticalLine, lineHighlightCol);
        SetSquareColors(square, lineHighlightCol);
    }

    private void CheckBoardCompleted()
    {
        foreach(var square in gridSquares)
        {
            var comp = square.GetComponent<GridSquare>();
            if (comp.IsCorrect() == false)
            {
                return;
            }
        }
        GameEvents.OnBoardCompletedMethod();
    }

    public void CheckSolvedSudoku()
    {
        foreach(var square in gridSquares)
        {
            var comp = square.GetComponent<GridSquare>();
            comp.SetCorrectNum();
        }
        CheckBoardCompleted();
    }
}
