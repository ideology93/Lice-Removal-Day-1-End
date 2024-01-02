using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
public class PlayerController : MonoBehaviour
{
    public Transform testTarget;
    public bool isWorking;
    public Transform head;
    public bool isGrabbing;
    public Transform gizmoTarget;
    public FloatingJoystick floatingJoystick;
    public Animator playerAnimator;
    public CharacterController controller;
    private Vector3 playerVelocity;
    public bool groundedPlayer;

    public bool isMoving;
    public float playerSpeed = 1.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    private bool working;



    void LateUpdate()
    {
        if (working)
        {
            RotateToChair();
        }
    }
    void Update()
    {

        if (transform.position.y < 2)
        {
            groundedPlayer = true;
        }
        Vector3 move = Vector3.forward * floatingJoystick.Vertical + Vector3.right * floatingJoystick.Horizontal;

        if (move != Vector3.zero)
        {
            isMoving = true;
            controller.Move(move * Time.deltaTime * playerSpeed);
            gameObject.transform.forward = move;
            playerAnimator.Play("Walk");
        }
        else
        {
            isMoving = false;
            if (!isGrabbing)
            {
                playerAnimator.Play("Idle");
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void ToggleGrab()
    {
        isGrabbing = false;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Cash")
        {
            hit.transform.GetComponent<Collider>().enabled = false;
            hit.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
            GameObject cash = Instantiate(Idle_UIManager.Instance.cashFloater,
             Idle_UIManager.Instance.cashFloater.transform.position,
              Quaternion.identity,
              Idle_UIManager.Instance.cashFloater.transform.parent);
            cash.SetActive(true);
            Destroy(hit.gameObject, 0.51f);
        }

    }
    [Button]
    public void Work()
    {
        working = true;
    }
    public void RotateToChair()
    {
        Debug.DrawLine(Vector3.zero, new Vector3(5, 0, 0), Color.white, 2.5f);
        testTarget = IdleManager.Instance.currentChair.target.transform;
        gizmoTarget = testTarget;
        // Vector3 startPos = transform.position;
        // Vector3 startRot = transform.rotation.eulerAngles;
        // Vector3 dir = testTarget.position - transform.position;
        // float singleStep = 10000000f * Time.deltaTime;
        // Vector3 newDir = Vector3.RotateTowards(transform.forward, dir, singleStep, 0.0f);
        transform.LookAt(new Vector3(testTarget.position.x, transform.position.y, testTarget.position.z));
        // transform.rotation = Quaternion.LookRotation(newDir);
        // transform.position = startPos;
        // transform.rotation = Quaternion.Euler(startRot.x, transform.rotation.y, startRot.z);
        // Debug.Log("Rotation is:  X- " + transform.rotation.x + " Y- " + transform.rotation.eulerAngles.y + " Z- " + transform.rotation.z);
    }
    private void OnDrawGizmos()
    {
        if (gizmoTarget != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, gizmoTarget.position);

        }
    }

}
