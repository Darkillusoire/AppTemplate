using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    private GameObject _cube;
    private float _again = 25;

    private UnityAction GameUpdate;

    public void GetWindowState(Window previous, Window current) {
        current.AcceptGameController(this, previous);
    }

    public void StartGame() {
        _cube.SetActive(true);

        GameUpdate = GameUpdateAction;
    }

    public void GameOver() {
        _cube.SetActive(false);

        Destroy(_cube);
    }

    public void PreloadGame() {
        _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _cube.transform.SetParent(transform);

        _cube.SetActive(false);
    }

    public void PauseGame() {
        GameUpdate = null;
    }

    public void ResumeGame() {
        GameUpdate = GameUpdateAction;
    }

    public void GameUpdateAction() {
        _cube.transform.Rotate((Vector3.up + Vector3.right) * _again * Time.fixedDeltaTime);
    }
    
    private void FixedUpdate() => GameUpdate?.Invoke();

}

