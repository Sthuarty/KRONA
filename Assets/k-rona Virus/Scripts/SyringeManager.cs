using UnityEngine;

public class SyringeManager : MonoBehaviour {


    public GameObject prefab;
    public GameObject curSyringe;

    public float interval = 10f;

    void Start() {
       InstantiateSyringe(); 
    }

    void Update() {
        if(curSyringe == null)
            Invoke("InstantiateSyringe", 5f);
        else
            CancelInvoke();
    }

    void InstantiateSyringe() {
        if(curSyringe == null)
            curSyringe = Instantiate(prefab, transform.position, transform.rotation, transform);
    }

}
