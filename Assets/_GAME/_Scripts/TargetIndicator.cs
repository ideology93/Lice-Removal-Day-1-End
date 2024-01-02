using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class TargetIndicator : MonoBehaviour
{
    private float q = 0.0f;
    public Queue<Chair> chairQueue = new Queue<Chair>();
    public List<Chair> chairs = new List<Chair>();
    public MeshRenderer arrowMesh;
    public GameObject arrow;
    private float turnSpeed = 100000000f;
    private Transform target;
    private int currentTargetNumber, totalTargets;
    private void LateUpdate()
    {
        Look();
    }
    private void Start()
    {
        StartCoroutine(GetTasks());
    }
    public IEnumerator GetTasks()
    {
        yield return new WaitForSeconds(1f);
        chairs = IdleManager.Instance.GetTasks();
        totalTargets = chairs.Count;
        if (chairs.Count > 0)
        {
            currentTargetNumber = 0;
            target = chairs[currentTargetNumber].target;
        }
        else arrowMesh.enabled=false;
            

    }
    [Button]
    public void ReturnNewTask()
    {
        // if (currentTargetNumber < chairs.Count - 1)
        // {
        //     currentTargetNumber++;
        //     target = chairs[currentTargetNumber].transform;
        //     Look();
        // }
        // else
        //     Debug.Log("No more targets");
        if (chairs.Count > 0)
        {
            currentTargetNumber = 0;
            target = chairs[currentTargetNumber].target;
            arrowMesh.enabled = true;
            Look();
        }
        else
        {

            Debug.Log("No more targets");
            arrowMesh.enabled = false;
        }

    }
    [Button]
    public void DequeueChair()
    {
        chairs.Remove(chairs[0]);
    }
    private void Look()
    {
        if (target != null)
        {

            Vector3 dir = target.position - transform.position;
            float singleStep = turnSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.transform.forward, dir, singleStep, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
    void OnDrawGizmosSelected()
    {

    }
    private void OnDrawGizmos()
    {
        if (target != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }

}
