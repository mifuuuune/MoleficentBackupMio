using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Prototype.NetworkLobby
{
    public class CostomLobbyManager2 : NetworkLobbyManager
    {

        static public CostomLobbyManager2 s_Singleton;

        

        [Header("Playable Prefabs")]
        public GameObject player1;
        public GameObject player2;
        public GameObject player3;
        public GameObject player4;
        public CountPLayer x;
        private GameObject pg;
        private int player_num = 0;
        private int test = 0;
        private int spawn_player = 0;

        public void Awake()
        {
            s_Singleton = this;
        }
        public override void OnLobbyServerConnect(NetworkConnection conn)
        {
            player_num++;
            SpawnPG();
        }

        private void SpawnPG()
        {
            try { CountPLayer._instance.SpawnLobbyPlayer(player_num); }catch(Exception e) { Debug.Log(e); }

            
            /* if (player_num == 1)
            {
                pg = GameObject.Instantiate<GameObject>(this.playerLobby1, new Vector3(-2.141f, 0.02f, -1.639f), Quaternion.Euler(0, 180f, 0));
                CountPLayer._instance.SpawnLobbyPlayer(pg);
            }
            else if (player_num == 2)
            {
                pg = GameObject.Instantiate<GameObject>(this.playerLobby2, new Vector3(-0.548f, 0.02f, -1.639f), Quaternion.Euler(0, 180f, 0));
                CountPLayer._instance.SpawnLobbyPlayer(pg);
            }
            else if (player_num == 3)
            {
                pg = GameObject.Instantiate<GameObject>(this.playerLobby3, new Vector3(0.921f, 0.02f, -1.639f), Quaternion.Euler(0, 180f, 0));
                CountPLayer._instance.SpawnLobbyPlayer(pg);
            }
            else if (player_num == 4)
            {
                pg = GameObject.Instantiate<GameObject>(this.playerLobby4, new Vector3(2.365f, 0.02f, -1.639f), Quaternion.Euler(0, 180f, 0));
                CountPLayer._instance.SpawnLobbyPlayer(pg);
                player_num = 0;

            }*/

        }
        /*
        public override void OnLobbyClientEnter()
        {
            Debug.Log("sto ottenendo l'oggetot in client method");

            Debug.Log("ottenuto l'oggetto");
            //Debug.Log("sono il client con risultato->"+ num);
            if (player_num == 0)
            {
                Debug.Log("foreach-> " + test);
                int x = NetworkClient.allClients.Count;
                Debug.Log("client=" + numPlayers);
                for (int i = 0; i < numPlayers; i++)
                {
                    if (i == 0)
                    {
                        pg = GameObject.Instantiate<GameObject>(this.playerLobby2, new Vector3(2.365f, 0.02f, -1.639f), Quaternion.Euler(0, 180f, 0));

                    }
                    else if (i == 1)
                    {
                        pg = GameObject.Instantiate<GameObject>(this.playerLobby3, new Vector3(-0.548f, 0.02f, -1.639f), Quaternion.Euler(0, 180f, 0));

                    }
                    else if (i == 2)
                    {
                        pg = GameObject.Instantiate<GameObject>(this.playerLobby4, new Vector3(0.921f, 0.02f, -1.639f), Quaternion.Euler(0, 180f, 0));
                    }

                }
            }
            base.OnLobbyClientEnter();
        }*/

        public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
        {

            GameObject myPlayer = null;
            spawn_player++;
            if (spawn_player == 1)
                myPlayer = Instantiate(player1, GameObject.Find("sir_bean_spwan").transform.position, Quaternion.identity) as GameObject;
            if (spawn_player == 2)
                myPlayer = Instantiate(player2, GameObject.Find("sir_eal_spawn").transform.position, Quaternion.identity) as GameObject;
            if (spawn_player == 3)
                myPlayer = Instantiate(player3, GameObject.Find("sir_loin_spawn").transform.position, Quaternion.identity) as GameObject;
            if (spawn_player == 4)
            {
                spawn_player = 0;
                myPlayer = Instantiate(player4, GameObject.Find("sir_sage_spawn").transform.position, Quaternion.identity) as GameObject;
            }
            return myPlayer;
        }

        public override void OnLobbyStopHost()
        {
            //base.OnLobbyServerDisconnect(conn);
            player_num=0;
        }

        public override void OnLobbyServerDisconnect(NetworkConnection conn)
        {
            try { CountPLayer._instance.DespawnLobbyPlayer(player_num); } catch (Exception e) { Debug.Log(e); }
            player_num--;
        }

    }
}

