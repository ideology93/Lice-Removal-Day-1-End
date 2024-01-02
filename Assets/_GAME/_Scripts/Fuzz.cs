using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuzz : MonoBehaviour
{
    public GameEvent startLiquidEvent;
    public bool isRemoved;
    public bool isTargeted;
    public bool isReversed;
    public SkinnedMeshRenderer currentFuzz;
    public float startShape, endShape, currentShape;
    public int fuzzPart;
    public bool isDragging;
    public bool isPhasing;

    private void Start()
    {
        fuzzPart = CharacterGenerator.Instance.currentHairScript.FuzzPart;
        startLiquidEvent = ToolManager.Instance.liquidEvent;
        currentFuzz = GetComponent<SkinnedMeshRenderer>();
        startShape = currentFuzz.GetBlendShapeWeight(0);
        if (startShape == 100)
        {
            endShape = 0;
            isReversed = true;
        }
        else
            endShape = 100;

    }

    public void RemoveFuzzy()
    {
        StartCoroutine(RemoveFuzzyCoroutine());
    }
    public IEnumerator RemoveFuzzyCoroutine()
    {

        currentShape = currentFuzz.GetBlendShapeWeight(0);
        if (!isReversed)
        {
            if (isTargeted && isDragging)
            {
                currentFuzz.SetBlendShapeWeight(0, currentShape + 8);
                currentShape = currentFuzz.GetBlendShapeWeight(0);
                yield return new WaitForSeconds(0.05f);
                if (currentShape <= endShape)
                {
                    // this.gameObject.SetActive(false);
                    UI_Manager.Instance.fuzzRemoved++;
                    UI_Manager.Instance.fuzzSlider.value += fuzzPart;

                    if (UI_Manager.Instance.fuzzSlider.value >= 88)
                    {
                        if (CharacterGenerator.Instance.characterGameObject.GetComponent<Character>().characterGender == Character.CharacterGender.Female)
                        {
                            UI_Manager.Instance.fuzzSlider.value = 100;
                            startLiquidEvent.Raise();
                            UI_Manager.Instance.fuzzPanel.SetActive(false);
                            UI_Manager.Instance.HappyEmoji.Play();
                            RaycastController.Instance.fuzzRaycaster.enabled = false;
                            GameManager.Instance.fuzzPhase.SetActive(false);
                            GameManager.Instance.LiquidPhase.SetActive(true);
                            UI_Manager.Instance.guide_Fuzz.SetActive(false);
                            AudioManager.Instance.StopSound(1);


                            for (int i = 0; i < CharacterGenerator.Instance.currentHairScript.fuzzyHairParent.transform.childCount; i++)
                            {
                                CharacterGenerator.Instance.currentHairScript.fuzzyHairParent.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().enabled = false;
                            }
                        }
                        else
                        {

                            isPhasing = true;
                            AudioManager.Instance.StopSound(1);
                            UI_Manager.Instance.HappyEmoji.Play();
                            StartCoroutine(Vacuum());
                        }
                    }
                    if (!isPhasing)
                    {


                        RaycastController.Instance.fuzzRaycaster.enabled = true;
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            if (isTargeted && isDragging)
            {
                currentFuzz.SetBlendShapeWeight(0, currentShape - 8);
                currentShape = currentFuzz.GetBlendShapeWeight(0);
                yield return new WaitForSeconds(0.05f);

                if (currentShape <= endShape)
                {
                    // this.gameObject.SetActive(false);
                    UI_Manager.Instance.fuzzRemoved++;
                    UI_Manager.Instance.fuzzSlider.value += fuzzPart;

                    if (UI_Manager.Instance.fuzzSlider.value >= 88)
                    {
                        if (CharacterGenerator.Instance.characterGameObject.GetComponent<Character>().characterGender == Character.CharacterGender.Female)
                        {
                            UI_Manager.Instance.fuzzSlider.value = 100;
                            startLiquidEvent.Raise();
                            UI_Manager.Instance.fuzzPanel.SetActive(false);
                            UI_Manager.Instance.HappyEmoji.Play();
                            RaycastController.Instance.fuzzRaycaster.enabled = false;
                            GameManager.Instance.fuzzPhase.SetActive(false);
                            GameManager.Instance.LiquidPhase.SetActive(true);
                            UI_Manager.Instance.guide_Fuzz.SetActive(false);
                            AudioManager.Instance.StopSound(1);
                            for (int i = 0; i < CharacterGenerator.Instance.currentHairScript.fuzzyHairParent.transform.childCount; i++)
                            {
                                CharacterGenerator.Instance.currentHairScript.fuzzyHairParent.transform.GetChild(i).GetComponent<SkinnedMeshRenderer>().enabled = false;
                            }
                        }
                        else
                        {
                            isPhasing = true;
                            UI_Manager.Instance.HappyEmoji.Play();
                            StartCoroutine(Vacuum());
                            AudioManager.Instance.StopSound(1);

                        }
                    }
                    if (!isPhasing)
                    {


                        RaycastController.Instance.fuzzRaycaster.enabled = true;
                        this.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    public IEnumerator Vacuum()
    {

        RaycastController.Instance.fuzzRaycaster.enabled = false;

        yield return new WaitForSeconds(1f);

        CameraController.Instance.VacuumCamera();

        RaycastController.Instance.combRaycaster.enabled = false;

        yield return new WaitForSeconds(0.2f);

        UI_Manager.Instance.guide_Vacuum.SetActive(true);

        GameManager.Instance.vacuumPhase.SetActive(true);

        UI_Manager.Instance.vacuumPanel.SetActive(true);

        ToolManager.Instance.vacuum.SetActive(true);

        GameManager.Instance.fuzzPhase.SetActive(false);
        UI_Manager.Instance.fuzzPanel.SetActive(false);

        UI_Manager.Instance.guide_Fuzz.SetActive(false);
        UI_Manager.Instance.step1.SetActive(false);
        UI_Manager.Instance.step2.SetActive(true);

        CharacterGenerator.Instance.currentHairScript.liceParent.SetActive(false);
        CharacterGenerator.Instance.currentHairScript.fuzzyHairParent.SetActive(false);
    }
}

