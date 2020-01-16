using MLAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkController : NetworkedBehaviour
{
    public Transform controllerTransform;
    public string controllerString;
    // Start is called before the first frame update
    void Start()
    {
        controllerTransform = GameObject.FindWithTag(controllerString).transform;

        this.transform.SetParent(controllerTransform);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
