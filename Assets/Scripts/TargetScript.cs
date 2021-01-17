using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TargetScript : MonoBehaviour
{
    public TextMesh textMesh;

    public string[] bits = { "Ouch!", "No!", "Stop it!", "Ahh!", "How dare y..?!", "U bit..!", "Dick Move!"};

    [PunRPC]
    public void Yell()
    {
        textMesh.text = bits[Random.Range(0, bits.Length)];
    }
}
