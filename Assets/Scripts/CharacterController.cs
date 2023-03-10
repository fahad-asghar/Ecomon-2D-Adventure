using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController : MonoBehaviour
{

    Rigidbody2D rb;
    Animator playerAnimator;

    [SerializeField] LayerMask whatIsGround;
    private float extentX;
    private float extentY;

    [Header("Forces")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    [Header("Animations")]
    [SerializeField] string jumpAnimation;
    [SerializeField] string runAnimation;
    [SerializeField] string idleAnimation;


    [Header("Audio Clips")]
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;

    [Header("Effects")]
    [SerializeField] GameObject poof;

    [Header("Joystick")]
    [SerializeField] Joystick joystick;


    bool isGrounded1;
    bool isGrounded2;
    public bool moving;
    public bool movingLeft;
    public bool movingRight;
    public bool enableJump;

    bool enableMobileController = true;


    [SerializeField] public GameObject mountains;

    void Start()
    {
        extentX = GetComponent<SpriteRenderer>().bounds.extents.x;
        extentY = GetComponent<SpriteRenderer>().bounds.extents.y;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    //Mobile Controller
    public void Jump()
    {
        if (isGrounded1 || isGrounded2)
        {
            enableJump = true;
            transform.parent = null;
            PlaySound(clip1);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSource, 0.5f);
    }

    private void Update()
    {
        if (rb.velocity.x >= 2)
        {
            if (mountains != null)
                MoveMountain("Right");
        }

        else if (rb.velocity.x <= -2)
        {
            if (mountains != null)
                MoveMountain("Left");
        }

        else
        {
            if (mountains != null)
                MoveMountain("Garbage");
        }



        //Desktop Controller
        if (Input.GetKey(KeyCode.LeftArrow))
        {     
            moving = true;
            movingLeft = true;
            enableMobileController = false;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moving = true;
            movingRight = true;
            enableMobileController = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {      
            moving = false;
            movingLeft = false;
            enableMobileController = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {      
            moving = false;
            movingRight = false;
            enableMobileController = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded1 || isGrounded2))
        {
            enableJump = true;
            transform.parent = null;
            PlaySound(clip1);
        }


        //Mobile Controller       
        if (joystick.Horizontal > 0.3f)
        {   
            moving = true;
            movingRight = true;
            movingLeft = false;
        }
        if (joystick.Horizontal < -0.3f)
        {        
            moving = true;
            movingLeft = true;
            movingRight = false;
        }
        if (joystick.Horizontal <= 0.3f && joystick.Horizontal >= -0.3f && enableMobileController)
        {  
            moving = false;
            movingLeft = false;
            movingRight = false;
        }
   
        RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(transform.position.x + extentX/2, transform.position.y), Vector2.down, Mathf.Abs(-extentY), whatIsGround);       
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x - extentX/2, transform.position.y), Vector2.down, Mathf.Abs(-extentY), whatIsGround);

        if (hit1.collider != null)
        {
            if (!isGrounded1 && !isGrounded2)
            {
                Instantiate(poof, new Vector2(transform.position.x, transform.position.y - extentY), Quaternion.identity);
                PlaySound(clip2);
            }

            isGrounded1 = true;           
            Debug.DrawRay(new Vector2(transform.position.x + extentX / 2, transform.position.y), Vector2.down, Color.green);
        }
        else
        {
            isGrounded1 = false;
            Debug.DrawRay(new Vector2(transform.position.x + extentX/2, transform.position.y), Vector2.down, Color.red);
        }

        if (hit2.collider != null)
        {
            if (!isGrounded1 && !isGrounded2)
            {
                Instantiate(poof, new Vector2(transform.position.x, transform.position.y - extentY), Quaternion.identity);
                PlaySound(clip2);
            }

            isGrounded2 = true;
            Debug.DrawRay(new Vector2(transform.position.x - extentX / 2, transform.position.y), Vector2.down, Color.green);
        }
        else
        {
            isGrounded2 = false;
            Debug.DrawRay(new Vector2(transform.position.x - extentX/2, transform.position.y), Vector2.down, Color.red);
        }
    }

    private void FixedUpdate()
    {    
        if (moving)
        {
            if (movingLeft)
            {
                transform.localScale = new Vector2(-1, 1);
                rb.velocity = new Vector2(-speed * Time.fixedDeltaTime, rb.velocity.y);

                if (isGrounded1 || isGrounded2)
                    playerAnimator.Play(runAnimation);
            }
            if (movingRight)
            {
                transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);

                if (isGrounded1 || isGrounded2)
                    playerAnimator.Play(runAnimation);
            }
        }
        if(!moving)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            playerAnimator.Play(idleAnimation);
        }       
        if (enableJump)
        {
            enableJump = false;
            playerAnimator.Play(idleAnimation);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
        }
    }


    public void MoveMountain(string direciton)
    {
        if (mountains != null)
        {
            DOTween.Kill(mountains.transform);

            if (direciton.Equals("Left"))
            {
                mountains.transform.DOLocalMoveX(0f, 0.3f, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                {
                    mountains.transform.localPosition = new Vector2(-38.22f, 0);
                    MoveMountain("Left");
                });
            }

            else if (direciton.Equals("Right"))
            {
                mountains.transform.DOLocalMoveX(-38.22f, 0.3f, false).SetSpeedBased().SetEase(Ease.Linear).OnComplete(delegate ()
                {
                    mountains.transform.localPosition = new Vector2(0, 0);
                    MoveMountain("Right");
                });
            }

            else
                DOTween.Kill(mountains.transform);
        }
        
    }
}
