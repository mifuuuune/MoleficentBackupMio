﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.NetworkLobby
{
    public class MainMenuLobby : MonoBehaviour
    {
        public void OnClickHost()
        {
            CostomLobbyManager2.s_Singleton.StartHost();
        }

    }
}
