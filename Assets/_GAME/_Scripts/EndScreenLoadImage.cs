
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class EndScreenLoadImage : MonoBehaviour
{
    public SR_RenderCamera start;
    public Image imageBefore, imageAfter;
    public GameObject nextCustomer, cashImg;
    public Sprite sprite_before, sprite_after;
    public TMP_Text likes;
    private void Start()
    {
        
        UI_Manager.Instance.step4.SetActive(false);
        UI_Manager.Instance.extensionSlider.gameObject.SetActive(false);
        UI_Manager.Instance.glow.gameObject.SetActive(false);
        PlayerPrefs.SetInt("session", 1);
        UI_Manager.Instance.guide_Spray.SetActive(false);
        UI_Manager.Instance.overlayPanel.SetActive(true);
        UI_Manager.Instance.cashText.text = "$ "+UI_Manager.Instance.ShortNotation(PlayerPrefs.GetInt("cash".ToString()));
        StartCoroutine(AnimateText());
        imageBefore.sprite = sprite_before;
        imageAfter.sprite = sprite_after;
        GameManager.Instance.IncreasePopularity();
        GameManager.Instance.IncreaseMissionID();
    }
    IEnumerator AnimateText()
    {
        int startLikes = 0;
        int likesNumb = Random.Range(50, 1000);
        likes.text = "0";
        yield return new WaitForSeconds(0.75f);
        while (startLikes < likesNumb)
        {
            startLikes += 6;
            likes.text = startLikes.ToString();
            yield return null;
        }
        cashImg.SetActive(true);
        nextCustomer.SetActive(true);
        AnimateMoney(500, "+ $", true);

    }
    public void AnimateMoney(int amount, string direction, bool gain)
    {
        
        if (gain)
        {
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + amount);
        }
        else
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") - amount);
        UI_Manager.Instance.cashText.text = "$ " + UI_Manager.Instance.ShortNotation(PlayerPrefs.GetInt("cash"));
        UI_Manager.Instance.cashText.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.3f), 0.3f, 5, 0);
        UI_Manager.Instance.cashIcon.transform.DOPunchScale(new Vector3(0.15f, 0.15f, 0.15f), 0.3f, 5, 0);
        TMP_Text floatingText = Instantiate(UI_Manager.Instance.cashText, UI_Manager.Instance.cashText.transform.position, Quaternion.identity, UI_Manager.Instance.cashIcon.transform);
        floatingText.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 50);
        floatingText.GetComponent<TextMeshProUGUI>().color = Color.white;
        floatingText.text = direction + amount.ToString();
        floatingText.transform.DOMove(floatingText.transform.position + new Vector3(0, 120, 0), 1f);
        Destroy(floatingText, 1.01f);
    }

}
