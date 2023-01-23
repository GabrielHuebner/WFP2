using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : NetworkBehaviour
{
    private TextMeshProUGUI textMeshPlayer1;
    private TextMeshProUGUI textMeshPlayer2;
    private TextMeshProUGUI textMeshPlayer3;
    private TextMeshProUGUI textMeshPlayer4;

    public static NetworkManagerUI Instance { get; private set; }
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

    private TextMeshPro textMesh;
    private void Start()
    {
        textMeshPlayer1 = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        textMeshPlayer2 = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        textMeshPlayer3 = transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        textMeshPlayer4 = transform.GetChild(3).gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void setTextP1(String text)
    {
        textMeshPlayer1.text = text;
    }

    public void setTextP2(String text)
    {
        textMeshPlayer2.text = text;
    }
    public void setTextP3(String text)
    {
        textMeshPlayer3.text = text;
    }
    public void setTextP4(String text)
    {
        textMeshPlayer4.text = text;
    }
}
