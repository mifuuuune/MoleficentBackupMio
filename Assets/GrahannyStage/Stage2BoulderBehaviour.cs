using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2BoulderBehaviour : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        GameObject go = collision.gameObject;
        if (go.tag == "Grahanny")
        {
            Destroy(go);
            //fine partita
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
