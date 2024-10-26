using System;
using UnityEngine;

public enum PolishColor { Blue, Black, Pink }

public class NailPolish : MonoBehaviour
{
    public NailPolishCap PolishCap;
    
    public SpriteRenderer SpriteRenderer;
    public PolishColor PolishColor;

    public void Init()
    {
        PolishCap.Init();
    }
    
    public bool IsInside(Vector2 pos)
    {
        return SpriteRenderer.bounds.Contains(pos) || PolishCap.SpriteRenderer.bounds.Contains(pos);
    }

    public void Select() => PolishCap.Select();

    public void Deselect() => PolishCap.Deselect();

    private void Reset()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
