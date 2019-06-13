using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour {

    private PlayerMovement c_movement;  //Reference to PlayerMovement script
    private bool isJumping;             //To determine if the player is jumping
    private bool isWalking;
	
	void Awake()
    {
        //References
        c_movement = GetComponent<PlayerMovement>();
	}
	
	void Update ()
    {


        if (!isWalking) { 
        isWalking = Input.GetKey(KeyCode.LeftShift);
        }
        //If he is not jumping...
        if (!isJumping)
        {
            //See if button is pressed...
            isJumping = CrossPlatformInputManager.GetButtonDown("Jump");
        }	
	}

    private void FixedUpdate()
    {
        //Get horizontal axis
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        //Call movement function in PlayerMovement
        c_movement.Move(h, isJumping,isWalking);
        //Reset
        isJumping = false;
        isWalking = false;
    }
}
