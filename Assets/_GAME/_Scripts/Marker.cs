using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyLabsExtension;

public class Marker : MonoBehaviour
{
    public bool isUnlocked;
    public ParticleSystem ps;
    public Chair chair;
    public Material Mat;
    public GameObject fill;
    public Animator fillAnimator;
    public Material marker_Material;

    public void SetMarkerMaterial()
    {
        marker_Material = Mat;
        fill.gameObject.GetComponent<SpriteRenderer>().material = marker_Material;
    }
    public void Buy()
    {
        IdleManager.Instance.BuyChair();
        chair.SetChair();
        Instantiate(ps, transform.position, Quaternion.identity);
        chair.isUnlocked = true;
        HideMarker();
        IdleManager.Instance.unlockedChairs.Add(chair);
        HapticFeedbackController.TriggerHaptics(Lofelt.NiceVibrations.HapticPatterns.PresetType.SoftImpact);
        AudioManager.Instance.PlaySound(0);
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && IdleManager.Instance.cash >= 500)
        {
            fillAnimator.SetBool("flag", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        fillAnimator.SetBool("flag", false);
    }
    private void HideMarker()
    {
        chair.unlocked.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void GenerateChair()
    {
        if (isUnlocked)
        {
            HideMarker();
        }
        else
        {
            SetMarkerMaterial();
        }
    }

}
