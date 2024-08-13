using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Takes in user input and camera information, passes to selected/playable Character
 */
public class Human : MonoBehaviour
{
    //reference to active player; STATIC for direct access to UI
    public static Character player;

    //Input Action class; used to transfer user input to this script
    private static PlayerControls _actions;

    //joystick/WASD
    private Vector2 _input;

    //main camera transform, used to pass rotation to player for camera based movement
    private Transform _cam;

    #region InputSystemInit
    private void Awake()
    {
        _actions = new PlayerControls();
        
    }

    private void OnEnable()
    {
        _actions.Enable();
        
    }

    private void OnDisable()
    {
        _actions.Disable();
    }
    #endregion InputSystemInit


    void Start()
    {
        _cam = Camera.main.transform;
        player = GameObject.Find("xenith-alex64").GetComponent<Character>();

        if (!player) {
            Debug.LogError("Missing Player component");
            Debug.Break();
        }
           
        player.SetPosition(Door.a_spawn);
        player.SetDirection(Door.a_direction);
    }

    // Update is called once per frame
    void Update()
    {
        _input = _actions.Player.Move.ReadValue<Vector2>();

        //directly set input to player, the "cast" button refers to casting a fishing rod
        player.SetCast(_actions.Player.Cast.ReadValue<float>() > 0.0f);
        player.SetReel(_actions.Player.Reel.ReadValue<float>() > 0.0f);
    }

    private void FixedUpdate()
    {
        Quaternion camForward = Quaternion.Euler(0.0f, _cam.rotation.eulerAngles.y, 0.0f);

        Vector3 inputForward = new Vector3(_input.x, 0.0f, _input.y);

        //sends camera's Y forward direction to player
        player.SetDirection((camForward * inputForward).normalized);
    }

    //static function makes it easier for InteractionTrigger to look for input
    public static bool GetInteract() { return _actions.Player.Action.ReadValue<float>() > 0.0f; }
    public static bool GetCancel() { return _actions.Player.Cancel.ReadValue<float>() > 0.0f; }
    public static bool GetCast() { return _actions.Player.Cast.ReadValue<float>() > 0.0f; }
    public static bool GetDebugButton() { return _actions.Player.Debug.WasReleasedThisFrame(); }

    //TODO: move this to Character script, and access it directly via Human.SetInteract()
    public static void SetInteract(bool i, Transform interactable) {
        if (player)
            player.SetInteraction(i, interactable);
    }
}
