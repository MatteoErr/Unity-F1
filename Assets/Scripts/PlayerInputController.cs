using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    float boost = 1f;
    public int player;
    bool isBoosting, isBraking, isPaused;
    CallbackContext[] callbackContext = new CallbackContext[2];
    PlayerMovement playerMovement;
    
    private void Awake()
    {
        tag = "NewPrefab";
        PauseMenu.instance.AddEventSystem(GetComponent<MultiplayerEventSystem>());
        StartCoroutine(ChangeTag("Prefab", 0.05f));
        player = JoinPlayers.instance.playersJoined;

        playerMovement = GameObject.FindGameObjectWithTag("Player" + (player)).GetComponent<PlayerMovement>();
        player--;
        isPaused = !PauseMenu.instance.gameIsPaused;
    }

    private void Update()
    {
        if (!PauseMenu.instance.gameIsStarted)
            return;

        if (isBraking)
            Brake(callbackContext[1]);
        else if (isBoosting)
            Boost(callbackContext[0]);

        if (!PauseMenu.instance.gameIsStarted)
            return;
        
        if(PauseMenu.instance.gameIsPaused != isPaused && PauseMenu.instance.gameIsPaused)
            GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");
        else if(PauseMenu.instance.gameIsPaused != isPaused && !PauseMenu.instance.gameIsPaused)
            GetComponent<PlayerInput>().SwitchCurrentActionMap("Player");

        isPaused = PauseMenu.instance.gameIsPaused;
    }

    public void OnMove(CallbackContext context)
    {
        if (playerMovement != null)
            playerMovement.verticalAxis = context.ReadValue<float>();
    }
    
    public void OnRotate(CallbackContext context)
    {
        if(playerMovement != null)
            playerMovement.rotationForce = context.ReadValue<float>();
    }

    public void OnBoost(CallbackContext context)
    {
        if (playerMovement == null)
            return;
            
            boost = playerMovement.boost;

        if (context.ReadValue<float>() > 0.05f)
            isBoosting = true;
        else
        {
            isBoosting = false;
            StartCoroutine(StopBoostOrBrake(1));
        }
        callbackContext[0] = context;
    }
    
    public void OnBrake(CallbackContext context)
    {
        if (playerMovement == null)
            return;

        boost = playerMovement.boost;

        if (context.ReadValue<float>() > 0.05f)
            isBraking = true;
        else
        {
            isBraking = false;
            StartCoroutine(StopBoostOrBrake(2));
        }
        callbackContext[1] = context;
    }

    public void OnOptions()
    {
        PauseMenu.instance.PauseOrResume(player);
    }

    private IEnumerator StopBoostOrBrake(int boostOrBrake)
    {
        if (boost == 1)
            yield break;
        //Gamepad.all[player].PauseHaptics();
        if (boostOrBrake == 1)
        {
            if(boost < 1f)
            {
                StartCoroutine(StopBoostOrBrake(2));
                yield break;
            }
            isBoosting = false;
            while (boost > 1f)
            {
                yield return new WaitForSeconds(0.01f);
                boost -= 0.01f;
                playerMovement.boost = boost;
            }
        }
        else if (boostOrBrake == 2)
        {
            if (boost > 1f)
            {
                StartCoroutine(StopBoostOrBrake(1));
                yield break;
            }
            isBraking = false;
            while (boost < 1f)
            {
                yield return new WaitForSeconds(0.01f);
                boost += 0.03f;
                playerMovement.boost = boost;
            }
        }
        boost = 1f;
        playerMovement.boost = boost;
    }

    void Boost(CallbackContext _context)
    {
        isBraking = false;
        if (boost < 1.95f)
        {
            StopAllCoroutines();
            boost += _context.ReadValue<float>() / 200f;
            //Gamepad.all[player].SetMotorSpeeds(0.25f, _context.ReadValue<float>() * 0.75f);
        }

        playerMovement.boost = boost;
    }

    void Brake(CallbackContext _context)
    {
        isBoosting = false;
        if (boost > 0.3f)
        {
            StopAllCoroutines();
            boost -= _context.ReadValue<float>() / 50f;
            //Gamepad.all[player].SetMotorSpeeds(_context.ReadValue<float>() * 0.75f, 0.25f);
        }

        playerMovement.boost = boost;
    }

    /*void InitiateInputs()
    {
        inputs = new PlayerControls();

        inputs.Player.Move.performed += ctx => OnMove(ctx);
        inputs.Player.Move.canceled += ctx => OnMove(ctx);
        inputs.Player.Rotate.performed += ctx => OnRotate(ctx);
        inputs.Player.Rotate.canceled += ctx => OnRotate(ctx);
        inputs.Player.Boost.canceled += ctx => StartCoroutine(StopBoostOrBrake(1));
        inputs.Player.Boost.performed += ctx => OnBoost(ctx);
        inputs.Player.Brake.canceled += ctx => StartCoroutine(StopBoostOrBrake(2));
        inputs.Player.Brake.performed += ctx => OnBrake(ctx);
        inputs.Player.Options.performed += ctx => PauseMenu.instance.PauseOrResume(player);
    }*/

    IEnumerator ChangeTag(string newTag, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        tag = newTag;
    }
}