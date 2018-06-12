using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrahannyRespawnerBehaviour : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {

            BasicController playerController = collision.gameObject.GetComponent<BasicController>();
            playerController.DecreaseLives();
            playerController.Respawn(Vector3.zero);

        }
        else
        {
            collision.gameObject.SetActive(false);
        }
    }
}
