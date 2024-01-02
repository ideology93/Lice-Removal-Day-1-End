using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Money : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            transform.DOMove(other.transform.position, 0.5f);
            transform.DOScale(new Vector3(0, 0, 0), 0.5f);
            PlayerPrefs.SetInt("cash", PlayerPrefs.GetInt("cash") + 100);
            IdleManager.Instance.GetMoney(100);
            Destroy(this.gameObject, 0.55f);
        }
    }

}
