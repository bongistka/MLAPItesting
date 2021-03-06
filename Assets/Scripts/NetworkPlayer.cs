﻿using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkPlayer : NetworkedBehaviour
{
    public Transform playerLocal;
    public Text playerName;
    // Start is called before the first frame update
    void Start()
    {
        if (IsLocalPlayer)
        {
            playerLocal = GameObject.FindWithTag("FollowHead").transform;

            this.transform.SetParent(playerLocal);
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;

            playerName.text = SystemInfo.deviceName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
