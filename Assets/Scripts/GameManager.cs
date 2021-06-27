using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Game objects of the Player and AI
    public GameObject player1;
    public GameObject playerAI;

    //Array to handle all spawn locations for the pieces
    public GameObject[] spawnLocations;

    //Default player turn will be Player 1
    bool bPlayer1Turn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*Function to let GameManager know which column is being selected from InputColumn*/
    public void SelectColumn(int column)
    {
        Debug.Log("GameManager Column is: " + column);
        PlayTurn(column);
    }

    void PlayTurn(int column)
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
