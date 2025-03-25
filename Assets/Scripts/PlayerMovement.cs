using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Menu menu;
    
    public Collider2D playerCollider;
    public SpriteRenderer sr;
    public Transform tf;
    Rigidbody2D rb;

    private float moveX;
    private float moveY;

    void Start()
    {
        menu = GameObject.Find("ScriptHolder").GetComponent<Menu>();
        playerCollider = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        tf = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (menu.freeze) {rb.simulated = false;}
        else {rb.simulated = true;}
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");   
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed);
    }
}
