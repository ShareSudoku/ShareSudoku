using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;

public class Configuration : MonoBehaviour
{
#if UNITY_ANDROID && !UNITY_EDITOR
    private static string directory = Application.persistentDataPath;
#else
    private static string directory = Directory.GetCurrentDirectory();
#endif

    static string file = @"\boardData.ini";
    static string fPath = directory + file;

    public static void DeleteDataFile()
    {
        File.Delete(fPath);
    }

    public static void SaveboardData(SudokuData.SudokuBoardData boardData, string level, int boardIndex, int errorNumber,
        Dictionary<string, List <string>> boardNotes)
    {
        File.WriteAllText(fPath, string.Empty);
        StreamWriter writer = new StreamWriter(fPath, false);
        string cTime = "#time:" + GameTimer.GetTime();
        string levelDf = "#level:" + level;
        string errorNum = "#errors:" + errorNumber;
        string boardInd = "#boardIndex:" + boardIndex.ToString();
        string unsolved = "#unsolved:";
        string solved = "#solved:";

        foreach(var unsolvedData in boardData.unsolvedData)
        {
            unsolved += unsolvedData.ToString() + ",";

        }
        foreach (var solvedData in boardData.solvedData)
        {
            solved +=solvedData.ToString() + ",";

        }
        
        writer.WriteLine(cTime);
        writer.WriteLine(levelDf);
        writer.WriteLine(errorNum);
        writer.WriteLine(boardInd);
        writer.WriteLine(unsolved);
        writer.WriteLine(solved);

        foreach(var square in boardNotes)
        {
            string bNotes = "#" + square.Key + ":";
            bool save = false;

            foreach(var note in square.Value)
            {
                if(note != " ")
                {
                    bNotes += note + ",";
                    save = true;
                }
            }
            if (save) writer.WriteLine(bNotes);
        }

        writer.Close();
    } 

    public static Dictionary<int, List<int>> GetGridNotes()
    {
        Dictionary<int, List<int>> gridNotes = new Dictionary<int, List<int>>();
        string line;

        StreamReader file = new StreamReader(fPath);

        while((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#squareNote")
            {
                int squareIndex = -1;
                List<int> notes = new List<int>();

                int.TryParse(word[1], out squareIndex);
                string[] subString = Regex.Split(word[2], ",");

                foreach(var note in subString)
                {
                    int noteNum = -1;
                    int.TryParse(note, out noteNum);
                    if (noteNum > 0)
                        notes.Add(noteNum);
                }
                    gridNotes.Add(squareIndex, notes);
            }

        }
        file.Close();

        return gridNotes;
    }

    public static string ReadBoardLevel()
    {
        string line;
        string level = "";
        StreamReader file = new StreamReader(fPath);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#level")
            {
                level = word[1];
            }
        }
        file.Close();

        return level;
    }

    public static SudokuData.SudokuBoardData ReadGridData()
    {
        string line;
        StreamReader file = new StreamReader(fPath);

        int[] unsolvedData = new int[81];
        int[] solvedData = new int[81];

        int unsolvedIndex = 0;
        int solvedIndex = 0;

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#unsolved")
            {
                string[] subStrings = Regex.Split(word[1], ",");

                foreach (var value in subStrings)
                {
                    int squareNum = -1;
                    if (int.TryParse(value, out squareNum)) 
                    {
                        unsolvedData[unsolvedIndex] = squareNum;
                        unsolvedIndex++;
                    }

                }
            }
            if (word[0] == "#solved")
            {
                string[] subStrings = Regex.Split(word[1], ",");

                foreach (var value in subStrings)
                {
                    int squareNum = -1;
                    if (int.TryParse(value, out squareNum)) 
                    {
                        solvedData[unsolvedIndex] = squareNum;
                        solvedIndex++;
                    }

                }
            }
        }
        file.Close();

        return new SudokuData.SudokuBoardData(unsolvedData, solvedData);
    }

    public static int ReadGameBoardLevel()
    {
        int level = -1;
        string line;

        StreamReader file = new StreamReader(fPath);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#boardIndex")
            {
                int.TryParse(word[1], out level);
            }
        }
        file.Close();
        return level;
    }

    public static float ReadGameTime()
    {
        float time = -1.0f;
        string line;

        StringReader file = new StringReader(fPath);
        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#time")
            {
                float.TryParse(word[1], out time);
            }
        }
        file.Close();
        return time;
    }
    public static int ReadErrorNum()
    {
        int errorNum = 0;
        string line;

        StreamReader file = new StreamReader(fPath);

        while ((line = file.ReadLine()) != null)
        {
            string[] word = line.Split(':');
            if (word[0] == "#errors")
            {
                int.TryParse(word[1], out errorNum);
            }
        }
        file.Close();
        return errorNum;
    }

    public static bool CheckDataFile()
    {
        return File.Exists(fPath);
    }
}
