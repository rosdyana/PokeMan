using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePortal : MonoBehaviour
{
    public GameObject PortalOut;
    private void OnCollisionEnter(Collision collision)
    {

        collision.gameObject.transform.position = PortalOut.transform.position;
    }
}
