using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class FloatingCash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(Idle_UIManager.Instance.cashIcon.transform.position, 1f).OnComplete(() => TweenComplete());
    }
    public void TweenComplete()
    {
        IdleManager.Instance.GetMoney(100);
        Destroy(this.gameObject);

    }

}
