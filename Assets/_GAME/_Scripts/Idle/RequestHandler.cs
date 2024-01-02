using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrazyLabsExtension;
public class RequestHandler : MonoBehaviour
{
    public int id;

    public void Work()
    {
        if (IdleManager.Instance.currentRequest == id)
        {
            // IdleManager.Instance.currentChair.canvas.gameObject.SetActive(false);
            // IdleManager.Instance.currentChair.marker.SetActive(false);

            // IdleManager.Instance.currentChair.chairCollider.enabled=false;
            Debug.Log("ID correct" + " In manager" + IdleManager.Instance.currentChair.currentRequest + " And in handler " + id);
            IdleManager.Instance.player.GetComponent<PlayerController>().isGrabbing = true;
            IdleManager.Instance.player.GetComponent<Animator>().Play("Grab");
            IdleManager.Instance.playerController.RotateToChair();
            Idle_UIManager.Instance.panelJoystick.SetActive(false);
            StartCoroutine(Correct());
            Debug.Log("Playing");
        }
        else
        {
            // IdleManager.Instance.currentChair.requestSprite.transform.parent.parent.gameObject.SetActive(false);
            //  IdleManager.Instance.currentChair.marker.SetActive(false);
            // IdleManager.Instance.currentChair.chairCollider.enabled=false;
            IdleManager.Instance.playerController.RotateToChair();
            IdleManager.Instance.player.GetComponent<PlayerController>().isGrabbing = true;
            IdleManager.Instance.player.GetComponent<Animator>().Play("Grab");
            Idle_UIManager.Instance.panelJoystick.SetActive(false);

            StartCoroutine(Incorrect());
        }
    }
    public IEnumerator Correct()
    {
        yield return new WaitForSeconds(3f);
        IdleManager.Instance.currentChair.happy.Play();
        IdleManager.Instance.currentChair.RemoveCustomer();
        Idle_UIManager.Instance.panelJoystick.SetActive(true);
        Idle_UIManager.Instance.panelWork.SetActive(false);
        SpawnCash();
        AudioManager.Instance.PlaySound(0);
        if (IdleManager.Instance.targetIndicator.chairs.Contains(IdleManager.Instance.currentChair))
        {
            IdleManager.Instance.targetIndicator.chairs.Remove(IdleManager.Instance.currentChair);
        }
        IdleManager.Instance.targetIndicator.ReturnNewTask();
        HapticFeedbackController.TriggerHaptics(Lofelt.NiceVibrations.HapticPatterns.PresetType.Success);
    }
    public IEnumerator Incorrect()
    {
        yield return new WaitForSeconds(3f);
        IdleManager.Instance.currentChair.angry.Play();
        IdleManager.Instance.currentChair.RemoveCustomer();
        Idle_UIManager.Instance.panelJoystick.SetActive(true);
        Idle_UIManager.Instance.panelWork.SetActive(false);
        if (IdleManager.Instance.targetIndicator.chairs.Contains(IdleManager.Instance.currentChair))
        {
            IdleManager.Instance.targetIndicator.chairs.Remove(IdleManager.Instance.currentChair);
        }
        IdleManager.Instance.targetIndicator.ReturnNewTask();
        HapticFeedbackController.TriggerHaptics(Lofelt.NiceVibrations.HapticPatterns.PresetType.Failure);
    }
    public void SpawnCash()
    {

        Vector3 pos2 = IdleManager.Instance.currentChair.transform.position + new Vector3(1, 0, 0);
        GameObject cash2 = Instantiate(IdleManager.Instance.cashPrefab,
        IdleManager.Instance.currentChair.moneyPos.position,
        IdleManager.Instance.cashPrefab.transform.rotation);
        cash2.SetActive(true);
    }

}
