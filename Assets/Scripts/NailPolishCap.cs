using DG.Tweening;
using UnityEngine;

public class NailPolishCap : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    public Sprite PaintingSprite;

    private Sprite _defaultSprite;
    private Vector3 _defaultPos;
    
    public void Init()
    {
        _defaultPos = transform.position;
        _defaultSprite = SpriteRenderer.sprite;
    }

    public void Select()
    {
        transform.DOKill();
        transform.DOMove(_defaultPos + Vector3.up * .5f, .3f);
    }

    public void Deselect()
    {
        transform.DOKill();
        transform.DOMove(_defaultPos, .3f);
    }
    
    public void StartPainting()
    {
        SpriteRenderer.sprite = PaintingSprite;
    }

    public void StopPainting()
    {
        SpriteRenderer.sprite = _defaultSprite;
    }
    
    private void Reset()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
