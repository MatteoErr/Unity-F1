using UnityEngine;
using UnityEngine.UI;

public class ChooseCar : MonoBehaviour
{
    public int selectedCar;

    public void ChangeTag()
    {
        tag = "ConnectContent" + (GetComponentInChildren<ButtonScript>().player + 1);
    }

    void Update()
    {
        Car car = AllCars.instance.cars[selectedCar];
        GetComponentInChildren<Image>().sprite = car.sprite;
        GetComponentInChildren<Image>().rectTransform.localScale = car.size.normalized;
    }
}
