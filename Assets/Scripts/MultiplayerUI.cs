using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class MultiplayerUI : MonoBehaviour
{
    GameObject canvasContent;
    MultiplayerEventSystem eventSystem;
    PlayerInputController inputController;
    
    private void Awake()
    {
        eventSystem = GetComponent<MultiplayerEventSystem>();
        inputController = GetComponent<PlayerInputController>();

        GetComponent<PlayerInput>().SwitchCurrentActionMap("UI");

        canvasContent = GameObject.FindGameObjectWithTag("ConnectContent" + (inputController.player + 1));
        eventSystem.playerRoot = canvasContent;
        eventSystem.firstSelectedGameObject = null;
        eventSystem.firstSelectedGameObject = JoinPlayers.instance.buttonsToSelect[inputController.player];
    }

    private void Update()
    {
        if(JoinPlayers.instance.CheckIfReady())
        {
            canvasContent = GameObject.FindGameObjectWithTag("Content" + (inputController.player + 1));
            eventSystem.playerRoot = canvasContent;
        }
        eventSystem.firstSelectedGameObject = null;
    }
}
