using System.Collections.Generic;
using UnityEngine;

public class Nail : MonoBehaviour
{
    public Transform StartPos;
    public Transform EndPos;
    
    public SpriteRenderer SpriteRenderer;

    public Sprite BlueNail;
    public Sprite BlackNail;
    public Sprite PinkNail;

    private Dictionary<PolishColor, Sprite> _nailSprites;

    public void Init()
    {
        SpriteRenderer.color = Color.clear;

        _nailSprites = new Dictionary<PolishColor, Sprite>()
        {
            { PolishColor.Blue, BlueNail },
            { PolishColor.Black, BlackNail },
            { PolishColor.Pink, PinkNail }
        };
    }
    
    public bool IsInside(Vector2 pos)
    {
        return SpriteRenderer.bounds.Contains(pos);
    }

    public void SetColor(PolishColor polishColor)
    {
        SpriteRenderer.color = Color.white;
        SpriteRenderer.sprite = _nailSprites[polishColor];
    }

    private void Reset()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
