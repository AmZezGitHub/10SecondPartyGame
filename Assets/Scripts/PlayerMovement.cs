using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
  public Rigidbody2D rb;
  [SerializeField] private float speed;
  [SerializeField] private float jumpPower;
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private LayerMask wallLayer;
  private Rigidbody2D body;
  private Animator anim;
  private BoxCollider2D boxCollider;
  private float wallJumpCooldown;
  private float horizontalinput;
  public bool gameOver = false;
  public Text winText;
  public Text loseText;

  [Header ("SFX")]
  [SerializeField] private AudioClip jumpSound;
  [SerializeField] private AudioClip introSound;


  private void Awake()
    {
    rb = GetComponent<Rigidbody2D>();
    body = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    boxCollider = GetComponent<BoxCollider2D>();
    winText.text = "";
    loseText.text = "";
    }

  private void Update()
    {
    horizontalinput = Input.GetAxis("Horizontal");

  //Flip Player when moving left-right
    if (horizontalinput > 0.01f)
      transform.localScale = Vector3.one;
    if (horizontalinput < -0.01f)
      transform.localScale = new Vector3(-1,1,1);

    
    anim.SetBool("Walking", horizontalinput != 0);
    anim.SetBool("Grounded", isGrounded());

    if (wallJumpCooldown > 0.2f)
      { 
        //Jumping
        body.velocity = new Vector2(horizontalinput* speed, body.velocity.y);

        if(onWall() && !isGrounded())
        {
          body.gravityScale = 0;
          body.velocity = Vector2.zero;
        }
        else
          body.gravityScale = 3;

        if(Input.GetKey(KeyCode.W))
        {
          Jump();

            if(Input.GetKeyDown(KeyCode.W) && isGrounded())
              SoundManager.instance.PlaySound(jumpSound);
        }
      }
    else 
          wallJumpCooldown += Time.deltaTime;
        
     if (Input.GetKeyDown(KeyCode.Escape)) 
            {
        Application.Quit();
            }
    }

  void FixedUpdate()
        {
         if (Input.GetKey(KeyCode.R))
            {
            if (gameOver == true)
                {
              SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }

            }
        }
  private void Jump()
    {
      if(isGrounded())
      {
      body.velocity = new Vector2(body.velocity.x, jumpPower);
      anim.SetTrigger("Jump");
      }
      else if(onWall() && !isGrounded())
      {
        if(horizontalinput == 0)
        {
          body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 6, 0);
          transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else
          body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 2, 6);

        wallJumpCooldown = 0;
      }
    }

  private bool isGrounded()
   {
     RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
     return raycastHit.collider != null;
   }

  private bool onWall()
  {
    RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
     return raycastHit.collider != null;
  }
}