using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform target;
    public float smoothing;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void LateUpdate() {
        if(transform.position != target.position) {
            // Per risolvere bug della posizione della telecamera, rende l'asse z fisso in modo che non si allontani sull'asse z
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            
            //Funzione Lerp per muovere la telecamera in modo liscio (smooth)
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
