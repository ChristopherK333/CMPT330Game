using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonScript : MonoBehaviour
{
    public void ExitButton()
    {
        // exits the game
        Debug.Log("Exit button clicked!");
        Application.Quit();
    }
}
