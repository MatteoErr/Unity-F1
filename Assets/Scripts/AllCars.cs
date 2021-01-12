using System.Collections.Generic;
using UnityEngine;

public class AllCars : MonoBehaviour
{
    public List<Car> cars;
    
    public static AllCars instance;
    
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance AllCars dans la scène !");
            return;
        }
        instance = this;
    }
}
