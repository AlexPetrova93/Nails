using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PolishPainter PolishPainter;
    
    public Camera Camera;
    public NailPolish[] Polishes;

    private void Start()
    {
        PolishPainter.Init(Camera);
    }

    private void Update()
    {
        var input = Utility.GetInput();

        if (input.Ended)
        {
            var worldPos = Camera.ScreenToWorldPoint(input.Position);
            worldPos.z = 0;
            
            foreach (var nailPolish in Polishes)
            {
                if (nailPolish.IsInside(worldPos))
                {
                    PolishPainter.StartPainting(nailPolish.PolishColor);
                    break;
                }
            }
        }
    }
}
