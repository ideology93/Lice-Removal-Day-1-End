using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombController : GloballyAccessibleBase<CombController>
{
    public int part;

    public List<GameObject> combs = new List<GameObject>();
    public List<GameObject> hands = new List<GameObject>();
    public GameObject currentComb, currentHand;
    private bool hasMoved;
    private bool isFinished;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CombController.Instance.StartCombing();
        }
    }
    public void StartCombing()
    {
        float half = hands.Count / 2;
        if (part >= half && !hasMoved)
        {

            hasMoved = true;
            CameraController.Instance.RightCombingCamera();
        }

        if (part - 1 >= 0)
        {
            hands[part - 1].SetActive(false);
            hands[part - 1].GetComponent<Animator>().enabled = false;
            combs[part - 1].SetActive(false);
            combs[part - 1].GetComponent<Animator>().enabled = false;

        }
        if (part >= hands.Count)
        {
            isFinished = true;
            UI_Manager.Instance.guide_Comb.SetActive(false);
            CameraController.Instance.SittingCamera(0.1f);
            // UI_Manager.Instance.HappyEmoji.Play();
            if (CharacterGenerator.Instance.characterGameObject.GetComponent<Character>().characterGender == Character.CharacterGender.Female)
            {
                StartCoroutine(SmashPhase());
            }
            else
                StartCoroutine(VacuumPhase());
            return;
        }
        if (!isFinished)
        {
            hands[part].SetActive(true);
            hands[part].GetComponent<Animator>().enabled = true;
            part++;
        }
        // combs[part].SetActive(true);
        // combs[part].GetComponent<Animator>().enabled = true;

    }
    public IEnumerator VacuumPhase()
    {
        UI_Manager.Instance.HappyEmoji.Play();
        yield return new WaitForSeconds(1f);
        CameraController.Instance.VacuumCamera();
        RaycastController.Instance.combRaycaster.enabled = false;
        yield return new WaitForSeconds(0.2f);
        UI_Manager.Instance.guide_Vacuum.SetActive(true);
        GameManager.Instance.vacuumPhase.SetActive(true);
        UI_Manager.Instance.vacuumPanel.SetActive(true);
        ToolManager.Instance.vacuum.SetActive(true);
        CharacterGenerator.Instance.currentHairScript.fuzzyHairParent.SetActive(false);
        UI_Manager.Instance.step1.SetActive(false);
        UI_Manager.Instance.step2.SetActive(true);

    }
    public IEnumerator SmashPhase()
    {
        UI_Manager.Instance.HappyEmoji.Play();
        yield return new WaitForSeconds(1f);
        CameraController.Instance.SmashCamera();
        RaycastController.Instance.combRaycaster.enabled = false;
        UI_Manager.Instance.guide_Smash.SetActive(true);
        UI_Manager.Instance.smashPanel.SetActive(true);
        RaycastController.Instance.smashRaycaster.enabled = true;
    }

}
