using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class GoSolver : MonoBehaviour
{
    public struct Coord
    {
        public int x;
        public int y;
    }
    
   public  struct GoData
    {
        public char typeOfObject;
        public List<Coord> blobItBelongs;
    }

    public TextAsset[] myGoBoards;

    // Start is called before the first frame update
    public GameObject boardSlot;
    public GameObject blackChip;
    public GameObject whiteChip;

    public GameObject nextWhiteChip;
    public GameObject nextBlackChip;
    
    private GoData[,] board;
    private int width;
    private int height;

    private List<List<Coord>> blackBlobs = new List<List<Coord>>();
    private List<List<Coord>> whiteBlobs = new List<List<Coord>>();

    void Start()
    {
        string currentBoard = myGoBoards[0].text;
        width = 0;
        height = 0;
        bool foundLine = false;
        for (int i = 0; i < currentBoard.Length; i++)
        {
            if (currentBoard[i] == '\n')
            {
                height++;
                foundLine = true;
            }
            else if (!foundLine)
            {
                width++;
            }
        }

        board = new GoData[width, height];
        Debug.Log("Width: " + width);
        Debug.Log("Height: " + height);
        int wIndex = 0;
        int hIndex = 0;
        for (int i = 0; i < currentBoard.Length; i++)
        {
            if (currentBoard[i] == '\n')
            {
                hIndex++;
                wIndex = 0;
            }
            else
            {
                if (hIndex < height)
                {
                    board[wIndex, hIndex].typeOfObject = currentBoard[i];
                    board[wIndex, hIndex].blobItBelongs = null;
                }

                wIndex++;
            }
        }

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (i != height - 1 && j != width - 1)
                    Instantiate(boardSlot, new Vector3(j + 0.5f, -i - 0.5f, 0f), Quaternion.identity);
                List<Coord> myBlob = CheckMyself(j, i);
                if (myBlob != null)
                {
                    CheckNeighbors(j, i, myBlob);
                }
            }
        }

        Debug.Log("Number of White Blobs: " + whiteBlobs.Count);
        Debug.Log("Number of Black Blobs: " + blackBlobs.Count);

        foreach (var whiteBlob in whiteBlobs)
        {
            CheckBlob(whiteBlob);
        }        
        
        foreach (var blackBlob in blackBlobs)
        {
            CheckBlob(blackBlob);
        }
    }

    public void CheckBlob(List<Coord> currentBlob)
    {
        List<Coord> emptySpaces = new List<Coord>();
        foreach (var piece in currentBlob)
        {
            if (piece.x - 1 >= 0)
            {
                CheckEmpty(piece.x-1,piece.y, emptySpaces);
            }
            if (piece.x + 1 < width)
            {
                CheckEmpty(piece.x+1,piece.y, emptySpaces);
            }
            if (piece.y - 1 >= 0)
            {
                CheckEmpty(piece.x,piece.y-1, emptySpaces);
            }
            if (piece.y + 1 < height)
            {
                CheckEmpty(piece.x,piece.y+1, emptySpaces);
            }
        }

        if (emptySpaces.Count == 1)
        {
            Coord firstPoint = currentBlob[0];
            Instantiate(
                board[firstPoint.x, firstPoint.y].typeOfObject == 'W' ? nextBlackChip : nextWhiteChip,
                new Vector3(emptySpaces[0].x, -emptySpaces[0].y, -1f),
                Quaternion.identity);
        }
    }

    public void CheckEmpty(int x, int y, List<Coord> emptySpaces)
    {                    
        if (board[x, y].typeOfObject == ' ')
        {
            Coord point = new Coord();
            point.x = x;
            point.y = y;
            if (!emptySpaces.Exists(match => match.x == point.x && match.y == point.y))
            {
                emptySpaces.Add(point);
            }
        }
    }

    public List<Coord> CheckMyself(int j, int i)
    {
        if (board[j, i].typeOfObject != ' ')
        {
            if (board[j, i].blobItBelongs == null)
            {
                var newList = new List<Coord>();
                Coord point = new Coord();
                point.x = j;
                point.y = i;
                newList.Add(point);
                if (board[j, i].typeOfObject == 'W')
                {
                    
                    Instantiate(whiteChip, new Vector3(j, -i, -1f), Quaternion.identity);
                    whiteBlobs.Add(newList);
                    return newList;
                }

                Instantiate(blackChip, new Vector3(j, -i, -1f), Quaternion.identity);
                blackBlobs.Add(newList);
                return newList;
            }
            Instantiate( board[j, i].typeOfObject == 'W'? whiteChip: blackChip, new Vector3(j, -i, -1f), Quaternion.identity);
            return board[j, i].blobItBelongs;
        }

        return null;
    }

    public void CheckNeighbors(int j, int i, List<Coord> myBlob)
    {
        if (j + 1 < width)
        {
            if (board[j, i].typeOfObject == board[j + 1, i].typeOfObject)
            {
                Coord point = new Coord();
                point.x = j+1;
                point.y = i;
                myBlob.Add(point);
                board[j + 1, i].blobItBelongs = myBlob;
            }
        }

        if (i + 1 < height)
        {
            if (board[j, i].typeOfObject == board[j, i + 1].typeOfObject)
            {
                Coord point = new Coord();
                point.x = j;
                point.y = i+1;
                myBlob.Add(point);
                board[j, i + 1].blobItBelongs = myBlob;
            }
        }
    }
}