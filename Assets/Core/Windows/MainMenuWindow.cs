using UnityEngine;
using UnityEngine.UI;

public class MainMenuWindow : Window
{
    [SerializeField] KeyCode _startGame = KeyCode.Return;

    public override void OnEnter(StateController<WindowsManagement> controller, IState<WindowsManagement> previous)
    {
        var label = GetComponentInChildren<Text>();

        if (label != null)
            label.text = $"Main Menu. \n Press \"{_startGame}\" to start game.";
    }

    public override void OnUpdate(StateController<WindowsManagement> controller){
        if (Input.GetKeyDown(_startGame))
            controller.Show<LoadingWindow>();
    }

    public override void AcceptGameController(GameController game, Window previous)
    {
        if(previous != null)
            game.GameOver();
    }
}
