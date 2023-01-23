using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    //private NetworkManagerUI networkManagerUI;
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
        {
            return;
        }
    }

    public void setUIText()
    {

    }
}
