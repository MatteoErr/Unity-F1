using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JoinPlayers : MonoBehaviour
{
    public int playersJoined = 0, maxPlayersInThisScene;
    public static JoinPlayers instance;
    [SerializeField]
    GameObject playerJoinPrefab, connectPlayers;
    [SerializeField]
    Color[] playerColors;
    public GameObject[] buttonsToSelect;
    public bool[] isReady;
    ChooseCar[] chooseCars;
    
    private void Awake()
    {
        Time.timeScale = 0;
        connectPlayers.SetActive(true);

        for (int i = 0; i < maxPlayersInThisScene; i++)
            GameObject.FindGameObjectWithTag("Player" + (i + 1)).GetComponent<PlayerMovement>().enabled = false;

        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance JoinPlayers dans la scène !");
            return;
        }
        instance = this;
        buttonsToSelect = new GameObject[maxPlayersInThisScene];
        isReady = new bool[maxPlayersInThisScene];
        for (int i = 0; i < isReady.Length; i++)
            isReady[i] = false;
        ConnectPlayers(maxPlayersInThisScene);
    }

    private void Update()
    {
         if (playersJoined == maxPlayersInThisScene && CheckIfReady())
            StartCoroutine(AllPlayersConnectedAndReady());
    }

    public void OnPlayerJoined()
    {
        playersJoined++;
        Debug.Log("Player " + playersJoined + " joined !");
        GameObject.Find("PlayerConnection" + playersJoined).GetComponent<Image>().color = playerColors[playersJoined - 1];
    }

    IEnumerator AllPlayersConnectedAndReady()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        for (int i = 0; i < maxPlayersInThisScene; i++)
        {
            chooseCars[i].ChangeTag();
            GameObject.FindGameObjectWithTag("Player" + (i + 1)).GetComponent<PlayerMovement>().enabled = true;
        }
        connectPlayers.SetActive(false);
        Time.timeScale = 1;
        PauseMenu.instance.gameIsStarted = true;
    }

    void ConnectPlayers(int players)
    {
        GameObject connectPlayersPanel = connectPlayers.GetComponentInChildren<HorizontalLayoutGroup>().gameObject;

        for (int i = 0; i < connectPlayersPanel.transform.childCount; i++)
        {
            Destroy(connectPlayersPanel.transform.GetChild(i).gameObject);
        }

        chooseCars = new ChooseCar[players];
        for (int i = 0; i < players; i++)
        {
            GameObject playerPlate = Instantiate(playerJoinPrefab, connectPlayersPanel.transform);
            PlayerConnectScript playerConnect = playerPlate.GetComponent<PlayerConnectScript>();
            playerConnect.playerName = "Joueur " + (i + 1);
            playerPlate.name = "PlayerConnection" + (i + 1);
            buttonsToSelect[i] = playerPlate.GetComponentInChildren<Button>().gameObject;
            chooseCars[i] = playerPlate.GetComponentInChildren<ChooseCar>();
        }
    }

    public bool CheckIfReady()
    {
        bool yes = true;
        for (int i = 0; i < isReady.Length; i++)
            if (!isReady[i])
                yes = false;
        return yes;
    }

    public void Ready(int index)
    {
        isReady[index] = !isReady[index];
    }
}
