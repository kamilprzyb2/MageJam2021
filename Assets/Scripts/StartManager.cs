using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public int FirstLevel = 1;
    public void StartGame()
    {
        SceneManager.LoadScene(FirstLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
