using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Navigation : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject fader;
    [SerializeField] GameObject fader1;
    [SerializeField] GameObject gameOverPopUp;
    [SerializeField] GameObject levelCompletionPopUp;
    [SerializeField] GameObject fullGamePopUp;
    [SerializeField] Text codeText;
    [SerializeField] Text errorText;
    [SerializeField] RectTransform scrollViewContent;
    [SerializeField] GameObject infoPopUp;


    [Header("Level Locks")]
    [SerializeField] List<GameObject> locks = new List<GameObject>();

    [Header("Button To Unlock")]
    [SerializeField] Transform buttons;

    GameObject obj;
    static string sceneNameTemp = "";


    private void Awake()
    {
        Camera.main.aspect = 1920f / 1080f;
        //PlayerPrefs.SetInt("FullGame", 1);

        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            sceneNameTemp = "";
            if (PlayerPrefs.GetInt("FullGame") == 1)
            {
                buttons.transform.GetChild(0).gameObject.SetActive(false);
                buttons.transform.GetChild(1).gameObject.SetActive(false);
                buttons.transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        if (SceneManager.GetActiveScene().name == "LevelSelection")
        {
            //Grass Levels
            if (PlayerPrefs.GetInt("GrassLevel2") == 1)
            {
                locks[0].GetComponent<Button>().interactable = true;
                locks[0].transform.GetChild(locks[0].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("GrassLevel3") == 1)
            {
                locks[1].GetComponent<Button>().interactable = true;
                locks[1].transform.GetChild(locks[1].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("GrassLevel4") == 1)
            {
                locks[2].GetComponent<Button>().interactable = true;
                locks[2].transform.GetChild(locks[2].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("GrassLevel5") == 1)
            {
                if (PlayerPrefs.GetInt("FullGame") == 1)
                {
                    locks[3].GetComponent<Button>().interactable = true;
                    locks[3].transform.GetChild(locks[3].transform.childCount - 1).gameObject.SetActive(false);
                }
            }


            //Water Levels
            if (PlayerPrefs.GetInt("WaterLevel2") == 1)
            {
                locks[4].GetComponent<Button>().interactable = true;
                locks[4].transform.GetChild(locks[4].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("WaterLevel3") == 1)
            {
                locks[5].GetComponent<Button>().interactable = true;
                locks[5].transform.GetChild(locks[5].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("WaterLevel4") == 1)
            {
                locks[6].GetComponent<Button>().interactable = true;
                locks[6].transform.GetChild(locks[6].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("WaterLevel5") == 1)
            {
                if (PlayerPrefs.GetInt("FullGame") == 1)
                {
                    locks[7].GetComponent<Button>().interactable = true;
                    locks[7].transform.GetChild(locks[7].transform.childCount - 1).gameObject.SetActive(false);
                }
            }

            //Ground Levels
            if (PlayerPrefs.GetInt("GroundLevel2") == 1)
            {
                locks[8].GetComponent<Button>().interactable = true;
                locks[8].transform.GetChild(locks[8].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("GroundLevel3") == 1)
            {
                locks[9].GetComponent<Button>().interactable = true;
                locks[9].transform.GetChild(locks[9].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("GroundLevel4") == 1)
            {
                locks[10].GetComponent<Button>().interactable = true;
                locks[10].transform.GetChild(locks[10].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("GroundLevel5") == 1)
            {
                if (PlayerPrefs.GetInt("FullGame") == 1)
                {
                    locks[11].GetComponent<Button>().interactable = true;
                    locks[11].transform.GetChild(locks[11].transform.childCount - 1).gameObject.SetActive(false);
                }
            }


            //Ice Levels
            if (PlayerPrefs.GetInt("IceLevel2") == 1)
            {
                locks[12].GetComponent<Button>().interactable = true;
                locks[12].transform.GetChild(locks[12].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("IceLevel3") == 1)
            {
                locks[13].GetComponent<Button>().interactable = true;
                locks[13].transform.GetChild(locks[13].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("IceLevel4") == 1)
            {
                locks[14].GetComponent<Button>().interactable = true;
                locks[14].transform.GetChild(locks[14].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("IceLevel5") == 1)
            {
                if (PlayerPrefs.GetInt("FullGame") == 1)
                {
                    locks[15].GetComponent<Button>().interactable = true;
                    locks[15].transform.GetChild(locks[15].transform.childCount - 1).gameObject.SetActive(false);
                }
            }


            //Air Levels
            if (PlayerPrefs.GetInt("AirLevel2") == 1)
            {
                locks[16].GetComponent<Button>().interactable = true;
                locks[16].transform.GetChild(locks[16].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("AirLevel3") == 1)
            {
                locks[17].GetComponent<Button>().interactable = true;
                locks[17].transform.GetChild(locks[17].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("AirLevel4") == 1)
            {
                locks[18].GetComponent<Button>().interactable = true;
                locks[18].transform.GetChild(locks[18].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("AirLevel5") == 1)
            {
                if (PlayerPrefs.GetInt("FullGame") == 1)
                {
                    locks[19].GetComponent<Button>().interactable = true;
                    locks[19].transform.GetChild(locks[19].transform.childCount - 1).gameObject.SetActive(false);
                }
            }


            //Ether Levels
            if (PlayerPrefs.GetInt("EtherGrass") == 1)
            {
                locks[20].GetComponent<Button>().interactable = true;
                locks[20].transform.GetChild(locks[20].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("EtherGround") == 1)
            {
                locks[21].GetComponent<Button>().interactable = true;
                locks[21].transform.GetChild(locks[21].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("EtherIce") == 1)
            {
                locks[24].GetComponent<Button>().interactable = true;
                locks[24].transform.GetChild(locks[24].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("EtherAir") == 1)
            {
                locks[22].GetComponent<Button>().interactable = true;
                locks[22].transform.GetChild(locks[22].transform.childCount - 1).gameObject.SetActive(false);
            }
            if (PlayerPrefs.GetInt("EtherWater") == 1)
            {
                locks[23].GetComponent<Button>().interactable = true;
                locks[23].transform.GetChild(locks[23].transform.childCount - 1).gameObject.SetActive(false);
            }


            //Scroll Positions
            if (sceneNameTemp.Equals("GrassLevel1") 
                || sceneNameTemp.Equals("GrassLevel2")
                || sceneNameTemp.Equals("GrassLevel3")
                || sceneNameTemp.Equals("GrassLevel4")
                || sceneNameTemp.Equals("GrassLevel5"))
                scrollViewContent.position = new Vector3(-8.888889f, scrollViewContent.position.y, scrollViewContent.position.z);

            if (sceneNameTemp.Equals("WaterLevel1") 
                || sceneNameTemp.Equals("WaterLevel2")
                || sceneNameTemp.Equals("WaterLevel3")
                || sceneNameTemp.Equals("WaterLevel4")
                || sceneNameTemp.Equals("WaterLevel5"))
                scrollViewContent.position = new Vector3(-26.6589f, scrollViewContent.position.y, scrollViewContent.position.z);

            if (sceneNameTemp.Equals("GroundLevel1")
                || sceneNameTemp.Equals("GroundLevel2")
                || sceneNameTemp.Equals("GroundLevel3")
                || sceneNameTemp.Equals("GroundLevel4")
                || sceneNameTemp.Equals("GroundLevel5"))
                scrollViewContent.position = new Vector3(-44.4289f, scrollViewContent.position.y, scrollViewContent.position.z);

            if (sceneNameTemp.Equals("IceLevel1")
                || sceneNameTemp.Equals("IceLevel2")
                || sceneNameTemp.Equals("IceLevel3")
                || sceneNameTemp.Equals("IceLevel4")
                || sceneNameTemp.Equals("IceLevel5"))
                scrollViewContent.position = new Vector3(-62.1989f, scrollViewContent.position.y, scrollViewContent.position.z);

            if (sceneNameTemp.Equals("AirLevel1")
                || sceneNameTemp.Equals("AirLevel2")
                || sceneNameTemp.Equals("AirLevel3")
                || sceneNameTemp.Equals("AirLevel4")
                || sceneNameTemp.Equals("AirLevel5"))
                scrollViewContent.position = new Vector3(-79.9689f, scrollViewContent.position.y, scrollViewContent.position.z);

            if (sceneNameTemp.Equals("EtherGrass")
                || sceneNameTemp.Equals("EtherAir")
                || sceneNameTemp.Equals("EtherGround")
                || sceneNameTemp.Equals("EtherWater")
                || sceneNameTemp.Equals("EtherIce"))
                scrollViewContent.position = new Vector3(-97.7389f, scrollViewContent.position.y, scrollViewContent.position.z);
        }
    }

    private void Start()
    {
        fader.transform.DOLocalMoveX(-2300, 1, false).OnComplete(delegate ()
        {
            fader.SetActive(false);
            fader.transform.DOLocalMoveX(2300, 0, false);
        });
    }


    public void FullGamePopUp()
    {
        if (GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>().Play();

        fader1.GetComponent<Image>().DOFade(0, 0).OnComplete(delegate ()
        {
            fader1.SetActive(true);
            fader1.GetComponent<Image>().DOFade(0.7f, 0.6f).OnComplete(delegate ()
            {
                fullGamePopUp.SetActive(true);
                fullGamePopUp.GetComponent<Animator>().Play("PopUpOpen");
            });
        });
    }

    public void InfoPopUp()
    {
        if (GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>().Play();

        infoPopUp.SetActive(true);
        infoPopUp.GetComponent<Animator>().Play("PopUpOpen");                
    }
    public void CloseInfoPopUp()
    {
        if (GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>().Play();

        infoPopUp.GetComponent<Animator>().Play("PopUpClose");                 
    }

    public void CloseFullGamePopUp()
    {
        if (GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>().Play();

        fullGamePopUp.GetComponent<Animator>().Play("PopUpClose");
        fader1.GetComponent<Image>().DOFade(0, 0.65f).OnComplete(delegate ()
        {
            fader1.SetActive(false);
            fullGamePopUp.SetActive(false);
        });
    }

    public void MatchCode()
    {
        if (GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>().Play();

        if (codeText.text.Equals("hrffl6fc") || codeText.text.Equals("m6f2s8m2") ||
            codeText.text.Equals("w53xjjaw") || codeText.text.Equals("7ayspg1i") ||
            codeText.text.Equals("10s5xjjp") || codeText.text.Equals("tvh0o55k") ||
            codeText.text.Equals("ijts563u"))
        {
            PlayerPrefs.SetInt("FullGame", 1);

            buttons.transform.GetChild(0).gameObject.SetActive(false);
            buttons.transform.GetChild(1).gameObject.SetActive(false);
            buttons.transform.GetChild(2).gameObject.SetActive(true);

            fullGamePopUp.GetComponent<Animator>().Play("PopUpClose");
            fader1.GetComponent<Image>().DOFade(0, 0.65f).OnComplete(delegate ()
            {
                fader1.SetActive(false);
                fullGamePopUp.SetActive(false);
            });
        }

        else
        {
            errorText.text = "Incorrect Code!";
        }
    }

    public void GetCode()
    {
        Application.OpenURL("https://opensea.io/collection/ecomon");
    }

    public void GetTreasure()
    {
        
        Application.OpenURL("http://ecomon.world/game-treasure");
        obj = EventSystem.current.currentSelectedGameObject;
        obj.GetComponent<Button>().interactable = false;
        Invoke("Hide", 3);
    }

    private void Hide()
    {
        obj.SetActive(false);
    }

    public void ResetErrorMessage()
    {
        errorText.text = "";
    }

    public void GameOver()
    {
        fader1.GetComponent<Image>().DOFade(0, 0).OnComplete(delegate ()
        {
            fader1.SetActive(true);
            fader1.GetComponent<Image>().DOFade(0.7f, 0.6f).OnComplete(delegate ()
            {
                gameOverPopUp.SetActive(true);
            });
        });
    }

    public void LevelComplete()
    {
        fader1.GetComponent<Image>().DOFade(0, 0).OnComplete(delegate ()
        {
            fader1.SetActive(true);
            fader1.GetComponent<Image>().DOFade(0.7f, 0.6f).OnComplete(delegate ()
            {
                levelCompletionPopUp.SetActive(true);
            });
        });
    }

    public void SelectLevel()
    {
        DOTween.KillAll();
        if(GetComponent<AudioSource>() != null)
            GetComponent<AudioSource>().Play();

        string name = EventSystem.current.currentSelectedGameObject.name;

        if(SceneManager.GetActiveScene().name == "LevelSelection")
            sceneNameTemp = name;

        fader.SetActive(true);
        fader.transform.DOLocalMoveX(0, 1, false).OnComplete(delegate ()
        {
            SceneManager.LoadScene(name);
        });
    }
}
