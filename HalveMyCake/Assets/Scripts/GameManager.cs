using IACGGames;
using IACGGames.UISystem;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : GameManagerBase<GameManager>
{
    [SerializeField] private NormalScoreWrapper normalScore;
    [SerializeField] private CakeSliceConstructor cakeSlicer;
    [SerializeField] private FlourSlicer flourSlicer;
    [SerializeField] private ButterSlicer butterSlicer;
    private void Start()
    {
        normalScore.Initialize();
        UIManager.Instance.Init();
    }

    public void StartGame()
    {
        UIManager.Instance.Show(UIState.GameHUD, 0.5f);
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
    public void Update()
    {
        flourSlicer.UpdateSliceFill();
        if (Input.GetMouseButton(0))
        {
            flourSlicer.InitializeSlices(Random.Range(5, 10));
        }
    }
}
