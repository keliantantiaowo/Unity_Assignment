using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
  public GameObject MainCamera;
  private CharacterController Player;
  private Vector3 Velocity;
  public float WalkSpeed = 2.0f;
  public float RunSpeed = 4.0f;
  public float JumpSpeed = 4.0f;

  void Start() {
    Player = GetComponent<CharacterController>();
  }

  void FixedUpdate() {
    transform.Rotate(new Vector3(0.0f, MainCamera.transform.localEulerAngles.y, 0.0f));

	MainCamera.transform.localEulerAngles -= new Vector3(0.0f, MainCamera.transform.localEulerAngles.y, 0.0f);

    if (Player.isGrounded) {
      Velocity = Vector3.zero;
      Velocity += transform.forward * Input.GetAxis("Vertical") * (Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed);
      Velocity += transform.right * Input.GetAxis("Horizontal") * (Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed);

      if (Input.GetButton("Jump"))
        Velocity += Vector3.up * JumpSpeed;
    }

    Velocity += Physics.gravity * Time.fixedDeltaTime;

    Player.Move(Velocity * Time.fixedDeltaTime);
  }
}
