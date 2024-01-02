using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;
    private bool hasMoved;
    private string main = "MainScene";
    private string idle = "IdleScene";

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        StartCoroutine(FadeIn());
    }
    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(main));
    }
    public void LoadMainScene()
    {
        StartCoroutine(FadeOut(main));
    }
    public void LoadIdleScene()
    {
        StartCoroutine(FadeOut(idle));
    }
    public void FadePanels()
    {
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        float t = 0.5f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(img.color.r, img.color.g, img.color.b, a);
            yield return 0;
        }

    }
    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(img.color.r, img.color.g, img.color.b, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
    IEnumerator FadePanel()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(img.color.r, img.color.g, img.color.b, a);
            yield return 0;
        }

    }


    public void Next()
    {
        FadeTo(SceneManager.GetActiveScene().name);
    }
}