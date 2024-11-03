using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public SoundManager SoundManager;
    public Camera Camera;
    public NailPolish[] Polishes;
    public Nail[] Nails;

    public SpeechManager SpeechManager;

    public PauseMenu PauseMenu;
    public CanvasGroup IntroPanel;
    
    private NailPolish _lastSelected;
    private int _lastSpeech;
    private bool _gameRunning;

    private float _lastInputTime;

    private const float InactiveTimeWindow = 120; // back to intro screen after 2 minutes inactive
    
    private void Start()
    {
        SoundManager.Init();
        SpeechManager.Init();
        SoundManager.PlayMusic(AudioRef.Music);
        
        foreach (var nailPolish in Polishes)
        {
            nailPolish.Init();
        }

        foreach (var nail in Nails)
        {
            nail.Init();
        }

        PauseMenu.OnClickedOutside.AddListener(UnpauseGame);
    }

    public void StartGame()
    {
        _lastInputTime = Time.time;
        IntroPanel.Hide();
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

        if (Time.time - _lastInputTime > InactiveTimeWindow)
        {
            Restart();
        }
        
        var input = Utility.GetInput();

        // TODO better input detect
        if (input.Ended)
        {
            _lastInputTime = Time.time;
            var worldPos = Camera.ScreenToWorldPoint(input.Position);
            worldPos.z = 0;
            
            foreach (var nailPolish in Polishes)
            {
                if (nailPolish.IsInside(worldPos))
                {
                    if (_lastSelected != null)
                    {
                        SoundManager.PlaySound(AudioRef.CloseCap);
                        _lastSelected.Deselect();
                    }

                    if (_lastSelected != nailPolish)
                    {
                        SoundManager.PlaySound(AudioRef.OpenCap);
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
                    if ((SpeechType)_lastSpeech == SpeechType.Negative) SoundManager.PlaySound(AudioRef.Discomfort);
                    SpeechManager.RunLine((SpeechType)_lastSpeech);
                    ++_lastSpeech;
                    _lastSpeech %= 3;
                    nail.SetColor(_lastSelected.PolishColor);
                    _lastSelected.PolishCap.PaintNail(nail);
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
