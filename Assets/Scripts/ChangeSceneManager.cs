using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ChangeSceneManager : NetworkBehaviour
{

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 9)
        {
            Debug.Log("change scene");
            CmdChange("Level 1");
            //Invoke("CmdChange", 1.0f);
        }
    }*/

    public void changeScene(string s)
    {
        CmdChange(s);
    }

    [Command]
    public void CmdChange(string scene)
    {
        RpcSaveSpawn();

        Prototype.NetworkLobby.CostomLobbyManager2.s_Singleton.ServerChangeScene(scene);
        if (GameObject.FindWithTag("Bean").GetComponent<NetworkIdentity>().hasAuthority)
        {
            Debug.Log("bean -> ho autorità");
            GameObject.FindWithTag("Bean").transform.position = new Vector3(-9.5f, 5.0f, -33.5f);
        }
    }

    //affinchè i player possano respawnare devono rendere gli spawn point permanenti
    [ClientRpc]
    public void RpcSaveSpawn()
    {
        //sposto i personaggi, cambiare le coordinate dei vector3 (attenzione a dove si mette!!!!!)


        if (GameObject.FindWithTag("Loin"))
        {
            Debug.Log("Loin -> ho autorità");
            GameObject.FindWithTag("Loin").transform.position = new Vector3(-3.5f, 5.0f, -8.2f);
        }
        else if (GameObject.FindWithTag("Eal")/*.GetComponent<NetworkIdentity>().hasAuthority*/)
        {
            Debug.Log("eal -> ho autorità");
            GameObject.FindWithTag("Eal").transform.position = new Vector3(-9.5f, 5.0f, -33.5f);
        }
        else if (GameObject.FindWithTag("Bean"))
        {
            Debug.Log("bean -> ho autorità");
            GameObject.FindWithTag("Bean").transform.position = new Vector3(-9.5f, 5.0f, -33.5f);
        }
        else if (GameObject.FindWithTag("Sage"))
        {
            Debug.Log("Sage -> ho autorità");
            GameObject.FindWithTag("Sage").transform.position = new Vector3(-9.5f, 5.0f, -33.5f);
        }

        //sposto gli spawnpoints
        GameObject.Find("sir_loin_spawn").transform.position = new Vector3(-3.5f, 5.0f, -8.2f);
        GameObject.Find("sir_eal_spawn").transform.position = new Vector3(-9.5f, 5.0f, -33.5f);
        GameObject.Find("sir_bean_spawn").transform.position = new Vector3(-9.5f, 5.0f, -33.5f);
        GameObject.Find("sir_sage_spawn").transform.position = new Vector3(-9.5f, 5.0f, -33.5f);
    }
}

   

