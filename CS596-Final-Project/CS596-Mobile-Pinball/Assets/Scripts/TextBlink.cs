using TMPro;
using UnityEngine;
using System.Collections;

public class BlinkText : MonoBehaviour
{
    public TextMeshProUGUI textToBlink;
    public float blinkInterval = 0.5f;

    private Coroutine blinkRoutine;

    void OnEnable()
    {
        blinkRoutine = StartCoroutine(Blink());
    }

    void OnDisable()
    {
        if (blinkRoutine != null)
            StopCoroutine(blinkRoutine);
    }

    IEnumerator Blink()
    {
        while (true)
        {
            textToBlink.enabled = !textToBlink.enabled;
            yield return new WaitForSecondsRealtime(blinkInterval); // ✅ works even if timeScale = 0
        }
    }
}

