using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RespawnWithDelay : NetworkBehaviour {

    public float Delay = 0f;

    public void MoleAbility()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == 9)
                child.parent = null;
        }
        gameObject.SetActive(false);
        Invoke("CmdReEnable", Delay);
    }

    [Command]
    private void CmdReEnable()
    {
        gameObject.SetActive(true);
    }

}
