using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class WindowChangeEvent : UnityEvent<Window, Window> { }

public class WindowsManagement : MonoBehaviour
{
    [Header("Actual window list")]
    [SerializeField] Window[] _windows = null;
    [Space]

    [Header("Controller properties")]
    [SerializeField] Window _defaultWindow = null;
    [Space]

    [SerializeField] WindowChangeEvent OnWindowChange = null;

    private StateController<WindowsManagement> _controller;

    private void Start() {
        _controller = Initialize();

        FillWindowsList(_controller, _windows);

        Startup();
    }

    private StateController<WindowsManagement> Initialize() {
        var controller = new StateController<WindowsManagement>(this);

        controller.StateChanged += (previous, current) => {
            ActiveWindowsVisibility(previous as Window, current as Window);
            OnWindowChange?.Invoke(previous as Window, current as Window);
        };

        return controller;
    }

    private void FillWindowsList(StateController<WindowsManagement> controller, Window[] windows) {
        foreach (var window in windows)
        {
            if (controller.Contains(window)) continue;

            window.gameObject.SetActive(false);
            window.Initialize(this);

            controller.Add(window);
        }
    }

    private void Startup() {
        if (_defaultWindow == null) throw new System.Exception("No default window is selected, nothing to show.");

        _controller.Show(_defaultWindow);
    }

    private void ActiveWindowsVisibility(Window previous, Window current) {
        previous?.Hide();
        current?.Show();
    }

    private void Update(){
        _controller.Update();
    }
}

public abstract class Window : MonoBehaviour, IState<WindowsManagement>
{
    protected WindowsManagement _manager;

    public virtual void Initialize(WindowsManagement manager) => _manager = manager;

    public virtual void OnEnter(StateController<WindowsManagement> controller, IState<WindowsManagement> previous) { }

    public virtual void OnUpdate(StateController<WindowsManagement> controller) { }

    public virtual void AcceptGameController(GameController gameController, Window previous) { }

    public virtual void Show() => gameObject.SetActive(true);

    public virtual void Hide() => gameObject.SetActive(false);
}
