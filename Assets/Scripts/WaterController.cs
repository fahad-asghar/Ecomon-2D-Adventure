using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator playerAnimator;

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

    public bool moving;
    public bool movingLeft;
    public bool movingRight;
    bool enableJump;

    bool enableMobileController = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    //Mobile Controller
    public void Jump()
    {
        if (!poof.GetComponent<ParticleSystem>().isPlaying)
            poof.GetComponent<ParticleSystem>().Play();

        enableJump = true;
        transform.parent = null;
        PlaySound(clip1);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!poof.GetComponent<ParticleSystem>().isPlaying)
                poof.GetComponent<ParticleSystem>().Play();

            enableJump = true;
            transform.parent = null;
            PlaySound(clip1);
        }

        //Mobile Controller 
        if (joystick != null)
        {
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
        }
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            if (movingLeft)
            {
                //transform.localScale = new Vector2(-1, 1);
                transform.DORotate(new Vector3(transform.rotation.x, -180, -57.907f), 0, RotateMode.Fast);
                rb.velocity = new Vector2(-speed * Time.fixedDeltaTime, rb.velocity.y);               
            }
            if (movingRight)
            {
                //transform.localScale = new Vector2(1, 1);
                transform.DORotate(new Vector3(transform.rotation.x, 0, -57.907f), 0, RotateMode.Fast);
                rb.velocity = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
            }
        }

        if (!moving)
        {
            playerAnimator.Play(idleAnimation);
        }
        else
            playerAnimator.Play(runAnimation);

        if (enableJump)
        {
            enableJump = false;
            playerAnimator.Play(runAnimation);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
        }
    }
}
