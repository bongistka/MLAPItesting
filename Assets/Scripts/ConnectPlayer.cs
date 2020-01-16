using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject leftControllerPrefab;
    public GameObject rightControllerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        NetworkingManager.Singleton.StartHost();
        //GameObject leftController = Instantiate(leftControllerPrefab);
        //GameObject rightController = Instantiate(rightControllerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
