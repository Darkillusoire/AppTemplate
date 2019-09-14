using UnityEngine;
using UnityEngine.UI;

public class LoadingWindow : Window
{
    [SerializeField] float _loadingDelay = 2f;

    private float _time;

    public override void OnEnter(StateController<WindowsManagement> controller, IState<WindowsManagement> previous){
        var label = GetComponentInChildren<Text>();

        if (label != null)
            label.text = $"Loading...";

        _time = Time.time + _loadingDelay;
    }

    public override void OnUpdate(StateController<WindowsManagement> controller){
        if (_time <= Time.time)
            controller.Show<GameplayWindow>();
    }

    public override void AcceptGameController(GameController game, Window previous)
    {
        game.PreloadGame();
    }
}
