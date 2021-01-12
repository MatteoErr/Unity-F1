using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiplePlayerFollow : MonoBehaviour
{
    public List<Transform> players;
    [SerializeField]
    Vector3 offset;
    Vector3 velocity;
    [SerializeField]
    float smoothTime, maxZoom = 10, minZoom = 100, zoomLimiter = 50;
    
    void LateUpdate()
    {
        if (players.Count == 0)
            return;

        transform.position = Vector3.SmoothDamp(transform.position, GetCenterPoint() + offset, ref velocity, smoothTime);
        GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter), Time.deltaTime);
    }

    Vector3 GetCenterPoint()
    {
        if (players.Count == 1)
            return players[0].position;
        var bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        return bounds.center;
    }
    
    float GetGreatestDistance()
    {
        var bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        return bounds.size.x;
    }
}
