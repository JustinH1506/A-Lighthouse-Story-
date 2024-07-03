using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialUI : MonoBehaviour
{
    private enum TutorialName
    {
        Move,
        Boxes,
        Jump,
        Sprint,
        Crouch
    };

    [SerializeField] private TutorialName tutorialUI;

    private bool isActive = false;
    
    private Animator tutorialAnim;
    
    private PlayerControllerMap inputActions;

    private void Awake()
    {
        tutorialAnim = GetComponent<Animator>();
        inputActions = new PlayerControllerMap();
    }

    private void Start()
    {
        if (tutorialUI == TutorialName.Move)
        {
            tutorialAnim.SetBool("Fade", true);
        }
    }
    
    private void OnEnable()
    {
        inputActions.Enable();

        inputActions.Player.Move.performed += PlayerMovement;
        inputActions.Player.MoveObject.performed += PlayerObjectInteraction;
        inputActions.Player.Jump.performed += PlayerJump;
        inputActions.Player.Sprint.performed += PlayerSprint;
        inputActions.Player.Sneak.performed += PlayerCrouch;
    }

    private void OnDisable()
    {
        inputActions.Disable();

        inputActions.Player.Move.performed -= PlayerMovement;
        inputActions.Player.MoveObject.performed -= PlayerObjectInteraction;
        inputActions.Player.Jump.performed -= PlayerJump;
        inputActions.Player.Sprint.performed -= PlayerSprint;
        inputActions.Player.Sneak.performed -= PlayerCrouch;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialAnim.SetBool("Fade", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialAnim.SetBool("Fade", true);
        }
    }

    private void PlayerMovement(InputAction.CallbackContext context)
    {
        if (context.ReadValue<Vector3>() != Vector3.zero && tutorialUI == TutorialName.Move)
        {
            tutorialAnim.SetBool("Fade", false);
            StartCoroutine(WaitForFadeOut());
        }
    }

    private void PlayerObjectInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && tutorialUI == TutorialName.Boxes && isActive)
        {
            tutorialAnim.SetBool("Fade", false);
            StartCoroutine(WaitForFadeOut());
        }
    }

    private void PlayerJump(InputAction.CallbackContext context)
    {
        if (context.performed && tutorialUI == TutorialName.Jump && isActive)
        {
            tutorialAnim.SetBool("Fade", false);
            StartCoroutine(WaitForFadeOut());
        }
    }

    private void PlayerSprint(InputAction.CallbackContext context)
    {
        if (context.performed && tutorialUI == TutorialName.Sprint && isActive)
        {
            tutorialAnim.SetBool("Fade", false);
            StartCoroutine(WaitForFadeOut());
        }
    }

    private void PlayerCrouch(InputAction.CallbackContext context)
    {
        if (context.performed && tutorialUI == TutorialName.Crouch && isActive)
        {
            tutorialAnim.SetBool("Fade", false);
            StartCoroutine(WaitForFadeOut());
        }
    }

    public void FadeInTutorial()
    {
        tutorialAnim.SetBool("Fade", true);
        isActive = true;
    }

    private IEnumerator WaitForFadeOut()
    {
        yield return new WaitForSeconds(1.1f);
        gameObject.SetActive(false);
    }
}
