using UnityEngine;
using UnityEngine.UI;

public class GameplayWindow : Window
{
    [SerializeField] KeyCode _pauseKey = KeyCode.P;

    public override void OnEnter(StateController<WindowsManagement> controller, IState<WindowsManagement> previous)
    {
        var label = GetComponentInChildren<Text>();
        
        if(label != null)
            label.text = 
                $"Game Running. \n Press \"{_pauseKey}\" to pause or exit.";
    }

    public override void OnUpdate(StateController<WindowsManagement> controller){

        if (Input.GetKeyDown(_pauseKey))
            controller.Show<PauseWindow>();
    }

    public override void AcceptGameController(GameController game, Window previous)
    {
        if (!(previous is PauseWindow)) {
            game.StartGame();

            return;
        }

        game.ResumeGame();
    }
}
