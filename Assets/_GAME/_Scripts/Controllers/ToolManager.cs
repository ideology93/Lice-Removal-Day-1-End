using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ToolManager : GloballyAccessibleBase<ToolManager>
{
    public GameEvent scanEvent, liquidEvent;
    public GameObject tweezers, scanner, monitor, vacuum, shower, steam, bottle, foam;

    public void StartScan()
    {

    }
    public void ScanResponse()
    {

        CameraController.Instance.StartScanCamera(0f);
        scanner.GetComponent<Move>().PlayTween();
        // monitor.GetComponent<Move>().PlayTween();
    }
    public void ScanResponseV2()
    {
        CameraController.Instance.ScanCamera(1f);
        scanner.GetComponent<Move>().PlayTween();
        monitor.GetComponent<Move>().PlayTween();
    }
}
