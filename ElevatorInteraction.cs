using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorInteraction : MonoBehaviour {
  public GameObject[] ElevatorDoor = new GameObject[2];
  public GameObject[] UpperFloorDoor = new GameObject[2];
  public GameObject[] LowerFloorDoor = new GameObject[2];
  public GameObject ElevatorSwitch;
  public GameObject UpperFloorSwitch;
  public GameObject LowerFloorSwitch;
  private Ray LineOfSight;
  private RaycastHit HitDetection;
  private float RayLength = 2.0f;
  private enum DoorCondition {Opening, Closing}
  private enum Destination {UpperFloor, LowerFloor}
  private DoorCondition CurrentCondition = DoorCondition.Closing;
  private Destination CurrentDestination = Destination.UpperFloor;
  private Vector3 UpperFloorStopPosition = Vector3.zero;
  private Vector3 LowerFloorStopPosition = Vector3.zero;
  private Vector3 UpperRotationCenter = Vector3.zero;
  private Vector3 LowerRotationCenter = Vector3.zero;
  private float FloorHeight = 3.6f;
  private float AngleLimit = 33.75f;
  private float AscendSpeed = 0.0f;
  private float DescendSpeed = 0.0f;
  private float Acceleration = 0.9f;
  private float OperationTime = 0.0f;
  private float ArrivalTime = 5.0f;
  private float AccelerationTime = 1.0f;
  private float DecelerationTime = 4.0f;
  private float[] WaitingTime = new float[2] {1.0f, 3.0f};
  private float OpeningTime = 2.0f;
  private float ClosingTime = 2.0f;

  void Start() {
    UpperFloorStopPosition = transform.position;
    LowerFloorStopPosition = transform.position - Vector3.up * FloorHeight;
    OperationTime = WaitingTime[1] + ClosingTime;
  }

  void FixedUpdate() {
    if (transform.position == UpperFloorStopPosition && CurrentDestination == Destination.UpperFloor) {
      OperationTime += Time.fixedDeltaTime;

      switch (CurrentCondition) {
        case DoorCondition.Opening :
          if (OperationTime > WaitingTime[0] && OperationTime <= WaitingTime[0] + OpeningTime) {
            ElevatorDoor[0].transform.Rotate(Vector3.up * AngleLimit * Time.fixedDeltaTime / OpeningTime);
            ElevatorDoor[1].transform.Rotate(Vector3.down * AngleLimit * Time.fixedDeltaTime / OpeningTime);
            UpperFloorDoor[0].transform.Rotate(Vector3.up * AngleLimit * Time.fixedDeltaTime / OpeningTime);
            UpperFloorDoor[1].transform.Rotate(Vector3.down * AngleLimit * Time.fixedDeltaTime / OpeningTime);
          } else if (OperationTime > WaitingTime[0] + OpeningTime) {
            CurrentCondition = DoorCondition.Closing;
            OperationTime = 0.0f;
          }
          break;
        case DoorCondition.Closing :
          if (OperationTime > WaitingTime[1] && OperationTime <= WaitingTime[1] + ClosingTime) {
            ElevatorDoor[0].transform.Rotate(Vector3.down * AngleLimit * Time.fixedDeltaTime / ClosingTime);
            ElevatorDoor[1].transform.Rotate(Vector3.up * AngleLimit * Time.fixedDeltaTime / ClosingTime);
            UpperFloorDoor[0].transform.Rotate(Vector3.down * AngleLimit * Time.fixedDeltaTime / ClosingTime);
            UpperFloorDoor[1].transform.Rotate(Vector3.up * AngleLimit * Time.fixedDeltaTime / ClosingTime);
          } else if (OperationTime > WaitingTime[1] + ClosingTime) {
            if (Input.GetKeyDown(KeyCode.E)) {
              LineOfSight = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));

              if (Physics.Raycast(LineOfSight, out HitDetection, RayLength))
                if (HitDetection.collider.gameObject == ElevatorSwitch) {
                  CurrentDestination = Destination.LowerFloor;
                  OperationTime = 0.0f;
                } else if (HitDetection.collider.gameObject == UpperFloorSwitch) {
                  CurrentCondition = DoorCondition.Opening;
                  OperationTime = WaitingTime[0];
                } else if (HitDetection.collider.gameObject == LowerFloorSwitch) {
                  CurrentDestination = Destination.LowerFloor;
                  OperationTime = 0.0f;
                }
            }
          }
          break;
        default : break;
      }
    } else if (transform.position == LowerFloorStopPosition && CurrentDestination == Destination.LowerFloor) {
      OperationTime += Time.fixedDeltaTime;

      switch (CurrentCondition) {
        case DoorCondition.Opening :
          if (OperationTime > WaitingTime[0] && OperationTime <= WaitingTime[0] + OpeningTime) {
            ElevatorDoor[0].transform.Rotate(Vector3.up * AngleLimit * Time.fixedDeltaTime / OpeningTime);
            ElevatorDoor[1].transform.Rotate(Vector3.down * AngleLimit * Time.fixedDeltaTime / OpeningTime);
            LowerFloorDoor[0].transform.Rotate(Vector3.up * AngleLimit * Time.fixedDeltaTime / OpeningTime);
            LowerFloorDoor[1].transform.Rotate(Vector3.down * AngleLimit * Time.fixedDeltaTime / OpeningTime);
          } else if (OperationTime > WaitingTime[0] + OpeningTime) {
            CurrentCondition = DoorCondition.Closing;
            OperationTime = 0.0f;
          }
          break;
        case DoorCondition.Closing :
          if (OperationTime > WaitingTime[1] && OperationTime <= WaitingTime[1] + ClosingTime) {
            ElevatorDoor[0].transform.Rotate(Vector3.down * AngleLimit * Time.fixedDeltaTime / ClosingTime);
            ElevatorDoor[1].transform.Rotate(Vector3.up * AngleLimit * Time.fixedDeltaTime / ClosingTime);
            LowerFloorDoor[0].transform.Rotate(Vector3.down * AngleLimit * Time.fixedDeltaTime / ClosingTime);
            LowerFloorDoor[1].transform.Rotate(Vector3.up * AngleLimit * Time.fixedDeltaTime / ClosingTime);
          } else if (OperationTime > WaitingTime[1] + ClosingTime) {
            if (Input.GetKeyDown(KeyCode.E)) {
              LineOfSight = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));

              if (Physics.Raycast(LineOfSight, out HitDetection, RayLength))
                if (HitDetection.collider.gameObject == ElevatorSwitch) {
                  CurrentDestination = Destination.UpperFloor;
                  OperationTime = 0.0f;
                } else if (HitDetection.collider.gameObject == UpperFloorSwitch) {
                  CurrentDestination = Destination.UpperFloor;
                  OperationTime = 0.0f;
                } else if (HitDetection.collider.gameObject == LowerFloorSwitch) {
                  CurrentCondition = DoorCondition.Opening;
                  OperationTime = WaitingTime[0];
                }
            }
          }
          break;
        default : break;
      }
    } else {
      OperationTime += Time.fixedDeltaTime;

      if (OperationTime >= ArrivalTime) {
        CurrentCondition = DoorCondition.Opening;
        OperationTime = 0.0f;

        switch (CurrentDestination) {
          case Destination.UpperFloor :
            transform.position = UpperFloorStopPosition;
            break;
          case Destination.LowerFloor :
            transform.position = LowerFloorStopPosition;
            break;
          default : break;
        }
      } else {
        switch (CurrentDestination) {
          case Destination.UpperFloor :
            if (OperationTime <= AccelerationTime)
              AscendSpeed += Acceleration * Time.fixedDeltaTime;
            else if (OperationTime > DecelerationTime)
              AscendSpeed -= Acceleration * Time.fixedDeltaTime;
            transform.Translate(Vector3.up * AscendSpeed * Time.fixedDeltaTime);
            break;
          case Destination.LowerFloor :
            if (OperationTime <= AccelerationTime)
              DescendSpeed -= Acceleration * Time.fixedDeltaTime;
            else if (OperationTime > DecelerationTime)
              DescendSpeed += Acceleration * Time.fixedDeltaTime;
            transform.Translate(Vector3.up * DescendSpeed * Time.fixedDeltaTime);
            break;
          default : break;
        }
      }
    }
  }
}
