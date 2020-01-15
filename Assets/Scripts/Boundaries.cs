using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Material boundaryMaterial;
    public Material wallMaterial;
    public string playerTag;
    [Range(0.0f, 2.0f)]
    public float fadeSpeed;
    Color boundaryColor;
    float alpha;

    // Start is called before the first frame update
    void Start()
    {
        boundaryColor = boundaryMaterial.color;
        alpha = boundaryColor.a;
        SetAlpha(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            StartCoroutine(FadeImage(false));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == playerTag)
        {
            StartCoroutine(FadeImage(true));
        }
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from visible to invisible
        if (fadeAway)
        {
            for (float i = alpha; i >= 0; i -= fadeSpeed*Time.deltaTime)
            {
                SetAlpha(i);
                yield return null;
            }
            SetAlpha(0);
        }
        // fade from invisible to visible
        else
        {
            for (float i = 0; i <= alpha; i += fadeSpeed*Time.deltaTime)
            {
                SetAlpha(i);
                yield return null;
            }
            SetAlpha(alpha);
        }
    }

    private void SetAlpha(float i)
    {
        boundaryMaterial.color = new Color(boundaryColor.r, boundaryColor.g, boundaryColor.b, i);
        wallMaterial.color = new Color(0, 0, 0, i*2);
    }

    private void OnApplicationQuit()
    {
        boundaryMaterial.color = boundaryColor;
    }
}
