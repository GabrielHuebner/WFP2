using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private Animator anim;
    private int lives = 3;
    private int deaths = 0;
    private int kills = 0;

    private NetworkVariable<int> deathsPlayer0 = new NetworkVariable<int>(0);
    private NetworkVariable<int> deathsPlayer1 = new NetworkVariable<int>(0);
    private NetworkVariable<int> deathsPlayer2 = new NetworkVariable<int>(0);
    private NetworkVariable<int> deathsPlayer3 = new NetworkVariable<int>(0);


    public override void OnNetworkSpawn()
    {
        anim = gameObject.GetComponent<Animator>();
        switch (OwnerClientId)
        {
            case 0:
                Debug.Log("First Player/Host");
                transform.position = new Vector3(15, 0.5f, 15);
                transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                break;
            case 1:
                Debug.Log("Second Player");
                transform.position = new Vector3(-15, 0.5f, 15);
                transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                break;
            case 2:
                Debug.Log("Third Player");
                transform.position = new Vector3(15, 0.5f, -15);
                transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                break;
            case 3:
                Debug.Log("Fourth Player");
                transform.position = new Vector3(-15, 0.5f, -15);
                transform.GetChild(1).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;

        Vector3 moveDir = new Vector3(0, 0, 0);


        if (Input.GetKey(KeyCode.W)) 
        { 
            moveDir.z = +1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir.z = -1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir.x = -1f;
        }
        if (Input.GetKey(KeyCode.D)) 
        { 
            moveDir.x = +1f; 
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isAttacking", true);
            /*if (!anim.GetCurrentAnimatorStateInfo(0).IsName("isAttacking"))
            {
                GetComponentInChildren<BoxCollider>().enabled = true;
            }*/
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }

        if(!(moveDir.z == 0 && moveDir.x == 0))
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        if(moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDir);
        }
        
        float moveSpeed = 9f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "weapon" && GetComponentInChildren<BoxCollider>() != other)
        {
            lives--;
            Debug.Log("Hier sind die Leben: " + lives);
            if(lives <= 0)
            {
                other.gameObject.GetComponentInParent<CapsuleCollider>().gameObject.GetComponent<PlayerNetwork>().kills++;
                other.gameObject.GetComponentInParent<CapsuleCollider>().gameObject.GetComponent<PlayerNetwork>().UpdateUIText();
                PlayerDies();
            }
            Debug.Log("ES KOL MIT SCHWERT");

            //other.enabled = false;
        }
    }

    private void PlayerDies()
    {
        lives = 3;
        switch (OwnerClientId)
        {
            case 0:
                Debug.Log("First Player/Host dies");
                transform.position = new Vector3(15, 0.5f, 15);
                deaths++;
                NetworkManagerUI.Instance.setTextP1("P1 K:" + kills + " / D:" + deaths);
                break;
            case 1:
                Debug.Log("Second Player dies");
                transform.position = new Vector3(-15, 0.5f, 15);
                deaths++;
                NetworkManagerUI.Instance.setTextP2("P2 K:" + kills + " / D:" + deaths);
                break;
            case 2:
                Debug.Log("Third Player dies");
                transform.position = new Vector3(15, 0.5f, -15);
                deaths++;
                NetworkManagerUI.Instance.setTextP3("P3 K:" + kills + " / D:" + deaths);
                break;
            case 3:
                Debug.Log("Fourth Player dies");
                transform.position = new Vector3(-15, 0.5f, -15);
                deaths++;
                NetworkManagerUI.Instance.setTextP4("P4 K:" + kills + " / D:" + deaths);
                break;
        }
    }

    public void UpdateUIText()
    {
        switch (OwnerClientId)
        {
            case 0:
                NetworkManagerUI.Instance.setTextP1("P1 K:" + kills + " / D:" + deaths);
                break;
            case 1:
                NetworkManagerUI.Instance.setTextP2("P2 K:" + kills + " / D:" + deaths);
                break;
            case 2:
                NetworkManagerUI.Instance.setTextP3("P3 K:" + kills + " / D:" + deaths);
                break;
            case 3:
                NetworkManagerUI.Instance.setTextP4("P4 K:" + kills + " / D:" + deaths);
                break;
        }
    }
}
