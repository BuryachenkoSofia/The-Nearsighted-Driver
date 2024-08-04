using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrintingText : MonoBehaviour
{
    public TMP_Text displayText;
    private float displayDuration = 4f;
    private float typeDelay = 0.02f;

    private void Start()
    {
        StartCoroutine(DisplayTextSequence());
    }

    private IEnumerator DisplayTextSequence()
    {
            string text = "Due to damage to your car, you were unable to drive further and were caught by the police.";
            displayText.text = "";
            for (int i = 0; i < text.Length; i++)
            {
                displayText.text += text[i];
                yield return new WaitForSecondsRealtime(typeDelay);
            }
            yield return new WaitForSecondsRealtime(displayDuration);
            displayText.color = new Color(displayText.color.r, displayText.color.g, displayText.color.b, 1f);
    }
}
