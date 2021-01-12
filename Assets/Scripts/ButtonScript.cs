using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public int player;
    
    void Start()
    {
        player = FindInt(GetComponentInParent<PlayerConnectScript>().gameObject.name);
    }

    int FindInt(string stringToSearch)
    {
        char[] vs = stringToSearch.ToCharArray();

        return int.Parse(vs[stringToSearch.Length - 1].ToString()) - 1;
    }

    public void Ready()
    {
        JoinPlayers.instance.Ready(player);
        if (JoinPlayers.instance.isReady[player])
        {
            GetComponent<Image>().color = Color.green;
            GetComponentInChildren<Text>().text = "Prêt";
            for (int i = 0; i < GetComponentInParent<ChooseCar>().GetComponentsInChildren<Button>().Length; i++)
                GetComponentInParent<ChooseCar>().GetComponentsInChildren<Button>()[i].enabled = false;
            GetComponent<Button>().enabled = true;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
            GetComponentInChildren<Text>().text = "Pas encore prêt ...";
            for (int i = 0; i < GetComponentInParent<ChooseCar>().GetComponentsInChildren<Button>(true).Length; i++)
                GetComponentInParent<ChooseCar>().GetComponentsInChildren<Button>()[i].enabled = true;
        }
    }
}
