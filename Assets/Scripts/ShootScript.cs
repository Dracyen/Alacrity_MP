using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.VFX;
using UnityEngine.VFX;
using Photon.Pun;

public class ShootScript : MonoBehaviour
{
    public GameObject[] SmlBullets;

    public GameObject[] BigBullets;

    public Transform Origin;

    public VFXScript vfx;

    bool isPlaying0 = false;
    bool isPlaying1 = false;

    float arrowDmg = 0;
    float arrowDmgMax = 50;
    float arrowPull = 0;
    float arrowPullMax = 2;

    public PhotonView photonView;

    public VFXScript vfxScript;

    private void Awake()
    {
        photonView = gameObject.GetComponent<PhotonView>();

        vfxScript = gameObject.GetComponent<VFXScript>();

        photonView.RPC("Reset", RpcTarget.All);
    }

    void Update()
    {
        if(photonView.IsMine)
        Shoot();
    }

    private void LateUpdate()
    {
        //vfxScript.photonView.RPC("UpdatePos", RpcTarget.All, Origin); 
    }

    void Shoot()
    {
        if(Input.GetMouseButtonDown(0))
        {
            photonView.RPC("Reset", RpcTarget.All);
        }

        if (arrowPull < arrowPullMax)
        {
            if (Input.GetMouseButton(0))
            {
                arrowPull += Time.deltaTime;

                if (arrowPull > 0.5f)
                {
                    if (isPlaying0 == false)
                    {
                        //ChargeShot[0].Play();
                        isPlaying0 = true;
                    }
                }
            }
        }
        else
        {
            //ChargeShot[0].Stop();

            if(isPlaying1 == false)
            {
                //ChargeShot[1].Play();
                isPlaying1 = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //ChargeShot[0].Stop();
            //ChargeShot[1].Stop();

            isPlaying0 = false;
            isPlaying1 = false;

            if(arrowPull >= arrowPullMax)
            {
                //ChargeShot[2].Play();

                foreach (GameObject Bullet in BigBullets)
                {
                    if (Bullet.GetComponent<BulletBehavior>().isMoving == false)
                    {
                        Bullet.GetComponent<BulletBehavior>().Shoot(Origin.transform);
                        break;
                    }
                }
                photonView.RPC("UpdatePos3", RpcTarget.All, Origin);
            }
            else
            {
                foreach(GameObject Bullet in SmlBullets)
                {
                    if(Bullet.GetComponent<BulletBehavior>().isMoving == false)
                    {
                        Bullet.GetComponent<BulletBehavior>().Shoot(Origin.transform);
                        break;
                    }
                }
            }

            arrowDmg = (int)(arrowPull * arrowDmgMax) / arrowPullMax;

            Reset();
        }
    }

    void Reset()
    {
        arrowDmg = 0;

        arrowPull = 0;
    }
}
