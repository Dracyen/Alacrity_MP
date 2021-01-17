using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Photon.Pun;

public class BulletBehavior : MonoBehaviour
{
    Rigidbody RB;

    public bool isMoving = false;

    public Transform Home;

    public float Speed;

    public float Range;

    public PhotonView photonView;

    private void Awake()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        RB = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if(RB.useGravity == true)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        if(Vector3.Distance(transform.position, Home.transform.position) > Range)
        {
            Reset();
        }
    }

    public void Shoot(Transform Origin)
    {
        var angle = Origin.transform.forward;

        RB.transform.position = Origin.position;

        Vector3 move = angle * Speed;

        RB.useGravity = true;
        RB.AddForce(move);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            //other.gameObject.GetComponent<TargetScript>().Yell();
            other.gameObject.GetComponent<PhotonView>().RPC("Yell", RpcTarget.All);
        }
        Reset();
    }

    private void Reset()
    {
        RB.useGravity = false;
        RB.velocity = new Vector3();
        RB.transform.position = Home.position;
    }
}
