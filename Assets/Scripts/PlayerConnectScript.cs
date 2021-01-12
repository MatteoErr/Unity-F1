using UnityEngine;
using UnityEngine.UI;

public class PlayerConnectScript : MonoBehaviour
{
    public string playerName;
    Text playerNameText;
    int carCount;
    ChooseCar chooseCar;

    private void Start()
    {
        playerNameText = GetComponentInChildren<Text>();
        playerNameText.text = playerName;
        
        chooseCar = GetComponentInChildren<ChooseCar>();
        chooseCar.selectedCar = FindInt(playerName);

        carCount = AllCars.instance.cars.Count;
    }

    public void GoToNextCar()
    {
        if (chooseCar.selectedCar + 1 < carCount)
            chooseCar.selectedCar++;
        else
            chooseCar.selectedCar = 0;
    }

    public void GoToPreviousCar()
    {
        if (chooseCar.selectedCar - 1 >= 0)
            chooseCar.selectedCar--;
        else
            chooseCar.selectedCar = carCount - 1;
    }


    int FindInt(string stringToSearch)
    {
        char[] vs = stringToSearch.ToCharArray();

        return int.Parse(vs[stringToSearch.Length - 1].ToString()) - 1;
    }
}
