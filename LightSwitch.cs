using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour {
  public List<GameObject> EmissionObjectList;
  public GameObject LightSource;
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
        if (HitDetection.collider.gameObject == transform.parent.gameObject)
          if (PowerConnection) {
            PowerConnection = !PowerConnection;

            foreach (GameObject EmissionObject in EmissionObjectList)
              EmissionObject.GetComponent<Renderer>().material = SwitchOffMaterial;

            LightSource.SetActive(false);

            transform.localScale = Vector3.right + Vector3.down + Vector3.forward;
          } else {
            PowerConnection = !PowerConnection;

            foreach (GameObject EmissionObject in EmissionObjectList)
              EmissionObject.GetComponent<Renderer>().material = SwitchOnMaterial;

            LightSource.SetActive(true);

            transform.localScale = Vector3.one;
          }
    }
  }
}
