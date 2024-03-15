using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;

    public bool isjumping;
    private float Move;
    public Rigidbody2D rb;

    private bool canDash = true;
    private bool isDashing;
    private float dashingpower = 45f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    
    public CoinCollector cm;
    [SerializeField] private TrailRenderer tr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (isDashing)
        {
            return;
        }
        Move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed * Move, rb.velocity.y);
        if (Input.GetButtonDown("Jump")&& isjumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && canDash)
        {
            StartCoroutine(Dash());
        }
      }
     private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float orginalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(Move * dashingpower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = orginalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
      }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            cm.coinCount++;
        } 
    }

}

