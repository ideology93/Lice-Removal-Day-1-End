using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using Tabtale.TTPlugins;

public class GameManager : MonoBehaviour
{
    public GameObject vacuumTrash;
    public enum GamePhase
    {
        Start, Dye, Comb, Grow, Clean, Steam, Wash, Fuzz, Vacuum
    }
    public bool session;
    public bool newGame;

    public RectTransform lookBookImagePrefab;
    public int salonLevel;
    public List<Sprite> beforePictures = new List<Sprite>();
    public List<Sprite> afterPictures = new List<Sprite>();

    public List<GameObject> salons = new List<GameObject>();
    public GameObject dripParticles;
    public int sessionNum;
    public Sprite lastBeforeSprite, lastAfterSprite;
    public GamePhase gamePhase;
    private static GameManager _instance;
    public List<Sprite> lookBook = new List<Sprite>();
    public GameObject PickPhase, SittingPhase, LiquidPhase, vacuumPhase, ExtensionPhase, fuzzPhase, scanPhase, monitorPhase;
    public RectTransform LookBookGameObject;
    public Character character;
    public SR_RenderCamera_After cam;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Game Manager is null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("firstLaunch") == 0)
        {
            UI_Manager.Instance.lookbookButton.GetComponent<Button>().interactable = false;
            UI_Manager.Instance.shopIcon.GetComponent<Button>().interactable = false;
            PlayerPrefs.SetInt("firstLaunch", 1);
            PlayerPrefs.SetInt("MissionLevel", 1);
            Debug.Log("Level is" + PlayerPrefs.GetInt("Level"));
            
        }
        else
        {
            if (CheckLevel() == 1)
            {
                UI_Manager.Instance.shopIcon.GetComponent<Button>().interactable = true;
                if (PlayerPrefs.GetInt("shopEnteredFlag") == 0)
                {
                    UI_Manager.Instance.shopIcon.GetComponent<Animator>().enabled = true;
                }
                else
                    UI_Manager.Instance.shopIcon.GetComponent<Animator>().enabled = false;
            }
            else if (CheckLevel() > 1)
            {
                UI_Manager.Instance.lookbookButton.GetComponent<Button>().interactable = true;
                UI_Manager.Instance.shopIcon.GetComponent<Button>().interactable = true;
                UI_Manager.Instance.shopIcon.GetComponent<Animator>().enabled = false;
                if (PlayerPrefs.GetInt("instagramEnteredFlag") == 0)
                {
                    UI_Manager.Instance.lookbookButton.GetComponent<Animator>().enabled = true;
                }
                else
                    UI_Manager.Instance.lookbookButton.GetComponent<Animator>().enabled = false;
            }

        }
        SetPopularity();

        salonLevel = PlayerPrefs.GetInt("salonLevel", 0);

        Application.targetFrameRate = 60;
        sessionNum = PlayerPrefs.GetInt("session", 1);
        PlayerPrefs.SetInt("session", sessionNum);

        LoadSalon();
        UI_Manager.Instance.cashText.text = "$ " + UI_Manager.Instance.ShortNotation(PlayerPrefs.GetInt("cash".ToString()));

        StartCoroutine(LoadLookBook());


    }
    public void CleanPhase()
    {
        gamePhase = GamePhase.Clean;
        RaycastController.Instance.garbageDragRaycaster.enabled = true;
    }
    public IEnumerator FuzzPhase()
    {
        yield return new WaitForSeconds(1f);
        gamePhase = GamePhase.Fuzz;
        RaycastController.Instance.fuzzRaycaster.enabled = true;
        CharacterGenerator.Instance.EnableFuzzColliders();
        UI_Manager.Instance.fuzzPanel.SetActive(true);
        GameManager.Instance.fuzzPhase.SetActive(true);
        CharacterGenerator.Instance.currentHairScript.Garbage.SetActive(false);
        UI_Manager.Instance.guide_Fuzz.SetActive(true);
        RaycastController.Instance.garbageDragRaycaster.enabled = false;
    }

    public void BottlePhase()
    {
        StartCoroutine(BottlePhaseCoroutine());
    }
    public IEnumerator BottlePhaseCoroutine()
    {
        Debug.Log("Raised");
        gamePhase = GamePhase.Dye;

        yield return new WaitForSeconds(0.7f);
        ;
        LiquidPhase.SetActive(true);
        ToolManager.Instance.bottle.SetActive(true);
        UI_Manager.Instance.liquidPanel.SetActive(true);
        CameraController.Instance.PaintingCamera();
        CharacterGenerator.Instance.characterGameObject.GetComponent<Character>().ToggleHeadColliders();
        RaycastController.Instance.liquidRaycaster.enabled = true;
        UI_Manager.Instance.guide_Liquid.SetActive(true);
        RaycastController.Instance.fuzzRaycaster.enabled = false;

        RaycastController.Instance.fuzzRaycaster.enabled = false;
    }
    public IEnumerator VacuumPhaseCoroutine()
    {

        gamePhase = GamePhase.Vacuum;
        UI_Manager.Instance.vacuumPanel.SetActive(true);
        ToolManager.Instance.vacuum.SetActive(true);
        CameraController.Instance.VacuumCamera();
        yield return new WaitForSeconds(1f);
        RaycastController.Instance.vacuumRaycaster.enabled = true;
    }
    public void VacuumPhase()
    {
        StartCoroutine(VacuumPhaseCoroutine());
    }
    public IEnumerator StartCombingPhase()
    {
        if (gamePhase != GamePhase.Comb)
        {
            gamePhase = GamePhase.Comb;

            yield return new WaitForSeconds(2f);
            CombController.Instance.StartCombing();
            CameraController.Instance.LeftCombingCamera();
            RaycastController.Instance.liquidRaycaster.enabled = false;
            RaycastController.Instance.combRaycaster.enabled = true;
            UI_Manager.Instance.guide_Comb.SetActive(true);
            ToolManager.Instance.bottle.SetActive(false);
        }

    }
    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f);
        cam.CamCaptureAfter();
        yield return new WaitForSeconds(1f);
        UI_Manager.Instance.endPanel.SetActive(true);
    }
    public void FinishLevel()
    {
        PlayerPrefs.SetInt("session", 1);
        PlayerPrefs.SetInt("firstLaunch", 1);
        // PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

    }
    [Button]
    public void LoadSalon()
    {
        for (int i = 0; i < salons.Count; i++)
        {
            if (i == PlayerPrefs.GetInt("salonLevel"))
            {
                salons[i].SetActive(true);

            }
            else
                salons[i].SetActive(false);
        }
    }
    [Button]
    public void LevelUp()
    {
        salonLevel++;
        PlayerPrefs.SetInt("salonLevel", PlayerPrefs.GetInt("salonLevel") + 1);
    }
    [Button]
    public void PrintPrefs()
    {
        Debug.Log("Prefs are" + PlayerPrefs.GetInt("session"));
    }
    public IEnumerator LoadLookBook()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < beforePictures.Count; i++)
        {
            if (i < afterPictures.Count)
            {
                if (afterPictures[i] != null)
                {
                    LookBookGameObject = Instantiate(lookBookImagePrefab, lookBookImagePrefab.transform.position, Quaternion.identity, UI_Manager.Instance.lookbookContent.GetComponent<RectTransform>());
                    LookBookGameObject.localPosition = new Vector3(LookBookGameObject.localPosition.x, LookBookGameObject.localPosition.y, 0);
                    LookBookGameObject.GetComponent<InstagramImage>().before.sprite = beforePictures[i];
                    LookBookGameObject.GetComponent<InstagramImage>().after.sprite = afterPictures[i];
                    // LookBookGameObject.GetComponent<RectTransform>().position = new Vector3(LookBookGameObject.GetComponent<RectTransform>().position.x, LookBookGameObject.GetComponent<RectTransform>().position.y, 0);
                }
            }

        }
        UI_Manager.Instance.postsText.text = beforePictures.Count.ToString();
    }
    public void IncreasePopularity()
    {
        int randomFollowers = Random.Range(256, 1025);
        PlayerPrefs.SetInt("followers", PlayerPrefs.GetInt("followers") + randomFollowers);
        Debug.Log("Followers " + PlayerPrefs.GetInt("followers"));
        // UI_Manager.Instance.likesText.text = UI_Manager.Instance.ShortNotation(PlayerPrefs.GetInt("likes"))+"K";

    }
    public void SetPopularity()
    {
        UI_Manager.Instance.followersText.text = UI_Manager.Instance.ShortNotation(PlayerPrefs.GetInt("followers"));
    }
    public void GetCurrentMissionID()
    {
        // int level = CheckLevel();
        // ttp event
        int level = PlayerPrefs.GetInt("MissionLevel");
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("Level: " + level, "Random Level loop");
        TTPGameProgression.FirebaseEvents.MissionStarted(level, parameters);
        Debug.Log("Mission started with " + parameters);
    }
    public void IncreaseMissionID()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        int level = PlayerPrefs.GetInt("MissionLevel");
        parameters.Add("Level: " + level, "Random Level loop of level" + level + " completed");
        TTPGameProgression.FirebaseEvents.MissionComplete(parameters);
        PlayerPrefs.SetInt("MissionLevel", PlayerPrefs.GetInt("MissionLevel") + 1);
        Debug.Log("mission level :" + PlayerPrefs.GetInt("MissionLevel"));
    }
    public int CheckLevel()
    {
        if (PlayerPrefs.GetInt("Level", 1) == 1)
        {
            Debug.Log("First Time Opening");

            //Set first time opening to false
            PlayerPrefs.SetInt("Level", 1);
            return PlayerPrefs.GetInt("Level");
            //Do your stuff here

        }
        else
        {
            Debug.Log("NOT First Time Opening");
            return PlayerPrefs.GetInt("Level");
            //Do your stuff here
        }
    }
    public void ChangeLookBookFlag()
    {
        PlayerPrefs.SetInt("instagramEnteredFlag", 1);
    }
    public void ChangeShopFlag()
    {
        PlayerPrefs.SetInt("shopEnteredFlag", 1);
    }




}
