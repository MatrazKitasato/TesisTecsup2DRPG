using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float moveX;
    [SerializeField] float moveY;
    [SerializeField] Vector2 movement;

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
        if(movement.magnitude > 0)
        {
            rb.velocity = movement.normalized * speed * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
            Debug.Log("No se mueve");
        }
    }
    public void GetInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        movement = new Vector2(moveX, moveY);
    }
    public void UpdateAnimation()
    {
        if(movement.magnitude > 0)
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
