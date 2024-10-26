using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera Camera;
    public NailPolish[] Polishes;
    public Nail[] Nails;

    private NailPolish _lastSelected;
    
    private void Start()
    {
        foreach (var nailPolish in Polishes)
        {
            nailPolish.Init();
        }

        foreach (var nail in Nails)
        {
            nail.Init();
        }
    }

    private void Update()
    {
        var input = Utility.GetInput();

        // TODO better input detect?
        if (input.Ended)
        {
            var worldPos = Camera.ScreenToWorldPoint(input.Position);
            worldPos.z = 0;
            
            foreach (var nailPolish in Polishes)
            {
                if (nailPolish.IsInside(worldPos))
                {
                    if (_lastSelected != null) _lastSelected.Deselect();

                    if (_lastSelected != nailPolish)
                    {
                        nailPolish.Select();
                        _lastSelected = nailPolish;
                    }
                    else
                    {
                        _lastSelected = null;
                    }
                    
                    return;
                }
            }

            if (_lastSelected == null) return;
            
            foreach (var nail in Nails)
            {
                if (nail.IsInside(worldPos))
                {
                    nail.SetColor(_lastSelected.PolishColor);
                    return;
                }
            }
        }
    }

    private void Reset()
    {
        Camera = Camera.main;
        Nails = FindObjectsOfType<Nail>();
        Polishes = FindObjectsOfType<NailPolish>();
    }
}
