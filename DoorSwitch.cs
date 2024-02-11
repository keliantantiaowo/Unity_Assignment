using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : MonoBehaviour {
  public GameObject Player;
  public GameObject Door;
  public GameObject LeverBody;
  private Vector3 DoorPosition = Vector3.zero;
  private Vector3 DoorSize = Vector3.zero;
  private bool PowerConnection = false;
  private float OpeningSpeed = 0.5f;
  private float ClosingSpeed = 0.5f;
  private Vector3 RotationCenter = Vector3.zero;
  private float RotateSpeed = 90.0f;
  private float AngleLimit = 30.0f;

  void Start() {
    DoorPosition = Door.transform.position;
    DoorSize = Door.transform.lossyScale;
    RotationCenter = transform.position + new Vector3(0.0f, 0.0f, -LeverBody.transform.lossyScale.y);

    transform.RotateAround(RotationCenter, Vector3.right, -AngleLimit);
  }

  void OnTriggerEnter(Collider DetectedCollider) {
    if (DetectedCollider.gameObject == Player)
      if ((transform.localEulerAngles.x > 180.0f ? transform.localEulerAngles.x - 360.0f : transform.localEulerAngles.x) >= AngleLimit || (transform.localEulerAngles.x > 180.0f ? transform.localEulerAngles.x - 360.0f : transform.localEulerAngles.x) <= -AngleLimit)
        PowerConnection = !PowerConnection;
  }

  void Update() {
    if (PowerConnection) {
      if (Door.transform.position.x < DoorPosition.x + DoorSize.x)
        Door.transform.Translate(Vector3.right * OpeningSpeed * Time.deltaTime);

      if ((transform.localEulerAngles.x > 180.0f ? transform.localEulerAngles.x - 360.0f : transform.localEulerAngles.x) < AngleLimit)
        transform.RotateAround(RotationCenter, Vector3.right, RotateSpeed * Time.deltaTime);
    } else {
      if (Door.transform.position.x > DoorPosition.x)
        Door.transform.Translate(Vector3.left * ClosingSpeed * Time.deltaTime);

      if ((transform.localEulerAngles.x > 180.0f ? transform.localEulerAngles.x - 360.0f : transform.localEulerAngles.x) > -AngleLimit)
        transform.RotateAround(RotationCenter, Vector3.right, -RotateSpeed * Time.deltaTime);
    }
  }
}
