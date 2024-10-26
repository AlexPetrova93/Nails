using System.Collections;
using UnityEngine;

public class PolishPainter : MonoBehaviour
{
    public SpriteRenderer BrushPrefab;
    public SpriteRenderer Brush;
    public int BrushSize = 128;
    
    public Transform BrushParent;

    public NailOld[] Nails;
    
    private Camera _camera;
    
    private Color _currentPolishColor;

    private Vector2 _lastInput;

    private int _curBrushes;
    
    private bool _paintingActive;
    private bool _isPainting;
    private bool _destroyNextFrame;

    private const int MaxBrushes = 250;
    private const int MinSpacing = 10;
    
    public void Init(Camera camera)
    {
        _camera = camera;
        foreach (var nail in Nails)
        {
            nail.Init(_camera);
        }
    }
    
    public void StartPainting(Color polishColor)
    {
        _currentPolishColor = polishColor;
        _paintingActive = true;
        Brush.color = polishColor;
    }
    
    private void Update()
    {
        if (!_paintingActive) return;

        var input = Utility.GetInput();
        
        var worldPos = _camera.ScreenToWorldPoint(input.Position);
        worldPos.z = 0;
        Brush.transform.position = worldPos;

        if (_destroyNextFrame)
        {
            _destroyNextFrame = false;
            _curBrushes = 0;
            foreach (Transform o in BrushParent)
            {
                Destroy(o.gameObject);
            }

            foreach (var nail in Nails)
            {
                nail.SaveTexture();
            }
        }
        
        if (input.Began)
        {
            _isPainting = true;
        }

        if (_isPainting && input.Moved)
        {
            Paint(input.Position, worldPos);
        }

        if (_isPainting && input.Ended)
        {
            _isPainting = false;
        }
    }

    private void Paint(Vector2 inputPos, Vector2 pos)
    {
        Vector2 spritePos;
        foreach (var nail in Nails)
        {
            if (nail.InputOnSprite(inputPos, out spritePos))
            {
                nail.Paint(spritePos, BrushSize, _currentPolishColor);
        
                var stroke = Instantiate(BrushPrefab, pos, Quaternion.identity, BrushParent);
                stroke.color = _currentPolishColor;
                ++_curBrushes;
                if (_curBrushes > MaxBrushes) _destroyNextFrame = true;

                break;
            }
        }
    }

}
