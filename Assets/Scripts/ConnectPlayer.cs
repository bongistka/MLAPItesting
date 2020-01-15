using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPlayer : MonoBehaviour
{
    public GameObject playerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        NetworkingManager.Singleton.StartHost();
        //GameObject go = Instantiate(playerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
