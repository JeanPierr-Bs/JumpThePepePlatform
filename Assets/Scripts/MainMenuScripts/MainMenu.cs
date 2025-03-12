using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;

    public string levelSelect;
    public GameObject historyScreen;
    public void NewGame()
    {
        
    }
    public void Continue()
    {
        
    }
    public void OpenHistory()
    {
        historyScreen.SetActive(true);
    }public void ClosedHistory()
    {
        historyScreen.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
