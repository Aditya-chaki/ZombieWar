using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
   private PlayerINputs playerInput;
   private PlayerINputs.PlayerActions onFoot ;
   private PlayerMotor motor;
   private PlayerLook look;
    void Awake()
    {
        playerInput = new PlayerINputs();
        onFoot= playerInput.Player; 
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        onFoot.Jump.performed += ctx => motor.Jump();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(onFoot.Move.ReadValue<Vector2>());
    }
    private void LateUpdate() {
         look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable() {
        onFoot.Enable();
    }
    private void OnDisable() {
        onFoot.Disable();
    }
}
