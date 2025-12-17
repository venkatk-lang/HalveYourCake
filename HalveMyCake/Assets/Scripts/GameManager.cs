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
    GameMode mode = GameMode.flour;
    
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
        if (mode == GameMode.butter)
            butterSlicer.UpdateSliceFill();
        else if (mode == GameMode.cake)
            cakeSlicer.UpdateSliceFill();
        else if (mode == GameMode.flour)
            flourSlicer.UpdateSliceFill();

        if (Input.GetMouseButtonUp(0))
        {
            // switch mode

            if (mode == GameMode.butter)
                butterSlicer.gameObject.SetActive(false);
            else if (mode == GameMode.cake)
                cakeSlicer.gameObject.SetActive(false);
            else if (mode == GameMode.flour)
                flourSlicer.gameObject.SetActive(false);


            mode = (GameMode)(((int)mode + 1) % 3);

            if (mode == GameMode.butter)
                butterSlicer.gameObject.SetActive(true);
            else if (mode == GameMode.cake)
                cakeSlicer.gameObject.SetActive(true);
            else if (mode == GameMode.flour)
                flourSlicer.gameObject.SetActive(true);
            
            // initialize slices

            if (mode == GameMode.butter)
                butterSlicer.InitializeSlices(Random.Range(3, 10));
            else if (mode == GameMode.cake)
                cakeSlicer.InitializeSlices(Random.Range(3, 10));
            else if (mode == GameMode.flour)
                flourSlicer.InitializeSlices(Random.Range(3, 10));

            
        }
    }
}
enum GameMode
{
    cake = 0,
    flour = 1,
    butter = 2
}
