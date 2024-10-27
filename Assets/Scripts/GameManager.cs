using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera Camera;
    public NailPolish[] Polishes;
    public Nail[] Nails;

    public SpeechManager SpeechManager;

    public PauseMenu PauseMenu;
    
    private NailPolish _lastSelected;
    private int _lastSpeech;
    private bool _gameRunning;
    
    private void Start()
    {
        // TODO add intro warning

        SpeechManager.Init();
        
        foreach (var nailPolish in Polishes)
        {
            nailPolish.Init();
        }

        foreach (var nail in Nails)
        {
            nail.Init();
        }
        
        PauseMenu.OnClickedOutside.AddListener(UnpauseGame);
        
        _gameRunning = true;
    }

    public void PauseGame()
    {
        PauseMenu.Show();
        _gameRunning = false;
    }
    
    public void UnpauseGame()
    {
        PauseMenu.Hide();
        _gameRunning = true;
    }

    private void Update()
    {
        if (!_gameRunning) return;
        
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
                    SpeechManager.RunLine((SpeechType)_lastSpeech);
                    ++_lastSpeech;
                    _lastSpeech %= 3;
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
        SpeechManager = FindObjectOfType<SpeechManager>();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
