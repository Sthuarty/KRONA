using System.Collections;
using UnityEngine;

public class Passenger : MonoBehaviour {

    public bool isSick;
    public int curPos;
    public bool isWalking;

    public float speed = 0.1f;

    public Transform[] positions;
    public float[] xPositions;

    public Symptom[] interrogableSymptoms = {
        new Symptom("Temperatura"),
        new Symptom("Interrogatório"),
        new Symptom("Raio X")
    };

    public Symptom[] discreteSymptoms = {
        new Symptom("Tosse"),
        new Symptom("Tontura")
    };


    void Start() {
        
        isSick = RandomBool(3);

        RandomizeInterrogablesSymptoms();
        RandomizeDiscretesSymptoms();

        StartCoroutine(GoToNextPos(Random.Range(0.0f, 2.5f)));
        
        /* Debug.Log("Is sick -> " + isSick);
        Debug.Log(interrogableSymptoms[0].name + " -> " + interrogableSymptoms[0].have);
        Debug.Log(interrogableSymptoms[1].name + " -> " + interrogableSymptoms[1].have);
        Debug.Log(interrogableSymptoms[2].name + " -> " + interrogableSymptoms[2].have);

        Debug.Log(discreteSymptoms[0].name + " -> " + discreteSymptoms[0].have);
        Debug.Log(discreteSymptoms[1].name + " -> " + discreteSymptoms[1].have); */
    }

    void Update() {

        if(isWalking){
            //Vector3 newPos = new Vector3(positions[curPos].position.x, transform.position.y, transform.position.z);
            Vector3 newPos = new Vector3(xPositions[curPos], transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, speed);
            
            //if(Vector3.Distance(transform.position, positions[curPos].position) < 0.1f)
            if(Vector3.Distance(transform.position, new Vector3(xPositions[curPos], transform.position.y, transform.position.z)) < 0.1f)
                isWalking = false;
        }
    }

    public IEnumerator GoToNextPos(float time = 0.1f){
        yield return new WaitForSeconds(time);
        curPos++;
        isWalking = true;
    }


    void RandomizeInterrogablesSymptoms() {

        Shuffle(interrogableSymptoms);

        if(isSick) {
            bool falseSymptomExists = false;

            for (int i = 0; i <= 2; i++) {
                if (falseSymptomExists == true)
                    interrogableSymptoms[i].have = true;
                else {
                    interrogableSymptoms[i].have = RandomBool();

                    if (!interrogableSymptoms[i].have)
                        falseSymptomExists = true;
                }
            }
        }

        else {
            interrogableSymptoms[0].have = RandomBool();
            
            if (!interrogableSymptoms[0].have)
                interrogableSymptoms[1].have = RandomBool();
        }
    }

    void RandomizeDiscretesSymptoms() {

        if(isSick) {
            for (int i = 0; i <= 1; i++)
                discreteSymptoms[i].have = RandomBool();
        }

        else {
            Shuffle(discreteSymptoms);
            discreteSymptoms[0].have = RandomBool();
            
            if (!discreteSymptoms[0].have)
                discreteSymptoms[1].have = RandomBool();
        }
    }


    bool RandomBool(int max = 1) {
        max++;
        if (Random.Range(0, max) == 0)
            return true;
        else
            return false;
    }

    void Shuffle(Symptom[] symptoms) {
        for (int t = 0; t < symptoms.Length; t++) {
            Symptom tmp = symptoms[t];
            int r = Random.Range(t, symptoms.Length);
            symptoms[t] = symptoms[r];
            symptoms[r] = tmp;
        }
    }

    public void ReceiveVaccine() {
        if(isSick)
            isSick = false;
    }

    public void boardTheRocket() {
        if(isSick == false)
            GameManager.points++;

        Debug.Log("GameManager.points, " + GameManager.points);
        Destroy(this.gameObject, 2f);
    }
}
