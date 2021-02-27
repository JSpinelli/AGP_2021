using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct GoData
{
    public char typeOfObject;
    public List<Vector2> blobItBelongs;
}

public class GoSolver : MonoBehaviour
{
    public TextAsset[] myGoBoards;

    // Start is called before the first frame update
    public GameObject boardSlot;
    public GameObject blackChip;
    public GameObject whiteChip;
    private GoData[,] board;
    private int width;
    private int height;

    private List<List<Vector2>> blackBlobs = new List<List<Vector2>>();
    private List<List<Vector2>> whiteBlobs = new List<List<Vector2>>();

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
                List<Vector2> myBlob = CheckMyself(j, i);
                if (myBlob != null)
                {
                    CheckNeighbors(j, i, myBlob);
                }
            }
        }

        Debug.Log("Number of White Blobs: " + whiteBlobs.Count);
        Debug.Log("Number of Black Blobs: " + blackBlobs.Count);
    }

    public List<Vector2> CheckMyself(int j, int i)
    {
        if (board[j, i].typeOfObject != ' ')
        {
            if (board[j, i].blobItBelongs == null)
            {
                var newList = new List<Vector2>();
                newList.Add(new Vector2(j, i));
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

            return board[j, i].blobItBelongs;
        }

        return null;
    }

    public void CheckNeighbors(int j, int i, List<Vector2> myBlob)
    {
        if (j + 1 < width)
        {
            if (board[j, i].typeOfObject == board[j + 1, i].typeOfObject)
            {
                myBlob.Add(new Vector2(j + 1, i));
                board[j + 1, i].blobItBelongs = myBlob;
            }
        }

        if (i + 1 < height)
        {
            if (board[j, i].typeOfObject == board[j, i + 1].typeOfObject)
            {
                myBlob.Add(new Vector2(j, i + 1));
                board[j, i + 1].blobItBelongs = myBlob;
            }
        }
    }
}