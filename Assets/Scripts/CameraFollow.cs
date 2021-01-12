using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    Vector3 playerPosition = new Vector3(0f, 0f, -10f);

    void LateUpdate()
    {
        playerPosition = player.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y, -10f);
        transform.rotation = player.transform.rotation;
    }
}
