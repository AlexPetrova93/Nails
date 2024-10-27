using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubble : MonoBehaviour
{
    public SpeechType SpeechType;
    public Image SpeechBubbleBg;
    public Image SpeechArrow;
    public TextMeshProUGUI SpeechText;

    private const float MinWidth = 350;
    private const float MaxWidth = 575;
    private const float MinHeight = 30;
    private const float MaxHeight = 100;
    
    private const float TextMargin = 50;
    
    public IEnumerator ShowLine(string line)
    {
        SpeechText.rectTransform.sizeDelta = new Vector2(MaxWidth, MaxHeight);
        SpeechText.text = line;
        SpeechText.ForceMeshUpdate(true, true);
        var textSize = SpeechText.textBounds.size;
        textSize.y = Mathf.Max(MinHeight, textSize.y);
        SpeechText.rectTransform.sizeDelta = textSize;
        SpeechText.rectTransform.anchoredPosition = new Vector2(TextMargin, -TextMargin);
        
        var bubbleSize = new Vector2(textSize.x + 2*TextMargin, textSize.y + 2*TextMargin);
        SpeechBubbleBg.rectTransform.sizeDelta = bubbleSize;
        
        SpeechText.text = "";
        for (var i = 0; i < line.Length; i++)
        {
            SpeechText.text += line[i];
            yield return new WaitForSeconds(.03f);
        }
        SpeechText.text = line;
        
    }
}
