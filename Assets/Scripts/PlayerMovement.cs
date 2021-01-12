using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // the commented code is the code that I was using with the old inputsystem

    float moveSpeed, rotationSpeed;
    
    Car car;
    Rigidbody2D rb;
    Animator animator;
    //string inputNameHorizontal, inputNameVertical, inputNameBoost, inputNameSlowDown;
    [HideInInspector]
    public float movement;
    [HideInInspector]
    public float rotationSensibility, boost, verticalAxis, rotationForce;
    float rotation;
    public int wichPlayer;

    private void OnEnable()
    {
        if (car != null)
            return;
        
        wichPlayer = FindInt(tag) - 1;
        car = AllCars.instance.cars[GameObject.FindGameObjectWithTag("ConnectContent" + (wichPlayer + 1)).GetComponent<ChooseCar>().selectedCar];

        Rigidbody2D _rb = gameObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        Animator _animator = gameObject.AddComponent(typeof(Animator)) as Animator;
        gameObject.AddComponent(typeof(SpriteRenderer));

        SpriteRenderer[] _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        _rb.bodyType = RigidbodyType2D.Dynamic;
        _rb.useAutoMass = false;
        _rb.mass = 1000f;
        _rb.sharedMaterial = car.material;
        _rb.freezeRotation = true;

        _animator.runtimeAnimatorController = car.animatorController;

        for (int i = 0; i < _spriteRenderers.Length; i++)
            _spriteRenderers[i].sprite = car.sprite;
        _spriteRenderers[0].sortingOrder = 5;

        PolygonCollider2D _playerCollider = gameObject.AddComponent(typeof(PolygonCollider2D)) as PolygonCollider2D;
        _playerCollider.sharedMaterial = car.material;

        moveSpeed = car.speed;
        rotationSpeed = car.rotationSpeed;
        transform.localScale = car.size;

        /*inputNameHorizontal = "Horizontal" + (wichPlayer + 1).ToString();
        inputNameVertical = "Vertical" + (wichPlayer + 1).ToString();
        inputNameBoost = "Boost" + (wichPlayer + 1).ToString();
        inputNameSlowDown = "SlowDown" + (wichPlayer + 1).ToString();*/

        rotationSensibility = 1;
    }

    private void Start()
    {
        gameObject.GetComponentInChildren<SpriteRenderer>(true).enabled = true;

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        EnableMiniMapIcons.instance.EnableObjects();

        boost = 1;
    }

    void Update()
    {
        /*if (Input.GetButton(inputNameBoost) && boost[wichPlayer] < 1.95f)
            boost[wichPlayer] += 0.001f;
        else if (boost[wichPlayer] > 1f && !Input.GetButton(inputNameBoost))
            boost[wichPlayer] -= 0.01f;
        
        if (Input.GetButton(inputNameSlowDown) && boost[wichPlayer] > 0.3f)
            boost[wichPlayer] -= 0.01f;
        else if (boost[wichPlayer] < 1f && !Input.GetButton(inputNameSlowDown))
            boost[wichPlayer] = 1f;

        rotation = Input.GetAxis(inputNameHorizontal) * rotationSpeed * Time.fixedDeltaTime * rotationSensibility[wichPlayer];
        movement = Input.GetAxis(inputNameVertical) * moveSpeed * Time.fixedDeltaTime * boost[wichPlayer];*/

        rotation = rotationForce * rotationSpeed * Time.fixedDeltaTime * rotationSensibility;
        movement = verticalAxis * moveSpeed * Time.fixedDeltaTime * boost;

        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x + rb.velocity.y));
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        transform.Rotate(new Vector3(0f, 0f, 1f) * -rotation);

        rb.velocity = transform.up * movement;
    }

    int FindInt(string stringToSearch)
    {
        char[] vs = stringToSearch.ToCharArray();

        return int.Parse(vs[stringToSearch.Length - 1].ToString());
    }
}
