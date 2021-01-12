using UnityEngine;

public class EnableMiniMapIcons : MonoBehaviour
{
    
    public static EnableMiniMapIcons instance;

    private void Awake()
    {
        if (instance != null)
            Debug.Log("Il y a plus d'une instance de EnableMiniMapIcons dans la scène.");
        
        instance = this;
    }

    public void EnableObjects()
    {
        GameObject[] objectsToEnble = GameObject.FindGameObjectsWithTag("MinimapIcons");

        for (int i = 0; i < objectsToEnble.Length; i++)
        {
            objectsToEnble[i].GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
