using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputColumn : MonoBehaviour
{
    //The column id from 0 to 6 
    public int column;

    //GameManager reference
    public GameManager gameManager;

    /*When function is called it calls to the GameManager Interface to let it know which column is selected*/
    void OnMouseDown()
    {
        //Call to SelectColumn function of GameManager
        gameManager.SelectColumn(column);
    }

}
