using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nail : MonoBehaviour
{
    private SpriteMask _mask;
    private SpriteRenderer _renderer;
    private Sprite _sprite;
    private Texture2D _paintTexture;

    private Color[] _texturePixels;
    
    private static readonly int MainTex = Shader.PropertyToID("_CustomTex");
    private Camera _cam;

    public void Init(Camera cam)
    {
        _cam = cam;
        _mask = GetComponent<SpriteMask>();
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _sprite = _renderer.sprite;
        var maskSprite = _mask.sprite; // size from mask, bc mask has the correct size !
        // TODO tady potencial na improvement - texture format less bits
        _paintTexture = new Texture2D(maskSprite.texture.width, maskSprite.texture.height, TextureFormat.RGBAFloat, true)
        {
            filterMode = FilterMode.Bilinear,
        };
        _texturePixels = new Color[_paintTexture.width * _paintTexture.height];
    }

    public void Paint(Vector2 pos, int brushSize, Color color)
    {
        var startX = (int)Mathf.Max(pos.x * _paintTexture.width - brushSize, 0);
        var startY = (int)Mathf.Max(pos.y * _paintTexture.height - brushSize, 0);
        var endX = (int)Mathf.Min(pos.x * _paintTexture.width + brushSize, _paintTexture.width);
        var endY = (int)Mathf.Min(pos.y * _paintTexture.height + brushSize, _paintTexture.height);
        //Debug.Log("pos = " + pos + ", startX = " + startX + ", startY = " + startY + ", endX = " + endX + ", endY = " + endY);
        for (var i = startX; i < endX; i++)
        {
            for (var j = startY; j < endY; j++)
            {
                // TODO
                _texturePixels[_paintTexture.width * i + j] = color;
            }
        }
    }
    
    private Vector2 GetSpritePos(Vector2 inputPos)
    {
        var worldPos = _cam.ScreenToWorldPoint(inputPos);
        var objPoint = transform.InverseTransformPoint(worldPos);
        objPoint += _sprite.bounds.extents;
        objPoint.x /= _sprite.bounds.size.x;
        objPoint.y /= _sprite.bounds.size.y;
        return objPoint;
    }

    private bool IsSpritePosOnSprite(Vector2 pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x <= 1 && pos.y <= 1;  // TODO factor in brush size
    }

    public bool InputOnSprite(Vector2 inputPos, out Vector2 pos)
    {
        pos = GetSpritePos(inputPos);
        return IsSpritePosOnSprite(pos);
    }
    
    public void SaveTexture()
    {
        Debug.Log("saving tex");
        _paintTexture.SetPixels(_texturePixels);
        _paintTexture.Apply(false);
        _renderer.material.SetTexture(MainTex, _paintTexture);
        
    }
}
