using UnityEngine;
using UnityEngine.UI;

public class LapsCount : MonoBehaviour
{
    public Text[] lapsCountText;
    JoinPlayers joinPlayers;
    public int numberOfLaps, wichPlayer;
    public int[] lapsCount, checkPointsCount;
    public Collider2D[] checkPoints;

    private void Awake()
    {
        checkPoints = GameObject.FindGameObjectWithTag("CheckPoints").GetComponentsInChildren<BoxCollider2D>();
        joinPlayers = GetComponent<JoinPlayers>();
        lapsCount = new int[joinPlayers.maxPlayersInThisScene];
        checkPointsCount = new int[joinPlayers.maxPlayersInThisScene];
    }

    public void UpdateUI()
    {
        lapsCountText[wichPlayer].text = lapsCount[wichPlayer].ToString() + " / " + numberOfLaps;
    }
}
