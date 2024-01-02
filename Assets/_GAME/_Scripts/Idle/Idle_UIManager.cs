using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class Idle_UIManager : GloballyAccessibleBase<Idle_UIManager>
{
    public ParticleSystem poof;
    public GameObject cashFloater;
    public TMP_Text cashText;
    public GameObject cashIcon, cashLabel;
    public GameObject panelWork, panelUpgrade, panelJoystick;
    public string ShortNotation(int number)
    {

        if (number >= 1000)
            return (number / 1000D).ToString("F2") + "K";

        return number.ToString();
    }
    
}
