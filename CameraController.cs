using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
  private Vector3 Angle = new Vector3(0.0f, 0.0f, 0.0f);
  public float RotateSpeed = 10.0f;
  public float AngleLimit = 90.0f;

  void Start() {
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
  }

  void Update() {
    transform.RotateAround(transform.position, Vector3.up, Input.GetAxis("Mouse X") * RotateSpeed);
    transform.RotateAround(transform.position, transform.right, -Input.GetAxis("Mouse Y") * RotateSpeed);

    if (AngleLimit >= 0.0f && AngleLimit < 90.0f) {
      Angle = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0.0f);

      if (Angle.x >= AngleLimit && Angle.x <= 180.0f)
        Angle.x = AngleLimit;
      else if (Angle.x <= 360.0f - AngleLimit && Angle.x > 180.0f)
        Angle.x = 360.0f - AngleLimit;

      transform.localEulerAngles = Angle;
    } else if (AngleLimit == 90.0f) {
      if (transform.up.y <= 0.0f) {
        transform.rotation = transform.forward.y >= 0.0f ? Quaternion.LookRotation(Vector3.up, new Vector3(transform.up.x, 0.0f, transform.up.z)) : Quaternion.LookRotation(Vector3.down, new Vector3(transform.up.x, 0.0f, transform.up.z));
      }
    }
  }
}
