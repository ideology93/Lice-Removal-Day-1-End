using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
public class CameraController : GloballyAccessibleBase<CameraController>
{
    private Transform mainCamera;

    [SerializeField] private Transform startScanCamera, sittingCamera, scanCamera, paintingCamera, vacuumCamera, leftCombing, rightCombing, extensionCamera, smashCamera;
    public Transform instagramCamera;

    private void Start()
    {
        mainCamera = Camera.main.transform;
    }

    public void SittingCamera(float delay)
    {
        StartCoroutine(DelaySittingCamera(delay));
        // SetCamera(sittingCamera, 0.3f);
    }
    public void SittingCamera(GameEvent gameEvent)
    {
        SetCamera(sittingCamera, 0.3f, gameEvent);
    }
    public void LeftCombingCamera()
    {
        SetCamera(leftCombing, 1f);
    }
    public void RightCombingCamera()
    {
        SetCamera(rightCombing, 0.5f);
    }
    public void ExtensionCamera()
    {
        SetCamera(extensionCamera, 0.5f);
    }

    public IEnumerator DelaySittingCamera(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetCamera(sittingCamera, 0.3f);
    }

    public void ScanCamera(float delay)
    {
        StartCoroutine(DelayScanCamera(delay));
    }
    public void ScanCamera(GameEvent gameEvent)
    {
        SetCamera(scanCamera, 1f, gameEvent);
        gameEvent.Raise();
    }
    public IEnumerator DelayScanCamera(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetCamera(scanCamera, 1f);

    }
    public IEnumerator DelayStartScanCamera(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetCamera(startScanCamera, 1f);

    }
    public void StartScanCamera()
    {
        SetCamera(startScanCamera, 1f);
    }
    public void StartScanCamera(float delay)
    {
        StartCoroutine(DelayStartScanCamera(delay));
    }
    public void StartScanCamera(GameEvent gameEvent)
    {
        SetCamera(startScanCamera, 0.5f, gameEvent);
    }
    public void VacuumCamera()
    {
        SetCamera(vacuumCamera, 1f);
    }
    public void PaintingCamera()
    {
        SetCamera(paintingCamera, 1f);
    }
    public void SmashCamera()
    {
        SetCamera(smashCamera, 1f);

    }
    public void SetCamera(Transform camera, float duration)
    {
        mainCamera.DOMove(camera.position, duration);
        mainCamera.DORotate(camera.rotation.eulerAngles, duration);
    }
    public void SetCamera(Transform camera, float duration, GameEvent gameEvent)
    {
        mainCamera.DOMove(camera.position, duration);
        mainCamera.DORotate(camera.rotation.eulerAngles, duration).OnComplete(() => RaiseEvent(gameEvent));
    }
    public void RaiseEvent(GameEvent gameEvent)
    {
        gameEvent.Raise();
    }
}
