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

    //A timer variable to delay a bit the AI actions
    float timerBeforeAction = 0f;
    float timerLimit = 2f;

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

    void Update()
    {
        //Check whenever is AI turn to Play
        if (!bPlayer1Turn)
        {
            //Timer Delay before AI takes turn
            timerBeforeAction += Time.deltaTime;
            Debug.Log("Timer Starting: " + timerBeforeAction);
            if (timerBeforeAction >= timerLimit)
            {
                //AI executes the turn
                Debug.Log("Timer Done");
                AITurn(SelectColumnAI());
                timerBeforeAction = 0f;
            }

            
        }
    }

    /*Function to let GameManager know which column is being selected from InputColumn*/
    public void SelectColumn(int column)
    {
        //Check if it is indeed player 1 turn
        Debug.Log("GameManager Column is: " + column);
        if (bPlayer1Turn)
        {
            //Do the Player Turn
            PlayerTurn(column);
        }
       
    }

    void PlayerTurn(int column)
    {
        //First thing to do is check the Board status,
        if (UpdateBoardStatus(column))
        {
            //Player 1 is playing the turn
            if (bPlayer1Turn)
            {
                //Spawn the piece in the right column and then switch to other player
                Instantiate(player1, spawnLocations[column].transform.position, Quaternion.identity);
                bPlayer1Turn = false;
                //Check if Player 1 Wins
                if (DidWin(boardStatus_player1))
                {
                    Debug.LogWarning("Player 1 Wins!!!");
                }
            }
            else
            {
              
            }
        }
    }
    //Function to update the board status of the Game
    bool UpdateBoardStatus(int column)
    {
        //iterate through rows
        for (int row = 0; row < boardHeight; row++)
        {
            //Check if it is empty
            if (boardStatus[column, row] == boardStatus_empty)
            {
                //If Player 1 is the one to update board status
                if (bPlayer1Turn)
                {
                    boardStatus[column, row] = boardStatus_player1;
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

    //Function to retrieve a valid column selection for the AI
    int SelectColumnAI()
    {
        int columnSelected = 0;
        bool bSelected = false; //to check if selected position is a valid one
        //Validate that it is indeed AI turn
        if (!bPlayer1Turn)
        {
            Debug.Log("AI will Select Column");
            if(!bSelected)
            {
                //Random of the selected column
                columnSelected = (int)Random.Range(0, boardLenght);
                Debug.Log("Column Selected");
                //iterate through rows
                for (int row = 0; row < boardHeight; row++)
                {
                    Debug.Log("checking if Empty");
                    //Check if it is empty position
                    if (boardStatus[columnSelected, row] == boardStatus_empty)
                    {
                        Debug.Log("Valid Column Selected");
                        bSelected = true;
                        break;
                    }
                }
            }
            else
            {
                /*Do Nothing*/
            } 
        }
        return columnSelected;
    }
    //Function to manage AI turn
    void AITurn(int column)
    {
        //First thing to do is check the Board status,
        if (UpdateBoardStatus(column))
        {
            //Player AI is playing the turn
            if (!bPlayer1Turn)
            {
                Instantiate(playerAI, spawnLocations[column].transform.position, Quaternion.identity);
                bPlayer1Turn = true;
                //Check if AI Wins
                if (DidWin(boardStatus_AI))
                {
                    Debug.LogWarning("Player AI Wins!!!");
                }
            }
            else
            {
                /*Do Nothing*/
            }
        }

    }
    bool DidWin(int playerNumber)
    {
        //Horizontal Check for a win, -3 is to not worry if only last 3 columns are left, since a connect 4 there would be not possible
        for(int x = 0; x < boardLenght - 3; x++ )
        {
            //Vertical Iteration
            for(int y = 0; y < boardHeight; y++)
            {
                //Check if x positions up to 4 have been assigned to a player number
                if(boardStatus[x,y] == playerNumber && boardStatus[x+1,y] == playerNumber && boardStatus[x + 2, y] == playerNumber && boardStatus[x + 3, y] == playerNumber) 
                {
                    return true;
                }
            }

        }
        //Vertical Check for a win
        for(int x = 0; x < boardLenght; x++)
        {
            for(int y = 0; y < boardHeight - 3; y++)
            {
                //Check if  y positions up to 4 have been assigned to a player number
                if (boardStatus[x,y] == playerNumber && boardStatus[x, y+1] == playerNumber && boardStatus[x, y+2] == playerNumber && boardStatus[x, y+3] == playerNumber)
                {
                    return true;
                }
            }
        }
        //Diagonal Up Check for a Win
        for (int x = 0; x < boardLenght - 3; x++)
        {
            for (int y = 0; y < boardHeight - 3; y++)
            {
                //Check if x and y positions up to 4 have been assigned to a player number
                if (boardStatus[x, y] == playerNumber && boardStatus[x + 1, y + 1] == playerNumber && boardStatus[x + 2, y + 2] == playerNumber && boardStatus[x + 3, y + 3] == playerNumber)
                {
                    return true;
                }
            }
        }
        //Diagonal Down Check for a Win
        for (int x = 0; x < boardLenght - 3; x++)
        {
            for (int y = 0; y < boardHeight - 3; y++)
            {
                //Check if x and y positions up to 4 have been assigned to a player number
                if (boardStatus[x, y + 3] == playerNumber && boardStatus[x + 1, y + 2] == playerNumber && boardStatus[x + 2, y + 1] == playerNumber && boardStatus[x + 3, y] == playerNumber)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
