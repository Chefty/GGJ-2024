using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputTest : MonoBehaviour
{
    private PlayerControls playerControls;

    private void Start() 
    {
        playerControls = new PlayerControls();
        playerControls.Enable();
    }

    void OnMove(InputValue inputValue)
    {
        Debug.Log("Move: " + inputValue.Get<Vector2>());
    }

    void OnAttack()
    {
        Debug.Log("Attack");
    }

    void OnFart()
    {
        Debug.Log("Fart");
    }
}
