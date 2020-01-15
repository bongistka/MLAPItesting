using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayer : NetworkedBehaviour
{
    public Transform playerLocal;
    // Start is called before the first frame update
    void Start()
    {
        if (IsLocalPlayer)
        {
            playerLocal = GameObject.FindWithTag("FollowHead").transform;

            this.transform.SetParent(playerLocal);
            this.transform.localPosition = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
