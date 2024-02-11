using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefrigeratorInteraction : MonoBehaviour {
  public GameObject DoorBody;
  private Ray LineOfSight;
  private RaycastHit HitDetection;
  private float RayLength = 2.0f;
  private bool OperationPerformed = false;
  private enum DoorCondition {Opening, Closing}
  public enum OpeningDirection {Left, Right}
  private DoorCondition CurrentCondition = DoorCondition.Closing;
  public OpeningDirection DirectionSetting = OpeningDirection.Left;
  private Vector3 RotationCenter = Vector3.zero;
  private float MaxAngle = 90.0f;
  private float MinAngle = 0.0f;
  private float OpeningSpeed = 90.0f;
  private float ClosingSpeed = 90.0f;

  void Start() {
    switch (DirectionSetting) {
      case OpeningDirection.Left :
        RotationCenter = DoorBody.transform.position + new Vector3(-DoorBody.transform.lossyScale.x / 2.0f, 0.0f, -DoorBody.transform.lossyScale.z / 2.0f);
        MaxAngle = 0.0f;
        MinAngle = -90.0f;
        break;
      case OpeningDirection.Right :
        RotationCenter = DoorBody.transform.position + new Vector3(DoorBody.transform.lossyScale.x / 2.0f, 0.0f, -DoorBody.transform.lossyScale.z / 2.0f);
        MaxAngle = 90.0f;
        MinAngle = 0.0f;
        break;
      default : break;
    }
  }

  void Update() {
    if ((transform.localEulerAngles.y > 180.0f ? transform.localEulerAngles.y - 360.0f : transform.localEulerAngles.y) < MaxAngle && (transform.localEulerAngles.y > 180.0f ? transform.localEulerAngles.y - 360.0f : transform.localEulerAngles.y) > MinAngle) {
      switch (CurrentCondition) {
        case DoorCondition.Opening :
          switch (DirectionSetting) {
            case OpeningDirection.Left :
              transform.RotateAround(RotationCenter, Vector3.up, -OpeningSpeed * Time.deltaTime);
              break;
            case OpeningDirection.Right :
              transform.RotateAround(RotationCenter, Vector3.up, OpeningSpeed * Time.deltaTime);
              break;
            default : break;
          }
          break;
        case DoorCondition.Closing :
          switch (DirectionSetting) {
            case OpeningDirection.Left :
              transform.RotateAround(RotationCenter, Vector3.up, ClosingSpeed * Time.deltaTime);
              break;
            case OpeningDirection.Right :
              transform.RotateAround(RotationCenter, Vector3.up, -ClosingSpeed * Time.deltaTime);
              break;
            default : break;
          }
          break;
        default : break;
      }
    } else if (Input.GetKey(KeyCode.E)) {
      LineOfSight = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));

      if (Physics.Raycast(LineOfSight, out HitDetection, RayLength))
        foreach (Transform ChildTransform in transform)
          if (HitDetection.collider.gameObject == ChildTransform.gameObject)
            OperationPerformed = true;

      if (OperationPerformed) {
        switch (CurrentCondition) {
          case DoorCondition.Opening :
            CurrentCondition = DoorCondition.Closing;
            switch (DirectionSetting) {
              case OpeningDirection.Left :
                transform.RotateAround(RotationCenter, Vector3.up, ClosingSpeed * Time.deltaTime);
                break;
              case OpeningDirection.Right :
                transform.RotateAround(RotationCenter, Vector3.up, -ClosingSpeed * Time.deltaTime);
                break;
              default : break;
            }
            break;
          case DoorCondition.Closing :
            CurrentCondition = DoorCondition.Opening;
            switch (DirectionSetting) {
              case OpeningDirection.Left :
                transform.RotateAround(RotationCenter, Vector3.up, -OpeningSpeed * Time.deltaTime);
                break;
              case OpeningDirection.Right :
                transform.RotateAround(RotationCenter, Vector3.up, OpeningSpeed * Time.deltaTime);
                break;
              default : break;
            }
            break;
          default : break;
        }

        OperationPerformed = false;
      }
    }
  }
}
