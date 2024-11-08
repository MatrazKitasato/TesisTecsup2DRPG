using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float moveX;
    [SerializeField] float moveY;
    [SerializeField] Vector2 movement;
    public bool isTalking;

    [Header("Animation")]
    [SerializeField] Animator anim;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        UpdateAnimation();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (movement == Vector2.zero || isTalking == true)
        {
            rb.velocity = Vector2.zero * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = movement.normalized * speed * Time.fixedDeltaTime;
        }
    }
    public void GetInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if(!isTalking)
        {
            movement = new Vector2(moveX, moveY);
        }
    }
    public void UpdateAnimation()
    {
        if(movement != Vector2.zero)
        {
            anim.SetBool("Walk", true);
            anim.SetFloat("MoveX", moveX);
            anim.SetFloat("MoveY", moveY);
        }
        else
        {
            anim.SetBool("Walk", false);
        }
        
    }
}
