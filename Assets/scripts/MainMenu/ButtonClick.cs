using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour {

    public void OnMouseDown (string buttonName)
    {
        switch (buttonName)
        {
            case "game":
                SceneManager.LoadScene("Level 1");
                break;
            case "options":
                break;
            case "exit":
                Application.Quit();
                break;
            default:
                Debug.Log("uh oh spaghettios");
                break;
        }
    }
}
