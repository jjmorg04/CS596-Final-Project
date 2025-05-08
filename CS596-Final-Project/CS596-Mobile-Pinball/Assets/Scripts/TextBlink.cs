using TMPro;
using UnityEngine;
using System.Collections;


public class BlinkText : MonoBehaviour
{
    public TextMeshProUGUI textToBlink;
    public float blinkInterval = 0.5f;

    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
{
    while (true)
    {
        textToBlink.enabled = !textToBlink.enabled;
        yield return new WaitForSecondsRealtime(blinkInterval); // ✅ uses unscaled time
    }
}

}
