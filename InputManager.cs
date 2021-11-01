using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(1)]
public class InputManager : MonoBehaviour
{
    private static Vector2 movements;
    private static Vector2 position;

    public delegate void MouseClick(Vector2 position);

    public static event MouseClick onClick; // left mouse btn or touch
    public static event MouseClick onRightClick;

    private void Awake()
    {
        movements = Vector2.zero;
        position = Vector2.zero;
        
    }

    // phase ===> start -> performed -> cancel
    public void move(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            movements = ctx.ReadValue<Vector2>();
        }
        else if(ctx.canceled)
        {
            movements = Vector2.zero;
        }
    }

    public void fire(InputAction.CallbackContext ctx)
    {
        //Debug.Log("fire   " + ctx);
        if (ctx.performed)
        {
            onClick?.Invoke(position);
        }
    }
    public void rightClick(InputAction.CallbackContext ctx)
    {
        //Debug.Log("fire   " + ctx);
        if (ctx.performed)
        {
            onRightClick?.Invoke(position);
        }
    }


    // use mouse position instead of delta
    public void look(InputAction.CallbackContext ctx)
    {
        
        position = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());


    }

    public static Vector2 getMovements()
    {
        return movements;
    }

    public static Vector2 getPosition()
    {
        return position;
    }

}
