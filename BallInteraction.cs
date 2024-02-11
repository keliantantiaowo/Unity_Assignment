using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInteraction : MonoBehaviour {
  [System.Serializable]
  public struct Target {
    public GameObject TargetBoard;
    public int TargetScore;
  }
  public Target[] TargetArray;
  public GameObject StandTop;
  public UnityEngine.UI.Text TotalScore;
  public UnityEngine.UI.Text TrialCount;
  public UnityEngine.UI.Text Average;
  private Vector3 InitialPosition = Vector3.zero;
  private Rigidbody BallRigidbody;
  private int Score = 0;
  private int Count = 0;

  void Start() {
    InitialPosition = transform.position;
    TotalScore.text = "Total Score : " + Score.ToString();
    TrialCount.text = "Trial Count : " + Count.ToString();
    Average.text = "Average : " + 0.ToString();
  }

  void OnTriggerStay(Collider DetectedCollider) {
    BallRigidbody = GetComponent<Rigidbody>();

    if (BallRigidbody.velocity.magnitude <= Mathf.Epsilon)
      if (DetectedCollider.gameObject == StandTop)
        ;
      else {
        transform.position = InitialPosition;
        BallRigidbody.velocity = Vector3.zero;
        BallRigidbody.angularVelocity = Vector3.zero;

        BallRigidbody.ResetInertiaTensor();

        foreach (Target CurrentTarget in TargetArray)
          if (DetectedCollider.gameObject == CurrentTarget.TargetBoard) {
            Score += CurrentTarget.TargetScore;
            Count++;
            TotalScore.text = "Total Score : " + Score.ToString();
            TrialCount.text = "Trial Count : " + Count.ToString();
            Average.text = "Average : " + (Count == 0 ? 0.ToString() : (Score / Count).ToString());

            break;
          }
      }
  }

  void OnDisable() {
    Score = 0;
    Count = 0;
    TotalScore.text = "Total Score : " + Score.ToString();
    TrialCount.text = "Trial Count : " + Count.ToString();
    Average.text = "Average : " + 0.ToString();
  }
}
