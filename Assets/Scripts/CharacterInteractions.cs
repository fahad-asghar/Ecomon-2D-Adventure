using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterInteractions : MonoBehaviour
{
    [Header("Audio Clips")]
    [SerializeField] AudioClip clip1;
    [SerializeField] AudioClip clip2;
    [SerializeField] AudioClip clip3;
    [SerializeField] AudioClip clip4;
    [SerializeField] AudioClip clip5;

    [Header("Death Effect")]
    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject enemyDeathEffect;

    [Header("Camera Control")]
    [SerializeField] GameObject cinemachine;

    [Header("Jump force on spring")]
    [SerializeField] float springJumpForce;

    [Header("Make player invincible")]
    [SerializeField] bool invincible;

    [Header("Game UI Canvas")]
    [SerializeField] GameObject Canvas;
    [SerializeField] Text coinsText;
    [SerializeField] Text lifesText;

    [Header("Level to Unlock")]
    [SerializeField] string levelName;

    [Header("Checkpoints")]
    [SerializeField] List<Transform> checkPoints = new List<Transform>();

    [Header("Enemies")]
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;

    [Header("Coffer")]
    [SerializeField] GameObject coffer;

    int counter = 0;

    private int coins = 0;
    private int lifes = 3;


    private void Awake()
    {
        lifesText.text = lifes.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Coins"))
        {
            collision.GetComponent<Collider2D>().enabled = false;
            coins++;
            coinsText.text = coins.ToString();

            PlaySound(clip1, gameObject);

            collision.transform.DOScale(new Vector2(0, 0), 0.2f).OnComplete(delegate ()
            {
                Destroy(collision.gameObject);
            });
        }

        if (collision.gameObject.CompareTag("FinalFight"))
        {
            collision.gameObject.SetActive(false);
            enemy1.SetActive(true);
        }

        if (collision.gameObject.CompareTag("DeathPoint"))
        {
            if (collision.transform.parent.name == "Enemy")
            {
                collision.transform.parent.GetComponent<FInalEnemyHealth>().health--;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 450 * Time.fixedDeltaTime);
                PlaySound(clip3, gameObject);

                if (collision.transform.parent.GetComponent<FInalEnemyHealth>().health == 0)
                {
                    counter++;
                    if (counter == 1)
                        enemy2.SetActive(true);
                    if (counter == 2)
                        enemy3.SetActive(true);
                    if (counter == 3)
                        coffer.SetActive(true);

                    GameObject obj = collision.gameObject;

                    Instantiate(enemyDeathEffect, obj.transform.position, Quaternion.identity);
                    Destroy(obj.transform.parent.GetComponent<Rigidbody2D>());
                    Destroy(obj.transform.parent.GetComponent<Pathfinding.AIDestinationSetter>());
                    obj.transform.parent.GetChild(1).GetComponent<Collider2D>().enabled = false;
                    obj.transform.parent.GetChild(0).GetComponent<Collider2D>().enabled = false;
                    obj.transform.parent.GetComponent<SpriteRenderer>().DOFade(0, 0.5f).OnComplete(delegate ()
                    {
                        Destroy(obj.transform.parent.gameObject);
                    });
                }
            }

            else
            {

                PlaySound(clip3, gameObject);
                GameObject obj = collision.gameObject;

                Destroy(obj.transform.parent.GetComponent<Movement>());
                Destroy(obj.transform.parent.GetComponent<Rigidbody2D>());

                obj.transform.parent.GetChild(1).GetComponent<Collider2D>().enabled = false;
                obj.transform.parent.GetChild(0).GetComponent<Collider2D>().enabled = false;


                obj.transform.parent.GetComponent<Animator>().Play("Slime_Dead");

                if (SceneManager.GetActiveScene().name == "EtherWater" ||
                    SceneManager.GetActiveScene().name == "WaterLevel1" ||
                    SceneManager.GetActiveScene().name == "WaterLevel2" ||
                    SceneManager.GetActiveScene().name == "WaterLevel3" ||
                    SceneManager.GetActiveScene().name == "WaterLevel4" ||
                    SceneManager.GetActiveScene().name == "WaterLevel5")
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 250 * Time.fixedDeltaTime);

                else
                    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 400 * Time.fixedDeltaTime);

                obj.transform.parent.GetComponent<SpriteRenderer>().DOFade(0, 1f).OnComplete(delegate ()
                {
                    Destroy(obj.transform.parent.gameObject);
                });
            }       
        }

        if (!invincible)
        {
            if (collision.gameObject.tag == "Killer")
            {
                if (lifes == 0)
                    GameOver(collision.gameObject);
                else
                    Respawn(collision.gameObject);           
            }
        }

        if (collision.gameObject.CompareTag("Spring"))
        {
            PlaySound(clip4, gameObject);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, springJumpForce * Time.fixedDeltaTime);
            collision.gameObject.GetComponent<Animator>().enabled = true;
            collision.gameObject.GetComponent<Animator>().Play("SpringAnimation", -1, 0);
        }

        if (collision.gameObject.CompareTag("LevelComplete"))
        {
            PlayerPrefs.SetInt(levelName, 1);
          
            collision.gameObject.GetComponent<AudioSource>().Play();
            collision.gameObject.GetComponent<Animator>().enabled = true;

            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GetComponent<Animator>().Play("Demo_Character_Idle");

            Canvas.GetComponent<Navigation>().LevelComplete();

            if(GetComponent<CharacterController>() != null)
                Destroy(GetComponent<CharacterController>());
            if (GetComponent<WaterController>() != null)
                Destroy(GetComponent<WaterController>());

            Destroy(GetComponent<CharacterInteractions>());
        }

        
    }

    private void GameOver(GameObject obj)
    {
        Canvas.GetComponent<Navigation>().GameOver();
        PlaySound(clip2, obj.gameObject);

        Instantiate(deathEffect, transform.position, Quaternion.Euler(90, 0, 0));

        if (GetComponent<CharacterController>() != null)
        {
            if (GetComponent<CharacterController>().mountains != null)
                GetComponent<CharacterController>().MoveMountain("Garbage");
        }
        Destroy(gameObject);
    }

    private void Respawn(GameObject obj)
    {
        PlaySound(clip2, obj.gameObject);

        lifes--;
        lifesText.text = lifes.ToString();
        Instantiate(deathEffect, transform.position, Quaternion.Euler(90, 0, 0));

        if (GetComponent<CharacterController>() != null)
        {
            if (GetComponent<CharacterController>().mountains != null)
                GetComponent<CharacterController>().MoveMountain("Garbage");
        }

        GetComponent<SpriteRenderer>().DOFade(0, 0.2f).OnComplete(delegate() {
            transform.localScale = new Vector2(1, 1);
        });
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Animator>().Play("Demo_Character_Idle");

        if (GetComponent<CharacterController>() != null)
        {
            GetComponent<CharacterController>().moving = false;
            GetComponent<CharacterController>().movingLeft = false;
            GetComponent<CharacterController>().movingRight = false;
            GetComponent<CharacterController>().enabled = false;
        }

        if (GetComponent<WaterController>() != null)
        {
            GetComponent<WaterController>().moving = false;
            GetComponent<WaterController>().movingLeft = false;
            GetComponent<WaterController>().movingRight = false;
            GetComponent<WaterController>().enabled = false;
        }

        int index = 0;
        for (int i = checkPoints.Count - 1; i >= 0; i--)
        {
            if (transform.position.x > checkPoints[i].position.x)
            {
                index = i;
                break;
            }
        }

        transform.parent = null;
        GetComponent<Rigidbody2D>().DOMove(checkPoints[index].position, 1, false).SetDelay(2).OnComplete(delegate ()
        {           
            GetComponent<SpriteRenderer>().DOFade(1, 0.3f).OnComplete(delegate ()
            {
                GetComponent<Collider2D>().enabled = true;
                GetComponent<Rigidbody2D>().isKinematic = false;

                if (GetComponent<CharacterController>() != null)
                    GetComponent<CharacterController>().enabled = true;
                if (GetComponent<WaterController>() != null)
                    GetComponent<WaterController>().enabled = true;
            });           
        });     
    }


    private void PlaySound(AudioClip clip, GameObject obj)
    {
        AudioSource audioSource = obj.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(audioSource, 0.5f);
    }
}
