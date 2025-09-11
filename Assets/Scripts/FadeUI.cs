using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadingcanvasGroup;

    public GameManager gameManager;

    private bool isFaded = true;

    // fade in the correct canvas group depending on the season
    // called from game manager when season changes
    
    public IEnumerator Fader()
    {
        isFaded = !isFaded;

        if (isFaded)
        {
            //.setactive(true);
            fadingcanvasGroup.DOFade(1, 2);
            // wait for 2 seconds
            yield return new WaitForSeconds(2);
        }
        else
        {
            fadingcanvasGroup.DOFade(0, 2);
            //.setactive(false);    
        }
    }
}
