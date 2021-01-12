using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsWindow, choosePlayers;

    private void Awake()
    {
        settingsWindow.SetActive(false);
        choosePlayers.gameObject.SetActive(false);
    }

    public void StartButton()
    {
        choosePlayers.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartGame(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OpenSettings()
    {
        settingsWindow.SetActive(true);
    }
    
    public void CloseSettings()
    {
        settingsWindow.SetActive(false);
    }

    public void HowManyPlayers(int players)
    {
        ConnectPlayers(players);
    }

    void ConnectPlayers(int players)
    {
        SceneManager.LoadScene("Scene" + players.ToString());
    }
}
