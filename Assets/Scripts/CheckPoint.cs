using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    LapsCount lapsCount;

    private void Awake()
    {
        lapsCount = GameObject.FindGameObjectWithTag("GameManager").GetComponent<LapsCount>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        lapsCount.checkPointsCount[FindInt(collision.tag)]++;
    }

    int FindInt(string stringToSearch)
    {
        char[] vs = stringToSearch.ToCharArray();

        return int.Parse(vs[stringToSearch.Length - 1].ToString()) - 1;
    }
}
