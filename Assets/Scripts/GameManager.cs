using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Game objects of the Player and AI
    public GameObject player1;
    public GameObject playerAI;

    //The values representing the length and height of the board
    int boardHeight = 6;
    int boardLenght = 7;

    //2D array to store board status
    int[,] boardStatus;// 0 is empty, 1 is player1, 2 is AI
    int boardStatus_empty = 0;
    int boardStatus_player1 = 1;
    int boardStatus_AI = 2;

    //Array to handle all spawn locations for the pieces
    public GameObject[] spawnLocations;

    //Default player turn will be Player 1
    bool bPlayer1Turn = true;

    // Start is called before the first frame update
    void Start()
    {
        //Set the Board Status with the board length and height at the start to prevent null reference
        boardStatus = new int[boardLenght, boardHeight];
    }

    /*Function to let GameManager know which column is being selected from InputColumn*/
    public void SelectColumn(int column)
    {
        Debug.Log("GameManager Column is: " + column);
        PlayTurn(column);
    }

    void PlayTurn(int column)
    {
        //First thing to do is check the Board status,
        if(UpdateBoardStatus(column))
        {
            //Player 1 is playing the turn
            if (bPlayer1Turn)
            {
                //Spawn the piece in the right column and then switch to other player
                Instantiate(player1, spawnLocations[column].transform.position, Quaternion.identity);
                bPlayer1Turn = false;
            }
            else
            {
                /*TODO Here goes the AI behaviour, in the meantime for test, will be like player 2 */
                //Spawn the piece in the right column and then switch to other player
                Instantiate(playerAI, spawnLocations[column].transform.position, Quaternion.identity);
                bPlayer1Turn = true;
            }
        }
        
        
    }

    bool UpdateBoardStatus(int column)
    {
        //iterate through rows
        for(int row = 0; row < boardHeight; row++)
        {
            //Check if it is empty
            if(boardStatus[column,row] == boardStatus_empty)
            {
                //If Player 1 is the one to update board status
                if(bPlayer1Turn)
                {
                    boardStatus[column,row] = boardStatus_player1;
                }
                //If AI is the one to update board status
                else
                {
                    boardStatus[column, row] = boardStatus_AI;
                }
                Debug.Log("Piece being spawned at ( " + column + ", " + row + ")");
                return true;
            }
            
        }
        Debug.LogWarning("Column " + column + " is full");
        return false;
    }

}
