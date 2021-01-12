using System.Collections;
using UnityEngine;

public class OutOfCircuit : MonoBehaviour
{
    LapsCount lapsCount;
    public Transform startPoint;
    Vector3[] startPointOffset;
    GameObject[] players;
    public float timeBeforeDie = 0.5f;

    bool[] isOutOfWay;
    int playerConcerned;

    private void Start()
    {
        lapsCount = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LapsCount>();

        InitiateVariables();
        for (int i = 0; i < players.Length; i++)
        {
            players[i] = GameObject.FindGameObjectWithTag("Player" + (i + 1));
        }


        for (int i = 0; i < isOutOfWay.Length; i++)
        {
            isOutOfWay[i] = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerConcerned = FindInt(collision.tag);
        isOutOfWay[playerConcerned] = true;
        StartCoroutine(WaitToDie(timeBeforeDie, collision.tag));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerConcerned = FindInt(collision.tag);
        StopCoroutine(WaitToDie(timeBeforeDie, collision.tag));
        isOutOfWay[playerConcerned] = false;
    }

    IEnumerator WaitToDie(float timeToWait, string tag)
    {
        yield return new WaitForSeconds(timeToWait);
        playerConcerned = FindInt(tag);
        if (isOutOfWay[playerConcerned])
        {
            players[playerConcerned].transform.position = startPoint.position + 4 * startPointOffset[playerConcerned];
            players[playerConcerned].transform.rotation = new Quaternion(0f, 0f, 0f, 1f);
            //GameObject.FindGameObjectWithTag("Player" + (playerConcerned + 1)).GetComponent<>
            lapsCount.checkPointsCount[playerConcerned] = 0;
            isOutOfWay[playerConcerned] = false;
        }
    }

    int FindInt(string strtingToSearch)
    {
        char[] vs = strtingToSearch.ToCharArray();

        return int.Parse(vs[vs.Length - 1].ToString()) - 1;
    }
    
    private void InitiateVariables()
    {
        if (GameObject.FindGameObjectWithTag("Player4"))
        {
            players = new GameObject[4];
            isOutOfWay = new bool[4];
            startPointOffset = new Vector3[4];
        }
        else if (GameObject.FindGameObjectWithTag("Player3"))
        {
            players = new GameObject[3];
            isOutOfWay = new bool[3];
            startPointOffset = new Vector3[3];
        }
        else if (GameObject.FindGameObjectWithTag("Player2"))
        {
            players = new GameObject[2];
            isOutOfWay = new bool[2];
            startPointOffset = new Vector3[2];
        }
        else if (GameObject.FindGameObjectWithTag("Player1"))
        {
            players = new GameObject[1];
            isOutOfWay = new bool[1];
            startPointOffset = new Vector3[1];
        }

        for (int i = 0; i < startPointOffset.Length; i++)
        {
            startPointOffset[i] = new Vector2((i - startPointOffset.Length / 2), 0);
            if (startPointOffset[i].x >= 0)
                startPointOffset[i].x++;
        }
    }

}
