using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HoverUIEffect : MonoBehaviour
{
    public void OnHoverEnterEffect(GameObject button)
    {
        button.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public void OnHoverExitEffect(GameObject button)
    {
        button.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
