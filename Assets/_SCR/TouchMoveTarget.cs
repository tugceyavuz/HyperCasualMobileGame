using UnityEngine;
using UnityEngine.InputSystem; // Required for Input System API

public class TouchMoveTarget : MonoBehaviour
{
    public float moveSpeed = 5f;    // Forward speed (Z+ direction)
    public float xSpeed = 5f;       // Horizontal speed (X-axis)
    public MonsterBehavior monsterBehavior;

    private InputAction touchPositionAction;
    private Camera mainCam;

    private void Awake()
    {
        // Create a new input action for touch position
        touchPositionAction = new InputAction(type: InputActionType.Value, binding: "<Touchscreen>/primaryTouch/position");
        touchPositionAction.Enable();

        mainCam = Camera.main;
    }

    void Update()
    {
        if (monsterBehavior != null && monsterBehavior.started)
        {
            transform.position = monsterBehavior.transform.position;
            return;
        }

        HandleTouch();
        MoveForwardInZ();
    }

    void HandleTouch()
    {
        if (Touchscreen.current == null || Touchscreen.current.primaryTouch.press.isPressed == false)
            return;

        Vector2 screenPos = touchPositionAction.ReadValue<Vector2>();

        Vector3 worldPos = mainCam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, mainCam.WorldToScreenPoint(transform.position).z));
        worldPos.y = transform.position.y;
        worldPos.z = transform.position.z;

        transform.position = Vector3.MoveTowards(transform.position, worldPos, xSpeed * Time.deltaTime);
    }

    void MoveForwardInZ()
    {
        transform.position += new Vector3(0, 0, moveSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        touchPositionAction.Disable();
    }
}
