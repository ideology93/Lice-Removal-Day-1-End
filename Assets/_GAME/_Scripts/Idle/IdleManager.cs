using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;

public class IdleManager : GloballyAccessibleBase<IdleManager>
{
    public TargetIndicator targetIndicator;
    public int amountToAdd;
    public GameObject upgradeMarker;
    public GameObject chairParent;
    public ES3AutoSaveMgr es3;
    public List<Chair> startingChairs = new List<Chair>();
    public GameObject cashPrefab;
    public GameObject player;
    public int currentRequest;
    public int minigameValue;
    public Chair currentChair;
    public List<Sprite> requests = new List<Sprite>();
    public bool isNewSession;
    public int baseChairPrice, baseSalonPrice;
    public int cash;
    public int salonChairs;
    public int salonLevel;
    public List<Transform> chairs = new List<Transform>();
    public List<Chair> unlockedChairs = new List<Chair>();
    public List<GameObject> unlockedLevels = new List<GameObject>();
    public List<GameObject> levels = new List<GameObject>();
    public Transform chairs_1;
    public Transform chairs_2;
    public Transform chairs_3;
    public Transform chairs_4;
    public bool session, sessionEntered;
    public int sessionNum, sessionEnteredNum;
    public PlayerController playerController;

    public void Start()
    {
        if (salonLevel >= 3)
        {
            upgradeMarker.SetActive(false);
        }
        Debug.Log(PlayerPrefs.GetInt("session"));
        sessionNum = PlayerPrefs.GetInt("session");
        if (sessionNum == 0)
        {
            session = false;
        }
        else
            session = true;
        // session=true;

        cash = PlayerPrefs.GetInt("cash", 0);
        Idle_UIManager.Instance.cashText.text = "$ " + Idle_UIManager.Instance.ShortNotation(cash);
        salonChairs = PlayerPrefs.GetInt("salonChairs", 2);
        salonLevel = PlayerPrefs.GetInt("salonLevel", 0);
        SpawnLevel();
        isNewSession = false;
        StartCoroutine(ChangeFlag());
    }
    public void SpawnLevel()
    {
        Debug.Log(unlockedLevels.Count);
        SetSalon();
        SetChairs();
    }
    public void UpgradeSalon()
    {
        salonLevel++;
        Debug.Log("salon upgraded");
        PlayerPrefs.SetInt("salonLevel", salonLevel);
        Idle_UIManager.Instance.poof.Play();

        Invoke("LoseMoneySalon", 0.2f);
        if (salonLevel >= 3)
        {
            upgradeMarker.SetActive(false);
        }


    }
    public void BuyChair()
    {
        LoseMoneyChair();
        ES3AutoSaveMgr.Current.Save();
    }

    public void GetMoney(int money)
    {
        string direction = "+ $";
        AnimateMoney(money, direction, true);
    }
    public void LoseMoneyChair()
    {
        int amount = baseChairPrice;
        string direction = "- $";
        AnimateMoney(baseChairPrice, direction, false);
    }
    public void LoseMoneySalon()
    {
        int amount = baseSalonPrice;
        string direction = "- $";
        AnimateMoney(baseSalonPrice, direction, false);
        SetSalon();

    }
    public void AnimateMoney(int amount, string direction, bool gain)
    {
        if (gain)
        {

            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + amount);
        }
        else
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") - amount);
        IdleManager.Instance.cash = PlayerPrefs.GetInt("cash");
        Idle_UIManager.Instance.cashText.text = "$" + Idle_UIManager.Instance.ShortNotation(PlayerPrefs.GetInt("cash"));
        Idle_UIManager.Instance.cashText.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f, 5, 0);
        Idle_UIManager.Instance.cashIcon.transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.3f, 5, 0);
        TMP_Text floatingText = Instantiate(Idle_UIManager.Instance.cashText, Idle_UIManager.Instance.cashText.transform.position, Quaternion.identity, Idle_UIManager.Instance.cashIcon.transform);
        floatingText.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 50);
        floatingText.GetComponent<TextMeshProUGUI>().color = Color.white;
        floatingText.text = direction + amount.ToString();
        floatingText.transform.DOMove(floatingText.transform.position + new Vector3(0, 120, 0), 1f);
        Destroy(floatingText, 1.01f);
    }
    [Button]
    public void AddMoney()
    {

        cash = cash + amountToAdd;
        PlayerPrefs.SetInt("cash", cash);
        Idle_UIManager.Instance.cashText.text = Idle_UIManager.Instance.ShortNotation(cash);
    }
    public void SetSalon()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            if (i == salonLevel)
            {
                levels[i].SetActive(true);
            }
            else
                levels[i].SetActive(false);
        }
        ChangeChairs();
    }
    public void SetChairs()
    {
        for (int i = 0; i < startingChairs.Count; i++)
        {
            if (!unlockedChairs.Contains(startingChairs[i]))
            {
                unlockedChairs.Add(startingChairs[i]);
            }
        }
        for (int i = 0; i < unlockedChairs.Count; i++)
        {
            unlockedChairs[i].gameObject.SetActive(true);
        }
    }
    public IEnumerator ChangeFlag()
    {
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("session", 0);
        session = false;
    }
    public void ChangeChairs()
    {
        Debug.Log("childcount" + chairParent.transform.childCount);
        for (int i = 0; i < chairParent.transform.childCount; i++)
        {
            Transform currentChair = chairParent.transform.GetChild(i);
            for (int j = 0; j < currentChair.GetComponent<Chair>().chairMeshes.Count; j++)
            {
                Debug.Log("salon level " + PlayerPrefs.GetInt("salonLevel"));
                if (j == PlayerPrefs.GetInt("salonLevel"))
                {
                    Debug.Log("Enabling mesh " + currentChair.GetComponent<Chair>().chairMeshes[salonLevel] + " Of chair set " + currentChair.name + " Chair mesh ");
                    currentChair.GetComponent<Chair>().chairMeshes[j].SetActive(true);

                }
                else
                {
                    Debug.Log("Disabling mesh " + currentChair.GetComponent<Chair>().chairMeshes[salonLevel] + " Of chair set " + currentChair.name + " Chair mesh ");
                    currentChair.GetComponent<Chair>().chairMeshes[j].SetActive(false);
                }
            }

        }
    }
    public List<Chair> GetTasks()
    {
        List<Chair> tasks = new List<Chair>();
        for (int i = 0; i < chairParent.transform.childCount; i++)
        {
            if (chairParent.transform.GetChild(i).GetComponent<Chair>().hasCustomer)
            {
                tasks.Add(chairParent.transform.GetChild(i).GetComponent<Chair>());
            }
            
        }
        return tasks;
    }



}
