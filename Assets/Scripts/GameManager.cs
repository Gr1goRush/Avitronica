using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void GameActionInt(int v);

public class GameManager : Singleton<GameManager>
{
    public int MedalsCount { get; private set; }
    public int Score;
    public AirplaneController AirplaneController;
    public PlayerController PlayerController;

    [SerializeField] private RoadGenerator RoadGeneratorRoad;

    [SerializeField] private Animator ButtonGameAnim;

    [SerializeField] private TMP_Text ScoreText;
    [SerializeField] private TMP_Text MedalText;

    [SerializeField] private GameObject GameScreen, MainMenu, BackgroundMainMenu, TutorialScreen;
    [SerializeField] private GameOverScreen GameOverScreen;

    private static bool skipMenu = false;

    public event GameActionInt OnMedalsAmountChanged;

    void Start()
    {
        MedalsCount = PlayerPrefs.GetInt("Medals");
        OnMedalsAmountChanged?.Invoke(MedalsCount);

        GameOverScreen.gameObject.SetActive(false);

        if(skipMenu )
        {
            skipMenu = false;
            StartGame();
        }
    }

    void Update()
    {
        ScoreText.text = $"{Score}";
        MedalText.text = $"{MedalsCount}";
    }

    public void Restart()
    {
        skipMenu = true;
        LoadMenu();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Game");
    }

    public void GameOver()
    {
        if (PlayerController.isDeath)
        {
            return;
        }

        AudioController.Instance.Sounds.PlayOneShot("game_over");
        PlayerController.isDeath = true;
        GameOverScreen.Show(Score);
        VibrationManager.Instance.Vibrate();
    }

    public void StartGameButton()
    {
        ButtonGameAnim.SetTrigger("OnStart");
    }

    public void AddMedals(int count = 1)
    {
        MedalsCount++;
        OnMedalsAmountChanged?.Invoke(MedalsCount);
        PlayerPrefs.SetInt("Medals", MedalsCount);
    }

    public void SubtractMedals(int count)
    {
        AddMedals(-count);
    }

    public bool HasMedals(int count)
    {
        return MedalsCount >= count;
    }

    public void StartGame()
    {
        RoadGeneratorRoad.enabled = true;
        PlayerController.isStartingGame = true;

        MainMenu.SetActive(false);
        BackgroundMainMenu.SetActive(false);

        GameScreen.SetActive(true);

        AudioController.Instance.LoadGameMusic();

        if (PlayerPrefs.GetInt("TutorialShowed", 0) != 1)
        {
            TutorialScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void OnTutorialShow()
    {
        PlayerPrefs.SetInt("TutorialShowed", 1);
        TutorialScreen.SetActive(false);
        Time.timeScale = 1f;
    }
}
