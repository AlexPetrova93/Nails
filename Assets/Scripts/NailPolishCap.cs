using System.Collections;
using DG.Tweening;
using UnityEngine;

public class NailPolishCap : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    public Sprite PaintingSprite;

    private Sprite _defaultSprite;
    private Vector3 _defaultPos;
    
    public bool Selectable { get; private set; }
    
    public void Init()
    {
        _defaultPos = transform.position;
        _defaultSprite = SpriteRenderer.sprite;
        Selectable = true;
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

    public void PaintNail(Nail nail)
    {
        StartCoroutine(PaintNailRoutine(nail));
    }

    private IEnumerator PaintNailRoutine(Nail nail)
    {
        Selectable = false;
        SpriteRenderer.sprite = PaintingSprite;

        var time = .4f;
        transform.DOKill();
        transform.position = nail.StartPos.position + Vector3.down * .2f;
        transform.DOMove(nail.EndPos.position + Vector3.down * .1f, time);
        yield return new WaitForSeconds(time);
        
        SpriteRenderer.sprite = _defaultSprite;
        yield return new WaitForSeconds(.1f);
        transform.position = _defaultPos + Vector3.up * .5f;
        Selectable = true;
    }
    
    private void Reset()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
}
