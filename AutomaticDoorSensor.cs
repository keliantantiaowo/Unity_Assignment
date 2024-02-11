using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoorSensor : MonoBehaviour {
  public GameObject Player;
  public GameObject AutomaticDoorLeftPart;
  public GameObject AutomaticDoorRightPart;
  private Vector3 LeftDoorPosition = Vector3.zero;
  private Vector3 RightDoorPosition = Vector3.zero;
  private Vector3 LeftDoorSize = Vector3.zero;
  private Vector3 RightDoorSize = Vector3.zero;
  private bool PowerConnection = false;
  private float OpeningSpeed = 0.5f;
  private float ClosingSpeed = 0.5f;
  private float OperationTime = 0.0f;
  private float WaitingTime = 1.0f;

  void Start() {
    LeftDoorPosition = AutomaticDoorLeftPart.transform.position;
    RightDoorPosition = AutomaticDoorRightPart.transform.position;
    LeftDoorSize = AutomaticDoorLeftPart.transform.lossyScale;
    RightDoorSize = AutomaticDoorRightPart.transform.lossyScale;
  }

  void OnTriggerEnter(Collider DetectedCollider) {
    if (DetectedCollider.gameObject == Player) {
      PowerConnection = true;
      OperationTime = 0.0f;
    }
  }

  void OnTriggerStay(Collider DetectedCollider) {
    if (DetectedCollider.gameObject == Player) {
      PowerConnection = true;
      OperationTime = 0.0f;
    }
  }

  void Update() {
    if (PowerConnection) {
      OperationTime += Time.deltaTime;

      if (AutomaticDoorLeftPart.transform.position.x < LeftDoorPosition.x + LeftDoorSize.x) {
        AutomaticDoorLeftPart.transform.Translate(Vector3.right * OpeningSpeed * Time.deltaTime);

        OperationTime = 0.0f;
      }

      if (AutomaticDoorRightPart.transform.position.x > RightDoorPosition.x - RightDoorSize.x) {
        AutomaticDoorRightPart.transform.Translate(Vector3.left * OpeningSpeed * Time.deltaTime);

        OperationTime = 0.0f;
      }

      if (OperationTime > WaitingTime)
        PowerConnection = false;
    } else {
      if (AutomaticDoorLeftPart.transform.position.x > LeftDoorPosition.x)
        AutomaticDoorLeftPart.transform.Translate(Vector3.left * ClosingSpeed * Time.deltaTime);

      if (AutomaticDoorRightPart.transform.position.x < RightDoorPosition.x)
        AutomaticDoorRightPart.transform.Translate(Vector3.right * ClosingSpeed * Time.deltaTime);
    }
  }
}
