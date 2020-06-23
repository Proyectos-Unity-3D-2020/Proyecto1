using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

public class MoveWithFloor : MonoBehaviour
{
    CharacterController player;

    Vector3 groundPosition;
    Vector3 lastGroundPosition;
    string groundName;
    string lastGroundName;

    // Start is called before the first frame update
    void Start()
    {
        player = this.GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isGrounded)
        {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, player.height / 5.2f, -transform.up, out hit))
            {
                GameObject groundedIn = hit.collider.gameObject;
                groundName = groundedIn.name;
                groundPosition = groundedIn.transform.position;
                if (groundPosition != lastGroundPosition && groundName == lastGroundName)
                {
                    UnityEngine.Debug.Log("Es El mismo suelo");
                    Vector3 v = new Vector3(groundPosition.x - lastGroundPosition.x, groundPosition.y - lastGroundPosition.y, groundPosition.z - lastGroundPosition.z);
                    this.transform.position = v;
                    UnityEngine.Debug.Log(v);
                    player.Move(v);

                }

                lastGroundName = groundName;
                lastGroundPosition = groundPosition;
            }

        }
        else if (!player.isGrounded)
        {
            lastGroundName = null;
            lastGroundPosition = Vector3.zero;
        }
    }

    private void OnDrawGizmos()
    {
        player = this.GetComponent<CharacterController>();
        Gizmos.DrawWireSphere(transform.position, player.height / 5.2f);
    }
}
