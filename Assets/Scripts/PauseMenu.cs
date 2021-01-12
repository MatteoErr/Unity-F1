using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.UI;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{
    public static bool[] isReady = new bool[2];
    public bool gameIsPaused = false, gameIsStarted = false;
    public GameObject[] pauseMenuUI, settingsWindow, readyWindow, firstButton, firstSettingsButton;
    List<MultiplayerEventSystem> multiplayerEventSystems = new List<MultiplayerEventSystem>();
    public static PauseMenu instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Il y a plus d'une instance de PauseMenu dans la scène !");
            return;
        }
        instance = this;
    }

    public void PauseOrResume(int player)
    {
       if (gameIsPaused)
            Resume(player);
       else
            Paused();
    }
    
    void Paused()
    {
        if (Time.timeScale == 0)
            return;
        
        for (int i = 0; i < pauseMenuUI.Length; i++)
            pauseMenuUI[i].SetActive(true);
        Time.timeScale = 0;
        gameIsPaused = true;

        for (int i = 0; i < multiplayerEventSystems.Count; i++)
        {
            multiplayerEventSystems[i].SetSelectedGameObject(null);
            multiplayerEventSystems[i].SetSelectedGameObject(firstButton[i]);
            GameObject.FindGameObjectWithTag("Player" + (i + 1)).GetComponent<PlayerMovement>().enabled = false;
        }
        
    }

    public void Resume(int index)
    {
        readyWindow[index].SetActive(!readyWindow[index].activeSelf);
        isReady[index] = readyWindow[index].activeSelf;

        if (isReady[0] && isReady[1])
        {
            for (int i = 0; i < isReady.Length; i++)
            {
                pauseMenuUI[i].SetActive(false);
                readyWindow[i].SetActive(false);
                Time.timeScale = 1;
                GameObject.FindGameObjectWithTag("Player" + (i + 1)).GetComponent<PlayerMovement>().enabled = true;
                gameIsPaused = false;
                isReady[i] = false;
            }
        }
    }

    public void LoadMainMenu()
    {
        Resume(0);
        Resume(1);
        SceneManager.LoadScene("MainMenu");
    }

    public void SettingsButton(int index)
    {
        settingsWindow[index].SetActive(true);
        multiplayerEventSystems[index].SetSelectedGameObject(null);
        multiplayerEventSystems[index].SetSelectedGameObject(firstSettingsButton[index]);
    }

    public void CloseSettingsWindow(int index)
    {
        settingsWindow[index].SetActive(false);
        multiplayerEventSystems[index].SetSelectedGameObject(null);
        multiplayerEventSystems[index].SetSelectedGameObject(firstButton[index]);
    }

    public void AddEventSystem(MultiplayerEventSystem multiplayerEventSystem)
    {
        multiplayerEventSystems.Add(multiplayerEventSystem);
    }
}
