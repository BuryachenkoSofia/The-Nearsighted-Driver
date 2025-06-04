using System.Collections;
using UnityEngine;
using TMPro;

public class PrintingText : MonoBehaviour
{
    private float displayDuration = 4f;
    private float typeDelay = 0.02f;
    [SerializeField] private TMP_Text displayText;

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