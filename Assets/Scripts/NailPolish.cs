using UnityEngine;

public class NailPolish : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Color PolishColor;
    
    public bool IsInside(Vector2 pos)
    {
        return SpriteRenderer.bounds.Contains(pos);
    }
}
