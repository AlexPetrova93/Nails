using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    public UnityEvent OnClickedOutside { get; } = new();
    
    private bool _open;
    private bool _clickingOutside;

    private void Update()
    {
        if (!_open) return;
        
        var input = Utility.GetInput();
        if (input.Began)
        {
            _clickingOutside = !RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, input.Position);
        }
        // check if clicking outside panel
        if (input.Ended)
        {
            if (_clickingOutside &&
                !RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, input.Position))
            {
                OnClickedOutside.Invoke();
            }
        }
    }

    public void Show()
    {
        var tr = (RectTransform)transform;
        tr.DOAnchorPosX(50, .15f).SetEase(Ease.OutSine).OnComplete(() => _open = true);
    }
    
    public void Hide()
    {
        _open = false;
        var tr = (RectTransform)transform;
        tr.DOAnchorPosX(550, .15f).SetEase(Ease.InSine);
    }
}
