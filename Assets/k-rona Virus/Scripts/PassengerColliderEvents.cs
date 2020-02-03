using System.Collections;
using UnityEngine;
class PassengerColliderEvents : MonoBehaviour{

    public Passenger passenger;
    public bool isCollidingWithPlayer;
    public bool isWaitingForResult;
    public GameObject tutorialButtonsSprite;

    bool hasReleased;

    public GameObject iconBg;
    public SpriteRenderer iconSprite;

    public Sprite temperaturaIcon;
    public Sprite interrogatorioIcon;
    public Sprite xRayIcon;


    public PlayerColliderEvents playerColliderEvents;

    void Start() {
        passenger = GetComponentInParent<Passenger>();
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            playerColliderEvents = other.gameObject.GetComponent<PlayerColliderEvents>();
            isCollidingWithPlayer = true;
            tutorialButtonsSprite.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            isCollidingWithPlayer = false;
            tutorialButtonsSprite.SetActive(false);
        }
    }


    void Update() {
        if (Input.GetKeyUp(KeyCode.JoystickButton0)) {
            hasReleased = true;
            StopCoroutine(StartHoldTimer());
        }

        if(isCollidingWithPlayer) {
            
            // A Button
            if (Input.GetKeyDown(KeyCode.JoystickButton0)) {

                hasReleased = false;
                
                if(passenger.curPos == 1 && GetComponentInParent<PassengersInstantiator>().lastPassenger == null) {
                    StartCoroutine(passenger.GoToNextPos());
                }

                if(passenger.curPos == 2) {
                    if(playerColliderEvents.isHoldingSyringe){
                        Destroy(playerColliderEvents.syringe);
                        playerColliderEvents.isHoldingSyringe = false;
                        passenger.ReceiveVaccine();
                    }
                    else {
                        StartCoroutine(passenger.GoToNextPos());
                        passenger.boardTheRocket();
                    }
                }
            }

            // B Button
            if (Input.GetKeyDown(KeyCode.JoystickButton1)) {
                if(passenger.curPos == 2) { 
                    StartCoroutine(GetSymptomResult("Temperatura", temperaturaIcon));
                }
            }

            // X Button
            if (Input.GetKeyDown(KeyCode.JoystickButton2)) {
                if(passenger.curPos == 2) { 
                    StartCoroutine(GetSymptomResult("Interrogatório", interrogatorioIcon));
                }
            }

            // Y Button
            if (Input.GetKeyDown(KeyCode.JoystickButton3)) {
                if(passenger.curPos == 2) { 
                    StartCoroutine(GetSymptomResult("Raio X", xRayIcon));
                }
            }
        }
    }

    private IEnumerator StartHoldTimer() {

        yield return new WaitForSeconds(1.5F);

        if(hasReleased == false) {
        }
    }

    
    IEnumerator GetSymptomResult(string symptomName, Sprite sprite) {
        if(isWaitingForResult == false) {

            isWaitingForResult = true;
            tutorialButtonsSprite.SetActive(false);
            iconBg.SetActive(true);
            iconSprite.sprite = sprite;

            //yield return new WaitForSeconds(3f);

            Material iconBgMaterial = iconBg.GetComponent<Renderer>().material;
            foreach(Symptom symptom in passenger.interrogableSymptoms){
                if(symptom.name == symptomName) {
                    if(symptom.have){
                        iconBgMaterial.SetColor("_Color", new Color(1, 0.02f, 0));
                        //iconBgMaterial.color = Color.Lerp(Color.white, Color.red, 0.1f);
                    }
                    else{
                        iconBgMaterial.SetColor("_Color", new Color(0, 1, 0.5f));
                        //iconBgMaterial.color = Color.Lerp(Color.white, Color.green, 0.1f);
                    }

                }
            }

            yield return new WaitForSeconds(2f);

            iconBgMaterial.SetColor("_Color", new Color(1, 1, 1));
            iconBg.SetActive(false);
            isWaitingForResult = false;

        }
    }



}