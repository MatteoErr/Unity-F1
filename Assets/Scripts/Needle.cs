using UnityEngine;
using UnityEngine.UI;

public class Needle : MonoBehaviour
{
    public Gradient gradient;
    public Image fill;
    public PlayerMovement playerMovement;

    private void Start()
    {
        SetMinSpeed();
    }

    private void Update()
    {
        SetSpeedColor(Mathf.Abs((GetComponent<RectTransform>().rotation.z - 1) / 2f));

        GetComponent<RectTransform>().rotation = Quaternion.Euler(0f, 0f, 180f - Mathf.Abs(playerMovement.movement * 6));
    }

    public void SetMinSpeed()
    {
        fill.color = gradient.Evaluate(0.01f);
    }

    public void SetSpeedColor(float speed)
    {
        fill.color = gradient.Evaluate(speed);
    }
}
