using UnityEngine;

public class FirstCheckPoint : MonoBehaviour
{
    LapsCount lapsCount;

    private void Awake()
    {
        lapsCount = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LapsCount>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (lapsCount.checkPointsCount[lapsCount.wichPlayer] > 2)
            lapsCount.checkPointsCount[lapsCount.wichPlayer] = 0;
    }
}
