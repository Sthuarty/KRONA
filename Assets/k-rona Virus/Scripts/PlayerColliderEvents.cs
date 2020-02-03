using System.Collections;
using UnityEngine;

public class PlayerColliderEvents : MonoBehaviour {
    
    bool isCollidingWithSyringe;
    bool isSyringecomming;
    public bool isHoldingSyringe;

    bool hasReleased;

    public GameObject syringe;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Syringe") {
            isCollidingWithSyringe = true;
            syringe = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Syringe") {
            isCollidingWithSyringe = false;
        }
    }

    void Update() {
        if(isCollidingWithSyringe) {
            if (Input.GetKeyDown(KeyCode.JoystickButton0)) {
                isSyringecomming = true;
                isHoldingSyringe = true;
            }
        }

        if(isSyringecomming) {
            syringe.transform.SetParent(transform);
            syringe.transform.position = Vector3.Lerp(syringe.transform.position, transform.position, 0.1f);
            
            if(Vector3.Distance(syringe.transform.position, transform.position) < 0.1f)
                isSyringecomming = false;
        }

        if(isHoldingSyringe){
            if (Input.GetKeyDown(KeyCode.JoystickButton0)) {
                hasReleased = false;
                StartCoroutine(StartHoldTimer());
            }
            if (Input.GetKeyUp(KeyCode.JoystickButton0)) {
                hasReleased = true;
                StopCoroutine(StartHoldTimer());
            }
        }
    }

    private IEnumerator StartHoldTimer() {

        yield return new WaitForSeconds(1.5F);

        if(hasReleased == false) {
            Destroy(syringe);
        }
    }
}