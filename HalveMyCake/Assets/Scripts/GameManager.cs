using IACGGames;
using IACGGames.UISystem;
using UnityEngine;

public class GameManager : GameManagerBase<GameManager>
{
    [SerializeField] private NormalScoreWrapper normalScore;
    [SerializeField] private CakeSlicer cakeSlicer;
    [SerializeField] private FlourSlicer flourSlicer;
    [SerializeField] private ButterSlicer butterSlicer;
    [SerializeField] private ProblemsSO problems;
    [SerializeField] private ProblemUI problemUI;
    
    GameMode mode = GameMode.flour;
    bool gameStarted = false;
    int problemCount => problems.problem.Count;
    int problemIndex = -1;
    //Problem currentProblem = null;
    private void Start()
    {
        UIManager.Instance.Init();
    }

    public void StartGame()
    {
        UIManager.Instance.Show(UIState.GameHUD, 0.5f);
        normalScore.Initialize();
        //currentProblem = problems.problem[0];
        NextLevel();
        gameStarted = true;
    }

    public override void OnRestart()
    {
        base.OnRestart();
    }
    public override void OnQuit()
    {
        base.OnQuit();

    }
    public override void OnPause()
    {
        base.OnPause();
    }
    public override void OnResume()
    {
        base.OnResume();
    }
    public override void OnStartTutorial()
    {
        base.OnStartTutorial();
    }
    public override void OnGameStarted()
    {
        base.OnGameStarted();
        StartGame();
    }
    int attemptsRemaining = 2;
    public void Update()
    {
        if (!gameStarted) return;
        if (mode == GameMode.butter)
            butterSlicer.UpdateSliceFill();
        else if (mode == GameMode.cake)
            cakeSlicer.UpdateSliceFill();
        else if (mode == GameMode.flour)
            flourSlicer.UpdateSliceFill();

        if (Input.GetMouseButtonUp(0))
        {
            if(CheckAnswer())
            {
                normalScore.Score.Add(10);
                NextLevel();
            } else
            {
                if(0 >= attemptsRemaining--)
                {
                    NextLevel();
                    attemptsRemaining = 2;
                }

            }

        }
    }
    int multiplier = 1;
    public bool CheckAnswer()
    {
        int ans = 0;
        if (mode == GameMode.butter)
            ans = butterSlicer.GetAnswer();
        else if (mode == GameMode.cake)
            ans = cakeSlicer.GetAnswer();
        else if (mode == GameMode.flour)
            ans = flourSlicer.GetAnswer();
        //currentProblem = problems.problem[problemIndex];
        return ans == problems.problem[problemIndex].answer.numerator * multiplier;
    }
    public void NextLevel()
    {
        if (mode == GameMode.butter)
            butterSlicer.gameObject.SetActive(false);
        else if (mode == GameMode.cake)
            cakeSlicer.gameObject.SetActive(false);
        else if (mode == GameMode.flour)
            flourSlicer.gameObject.SetActive(false);


        mode = (GameMode)(((int)mode + 1) % 3);
        problemIndex = (problemIndex + 1) % problemCount;
        multiplier = Random.Range(1, 4);
        problemUI.SetProblemText(problems.problem[problemIndex].question);


        if (mode == GameMode.butter)
            butterSlicer.gameObject.SetActive(true);
        else if (mode == GameMode.cake)
            cakeSlicer.gameObject.SetActive(true);
        else if (mode == GameMode.flour)
            flourSlicer.gameObject.SetActive(true);

        // initialize slices
        if (mode == GameMode.butter)
            butterSlicer.InitializeSlices(problems.problem[problemIndex].answer.denominator * multiplier);
        else if (mode == GameMode.cake)
            cakeSlicer.InitializeSlices(problems.problem[problemIndex].answer.denominator * multiplier);
        else if (mode == GameMode.flour)
            flourSlicer.InitializeSlices(problems.problem[problemIndex].answer.denominator * multiplier);

    }
}
enum GameMode
{
    cake = 0,
    flour = 1,
    butter = 2
}
