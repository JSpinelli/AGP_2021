using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

struct GoData
{
    public char typeOfObject;
    public int blobItBelongs;
}
public class GoSolver : MonoBehaviour
{
    public TextAsset[] myGoBoards;
    // Start is called before the first frame update

    private GoData[,] board;
    private int width;
    private int height;
    
    void Start()
    {
        string currentBoard = myGoBoards[0].text;
        width = 0;
        height = 0;
        for (int i = 0; i < currentBoard.Length; i++)
        {
            
            if (currentBoard[i] == '\n')
            {
                height++;
            }
            else
            {
                width++;
            }
        }
        board = new GoData[width, height];
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
                board[wIndex, hIndex].typeOfObject = currentBoard[i];
                wIndex++;
            }
        }
    }
}
