using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Cinemachine;

public class CameraController : NetworkBehaviour
{ 
    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera = null;

    private CinemachineTransposer transposer;

    public override void OnStartAuthority()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        virtualCamera.gameObject.SetActive(true);
        
        enabled = true;
    }

    // Update is called once per frame
    /*void Update()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }*/

}
