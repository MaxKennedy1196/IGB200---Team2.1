using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private Image SeasonUI;
    [SerializeField] public float fadeDuration;

    public System.Action OnFadeComplete;

    // fade in the correct canvas group depending on the season
    // called from game manager when season changes

    public IEnumerator FadeInAndOut(GameObject fadingObject)
    {
        //Debug.Log("RAAAAH");
        fadingObject.SetActive(true);
        /*
        var seasonColor = fadingObject.GetComponent<Image>().color;
        seasonColor = new Color(seasonColor.r, seasonColor.g, seasonColor.b, 0);
        fadingObject.GetComponent<Image>().color = seasonColor;
        */
        FadeIn();
        yield return new WaitForSeconds(4);
        FadeOut();
        yield return new WaitForSeconds(fadeDuration);

        OnFadeComplete?.Invoke();
        OnFadeComplete = null;
        fadingObject.SetActive(false);

        OnFadeComplete?.Invoke();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        Color color = SeasonUI.color;

        color.a = 0;

        SeasonUI.color = color;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = SmoothStep(0,1, elapsedTime /  fadeDuration);
            color.a = alpha;
            SeasonUI.color = color;
            yield return null;
        }

        color.a = 1;
        SeasonUI.color = color;
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;

        Color initialColor = SeasonUI.color;
        float initialAlpha = initialColor.a;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = SmoothStep(initialAlpha,1, elapsedTime / fadeDuration);
            initialColor.a = alpha;

            SeasonUI.color= initialColor;
            yield return null;
        }

        initialColor.a = 0;
        SeasonUI.color = initialColor;
    }


    private float SmoothStep(float start, float end, float value)
    {
        value = Mathf.Clamp01(value);
        return start + (end - start) * (value * value * (3f - 2f * value));
    }
}
