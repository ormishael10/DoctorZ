using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    public void MoveToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void QuitGAme()
    {
        Application.Quit();
    }

}
