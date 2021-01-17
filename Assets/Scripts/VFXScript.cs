using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Photon.Pun;

public class VFXScript : MonoBehaviour
{
    public VisualEffect[] ChargeShot;

    public PhotonView photonView;

    private void Awake()
    {
        photonView = gameObject.GetComponent<PhotonView>();
    }

    [PunRPC]
    public void Reset()
    {
        foreach (VisualEffect fx in ChargeShot)
        {
            fx.Stop();
        }
    }

    [PunRPC]
    public void FX1Play()
    {
        ChargeShot[0].Play();
    }

    [PunRPC]
    public void FX2Play()
    {
        ChargeShot[1].Play();
    }

    [PunRPC]
    public void FX3Play()
    {
        ChargeShot[2].Play();
    }

    [PunRPC]
    public void FX1Stop()
    {
        ChargeShot[0].Stop();
    }

    [PunRPC]
    public void FX2Stop()
    {
        ChargeShot[1].Stop();
    }

    [PunRPC]
    public void FX3Stop()
    {
        ChargeShot[2].Stop();
    }


    [PunRPC]
    public void UpdatePos(Transform trans)
    {
        ChargeShot[0].transform.position = trans.position;
        ChargeShot[0].transform.rotation = trans.rotation;

        ChargeShot[1].transform.position = trans.position;
        ChargeShot[1].transform.rotation = trans.rotation;
    }

    [PunRPC]
    public void UpdatePos3(Transform trans)
    {
        ChargeShot[2].transform.position = trans.position;
        ChargeShot[2].transform.rotation = trans.rotation;
    }
}
