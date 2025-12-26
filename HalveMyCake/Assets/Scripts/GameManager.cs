using System.Collections.Generic;
using IACGGames;
using IACGGames.UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : GameManagerBase<GameManager>
{
    [SerializeField] private NormalScoreWrapper normalScore;
    [SerializeField] private CakeSlicer cakeSlicer;
    [SerializeField] private FlourSlicer flourSlicer;
    [SerializeField] private ButterSlicer butterSlicer;
    [SerializeField] private ProblemsSO problems;
    [SerializeField] private ProblemUI problemUI;
    [SerializeField] private GameOverPanel gameOverPanel;
    [SerializeField] private Countdown countdown;
    [SerializeField] private TutorialPanel tutorialPanel;
    [SerializeField] private CorrectOrWrong correctOrWrong;
    QuizMode mode = QuizMode.flour;
    bool gameStarted = false;
    GameState gameState = GameState.ended_or_started;
    int problemCount => problems.problem.Count;
    int problemIndex = -1;
    Problem currentProblem = new();

    //Problem currentProblem = null;
    private void Start()
    {
        UIManager.Instance.Init();
    }

    Dictionary<int, List<Problem>> problemDict = new Dictionary<int, List<Problem>>();
    private int difficultyLevel => GameSDKSystem.Instance.currentLevel;
    public void StartGame()
    {
        gameState = GameState.running;
        problemDict.Clear();
        foreach (var p in problems.problem)
        {
            if (problemDict.ContainsKey(p.difficulty))
            {
                problemDict[p.difficulty].Add(p);
            }
            else
            {
                problemDict[p.difficulty] = new List<Problem>() { p };
            }
        }
        UIManager.Instance.Show(UIState.GameHUD, 0.5f);
        NextQuiz();
        countdown.StartTimer();
        normalScore.Initialize();
        gameStarted = true;
    }

    public override void OnRestart()
    {
        base.OnRestart();
        StartCoroutine(LoadScene(1));
    }
    public override void OnQuit()
    {
        base.OnQuit();
        StartCoroutine(LoadScene(1));
    }
    IEnumerator LoadScene(int sceneid)
    {
        AsyncOperation aop = SceneManager.LoadSceneAsync(sceneid);
        while (!aop.isDone)
        {
            yield return null;
        }
        Time.timeScale = 1;
    }
    public override void OnPause()
    {
        base.OnPause();
        gameState = GameState.paused;
        Time.timeScale = 0;
    }
    public override void OnResume()
    {
        base.OnResume();
        gameState = GameState.running;
        Time.timeScale = 1;

    }
    public override void OnStartTutorial()
    {
        base.OnStartTutorial();
        Time.timeScale = 0;
        StartTutorial();
    }
    public override void OnGameStarted()
    {
        base.OnGameStarted();
        StartGame();
    }
    public void OnLevelComplete()
    {
        gameState = GameState.time_up;
        normalScore.Score.Add(GameSDKSystem.Instance.currentLevel * 1000);
        Time.timeScale = 1;
        //gameOverPanel.ShowGameOver(normalScore.Score.Score);
        StartCoroutine(WaitForLastProblem());
    }
    IEnumerator WaitForLastProblem()
    {
        yield return new WaitUntil(() => gameState == GameState.game_over);
        gameState = GameState.ended_or_started;
        gameOverPanel.ShowGameOver(normalScore.Score.Score);
    }
    readonly int maxAttempts = 2;
    int attemptsRemaining = 2;

    public void Update()
    {
        if (gameState == GameState.time_up)
        {
            OnLevelComplete();
            gameState = GameState.game_over;
            return;
        }
        if (gameState != GameState.running)
            return;
        if (!gameStarted) return;
        if (mode == QuizMode.butter)
        {
            butterSlicer.UpdateSliceFill();
            problemUI.SetAnswerText(butterSlicer.GetFraction());
        }
        else if (mode == QuizMode.cake)
        {
            cakeSlicer.UpdateSliceFill();
            problemUI.SetAnswerText(cakeSlicer.GetFraction());
        }
        else if (mode == QuizMode.flour)
        {
            flourSlicer.UpdateSliceFill();
            problemUI.SetAnswerText(flourSlicer.GetFraction());
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (CheckAnswer())
            {
                correctOrWrong.CorrectAnswer();
                GameSDKSystem.Instance.correctAnswers++;
                normalScore.Score.Add(SaveDataHandler.Instance.levelData[GameSDKSystem.Instance.currentLevel].pointsReward * (int)((float)attemptsRemaining/maxAttempts));
                NextQuiz();
            }
            else
            {
                attemptsRemaining--;
                if (attemptsRemaining == 0)
                {
                    correctOrWrong.WrongAnswer();
                    NextQuiz();
                }
            }

        }
    }
    int multiplier = 1;
    public bool CheckAnswer()
    {
        int ans = 0;
        if (mode == QuizMode.butter)
            ans = butterSlicer.GetAnswer();
        else if (mode == QuizMode.cake)
            ans = cakeSlicer.GetAnswer();
        else if (mode == QuizMode.flour)
            ans = flourSlicer.GetAnswer();
        return ans == problems.problem[problemIndex].answer.numerator * multiplier;
    }
    public void NextQuiz()
    {
        attemptsRemaining = maxAttempts;

        if (mode == QuizMode.butter)
            butterSlicer.gameObject.SetActive(false);
        else if (mode == QuizMode.cake)
            cakeSlicer.gameObject.SetActive(false);
        else if (mode == QuizMode.flour)
            flourSlicer.gameObject.SetActive(false);


        mode = (QuizMode)(Random.Range(0, 3));
        problemIndex = (problemIndex + 1) % problemCount;
        //multiplier = Random.Range(1, 4);
        problemUI.SetProblemText(problems.problem[problemIndex].question);

        if (mode == QuizMode.butter)
            butterSlicer.gameObject.SetActive(true);
        else if (mode == QuizMode.cake)
            cakeSlicer.gameObject.SetActive(true);
        else if (mode == QuizMode.flour)
            flourSlicer.gameObject.SetActive(true);

        // initialize slices
        if (mode == QuizMode.butter)
            butterSlicer.InitializeSlices(problems.problem[problemIndex].answer.denominator * multiplier);
        else if (mode == QuizMode.cake)
            cakeSlicer.InitializeSlices(problems.problem[problemIndex].answer.denominator * multiplier);
        else if (mode == QuizMode.flour)
            flourSlicer.InitializeSlices(problems.problem[problemIndex].answer.denominator * multiplier);

    }
    private void StartTutorial()
    {
        tutorialPanel.gameObject.SetActive(true);
    }
}
public enum QuizMode
{
    cake = 0,
    flour = 1,
    butter = 2
}
public enum GameState
{
    ended_or_started,
    running,
    paused,
    time_up,
    game_over
}
