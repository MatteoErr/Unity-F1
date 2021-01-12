using UnityEngine;

[CreateAssetMenu(fileName = "Car", menuName = "Car")]
public class Car : ScriptableObject
{
    public string carName;
    public int id;
    public Sprite sprite;
    public RuntimeAnimatorController animatorController;
    public float speed = 1500f, rotationSpeed = 150f;
    public PhysicsMaterial2D material;
    public Color color;
    public Vector2 size;
}
