using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HoverUIEffect : MonoBehaviour
{

    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float tweenTime = 0.15f;
    [SerializeField] private LeanTweenType easeType = LeanTweenType.easeOutSine;


    public void OnHoverEnterEffect(GameObject button)
    {
        LeanTween.cancel(button);
        LeanTween.scale(button, Vector2.one * hoverScale, tweenTime).setEase(easeType);
    }

    public void OnHoverExitEffect(GameObject button)
    {
        LeanTween.cancel(button);
        LeanTween.scale(button, Vector2.one, tweenTime).setEase(LeanTweenType.easeOutBack);
    }

    public void OnClickEffect(GameObject button)
    {
        LeanTween.cancel(button);
        LeanTween.scale(button, Vector2.one * 0.95f, tweenTime * 0.5f)
            .setEase(LeanTweenType.easeOutSine)
            .setOnComplete(() =>
            {
                LeanTween.scale(button, Vector2.one, tweenTime).setEase(LeanTweenType.easeOutBack);
            });
    }
}
