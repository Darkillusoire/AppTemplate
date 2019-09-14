using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : Window
{
    [SerializeField] KeyCode _exitKey = KeyCode.Escape;
    [SerializeField] KeyCode _unpauseKey = KeyCode.P;

    public override void OnEnter(StateController<WindowsManagement> controller, IState<WindowsManagement> previous)
    {
        var label = GetComponentInChildren<Text>();

        if (label != null)
            label.text =
            $"Game Paused. \n Press \"{_exitKey}\" to exit. \n Press \"{_unpauseKey}\" to resume.";
    }

    public override void OnUpdate(StateController<WindowsManagement> controller)
    {
        if (Input.GetKeyDown(_exitKey))
            controller.Show<MainMenuWindow>();

        if (Input.GetKeyDown(_unpauseKey))
            controller.Show<GameplayWindow>();
    }

    public override void AcceptGameController(GameController game, Window previous)
    {
        game.PauseGame();
    }
}
