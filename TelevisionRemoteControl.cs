using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelevisionRemoteControl : MonoBehaviour {
  public GameObject TelevisionScreen;
  private Ray LineOfSight;
  private RaycastHit HitDetection;
  private float RayLength = 2.0f;
  public Material SwitchOnMaterial;
  public Material SwitchOffMaterial;
  private bool PowerConnection = true;

  void Start() {
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.E)) {
      LineOfSight = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0.0f));

      if (Physics.Raycast(LineOfSight, out HitDetection, RayLength))
        if (HitDetection.collider.gameObject == transform.gameObject)
          if (PowerConnection) {
            PowerConnection = !PowerConnection;
            TelevisionScreen.GetComponent<Renderer>().material = SwitchOffMaterial;
          } else {
            PowerConnection = !PowerConnection;
            TelevisionScreen.GetComponent<Renderer>().material = SwitchOnMaterial;
          }
    }
  }
}
