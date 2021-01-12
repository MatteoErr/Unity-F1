using UnityEngine;

public class PassLine : MonoBehaviour
{
    LapsCount lapsCount;

    private void Awake()
    {
        lapsCount = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LapsCount>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lapsCount.wichPlayer = FindInt(collision.tag);

        if (lapsCount.checkPointsCount[lapsCount.wichPlayer] >= lapsCount.checkPoints.Length)
            lapsCount.lapsCount[lapsCount.wichPlayer]++;

        lapsCount.checkPointsCount[lapsCount.wichPlayer] = 0;
        lapsCount.UpdateUI();
    }

    int FindInt(string stringToSearch)
    {
        char[] vs = stringToSearch.ToCharArray();

        return int.Parse(vs[stringToSearch.Length - 1].ToString()) - 1;
    }
}
