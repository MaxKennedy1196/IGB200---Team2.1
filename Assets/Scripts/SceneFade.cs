using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;

public class SceneFade : MonoBehaviour
{
    private Image sceneFadeImage;

    private void Awake()
    {
        sceneFadeImage = GetComponent<Image>();
    }

    public IEnumerator FadeInCoroutine(float duration)
    {
        Color startColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1f);
        Color endColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0f);

        yield return FadeCoroutine(startColor, endColor, duration);

        gameObject.SetActive(false);
    }

    public IEnumerator FadeOutCoroutine(float duration)
    {
        Color startColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0f);
        Color endColor = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1f);

        gameObject.SetActive(true);
        yield return FadeCoroutine(startColor, endColor, duration);
    }

    private IEnumerator FadeCoroutine(Color startColor, Color endColor, float duration)
    {
        float elapsedTime = 0f;
        float elapsedPercentage = 0f;

        while (elapsedPercentage < 1f)
        {
            elapsedPercentage = elapsedTime / duration;
            sceneFadeImage.color = Color.Lerp(startColor, endColor, elapsedPercentage);

            yield return null;
            elapsedTime += Time.deltaTime;
        }

    }
}
