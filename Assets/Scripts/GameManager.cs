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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        Instantiate(player1, spawnLocations[column].transform.position, Quaternion.identity);
    }

}
