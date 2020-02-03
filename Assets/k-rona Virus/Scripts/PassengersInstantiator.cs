using UnityEngine;

public class PassengersInstantiator : MonoBehaviour {

    public GameObject[] prefabs;
    public GameObject prefab;
    public GameObject curPassenger;
    public GameObject lastPassenger;


    void Start() {
        InstantiatePassenger();
    }

    void Update() {
        if(GameManager.spawnedPassengers < GameManager.numberOfPassengersToGenerate) {
            if(curPassenger.GetComponent<Passenger>().curPos == 2){
                lastPassenger = curPassenger;
                InstantiatePassenger();
            }
        }
    }

    void InstantiatePassenger() {

        prefab = prefabs[Random.Range(0, prefabs.Length)];

        GameManager.spawnedPassengers++;
        Debug.Log("GameManager.spawnedPassengers, " + GameManager.spawnedPassengers);
        curPassenger = Instantiate(prefab, transform.position, transform.rotation, transform);
    }
}
