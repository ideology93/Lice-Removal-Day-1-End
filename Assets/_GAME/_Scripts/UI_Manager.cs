using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UI_Manager : GloballyAccessibleBase<UI_Manager>
{
    public GameObject arrowShop, arrowLookBook;
    public TMP_Text followersText, postsText;
    public GameObject lookbookContent;
    public bool isFirstTime;
    public TMP_Text cashText;
    public GameObject lookbookButton;
    public GameObject cashIcon, shopIcon;
    public GameObject overlayPanel, tutorialPanel, startPanel, treatmentPanel, garbagePanel, fuzzPanel, liquidPanel, vacuumPanel, gelPanel, extensionPanel, endPanel, smashPanel;
    public GameObject scanButton, combButton, dyeButton, washButton, fuzzButton;
    public int fuzzPart, garbageRemoved, fuzzRemoved;
    public float trashPart;
    public Slider garbageSlider, fuzzSlider, liquidSlider, vacuumSlider, gelSlider, extensionSlider, smashSlider;
    public ParticleSystem HappyEmoji, glow;
    public GameObject guide_Garbage, guide_Fuzz, guide_Comb, guide_Vacuum, guide_Spray, guide_Liquid, guide_Smash, guideSmash2, guideScan, guideMonitor;
    public GameObject step0, step1, step2,step3, step4;
    public int first;
    private void Start()
    {
        first = PlayerPrefs.GetInt("first", 1);
        if (first == 1)
        {
            shopIcon.SetActive(false);

        }
        else
            shopIcon.SetActive(true);

    }
    public string ShortNotation(int number)
    {

        if (number >= 1000)
            return (number / 1000D).ToString("F2") + "K";

        return number.ToString();
    }
    public void NotAFirstTime()
    {
        PlayerPrefs.SetInt("first", 0);
    }
}