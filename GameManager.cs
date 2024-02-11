using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  public GameObject Player;
  public GameObject MainCamera;
  public GameObject GamingArea;
  public GameObject MachineScreen;
  private Ray LineOfSight;
  private RaycastHit HitDetection;
  private float RayLength = 2.0f;
  private Vector3 PreviousPosition = Vector3.zero;
  private Quaternion PreviousRotation = Quaternion.identity;
  private float GamingAngle = 22.5f;
  private CharacterController PlayerController;
  private Collider PlayerCollider;
  private MonoBehaviour PlayerMonoBehaviour;
  private MonoBehaviour CameraMonoBehaviour;
  private bool InGame = false;

  void Start() {
    GamingArea.SetActive(false);

    PlayerController = Player.GetComponent<CharacterController>();
    PlayerCollider = Player.GetComponent<Collider>();
    PlayerMonoBehaviour = Player.GetComponent<MonoBehaviour>();
    CameraMonoBehaviour = MainCamera.GetComponent<MonoBehaviour>();
  }

  void Update() {
    if (InGame) {
      if (Input.GetKeyDown(KeyCode.Escape)) {
        Player.transform.position = PreviousPosition;
        Player.transform.rotation = PreviousRotation;

        MainCamera.transform.Rotate(Vector3.left * GamingAngle);

        GamingArea.SetActive(false);

        PlayerController.enabled = true;
        PlayerCollider.enabled = true;
        PlayerMonoBehaviour.enabled = true;
        CameraMonoBehaviour.enabled = true;
        InGame = false;
      }
    } else {
      if (Input.GetKeyDown(KeyCode.E)) {
        LineOfSight = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));

        if (Physics.Raycast(LineOfSight, out HitDetection, RayLength))
          if (HitDetection.collider.gameObject == MachineScreen) {
            PreviousPosition = Player.transform.position;
            PreviousRotation = Player.transform.rotation;
            Player.transform.position = GamingArea.transform.position;
            Player.transform.rotation = GamingArea.transform.rotation;
            MainCamera.transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);

            MainCamera.transform.Rotate(Vector3.right * GamingAngle);

            GamingArea.SetActive(true);

            PlayerController.enabled = false;
            PlayerCollider.enabled = false;
            PlayerMonoBehaviour.enabled = false;
            CameraMonoBehaviour.enabled = false;
            InGame = true;
          }
      }
    }
  }
}
