using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeechManager : MonoBehaviour
{
    public SpeechBubble SpeechBubbleNegative;
    public SpeechBubble SpeechBubbleNeutral;
    public SpeechBubble SpeechBubblePositive;
    
    private Dictionary<SpeechType, int> _lastLineIdxs = new();
    private Dictionary<SpeechType, SpeechBubble> _speechBubbles = new();

    private const float Margin = 50;

    public void Init()
    {
        _lastLineIdxs.Add(SpeechType.Negative, 0);
        _lastLineIdxs.Add(SpeechType.Neutral, 0);
        _lastLineIdxs.Add(SpeechType.Positive, 0);
        _speechBubbles.Add(SpeechType.Negative, SpeechBubbleNegative);
        _speechBubbles.Add(SpeechType.Neutral, SpeechBubbleNeutral);
        _speechBubbles.Add(SpeechType.Positive, SpeechBubblePositive);
        SpeechBubbleNegative.gameObject.SetActive(false);
        SpeechBubbleNeutral.gameObject.SetActive(false);
        SpeechBubblePositive.gameObject.SetActive(false);
    }
    
    public void RunLine(SpeechType speechType)
    {
        var idx = _lastLineIdxs[speechType];
        var lines = SpeechContainer.GetLines(speechType);
        var line = lines[idx];
        ++idx;
        _lastLineIdxs[speechType] = idx % lines.Length;

        var bubble = _speechBubbles[speechType];
        bubble.gameObject.SetActive(true);
        StartCoroutine(bubble.ShowLine(line));
    }
}
