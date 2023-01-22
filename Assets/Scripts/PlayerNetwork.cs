using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    private Animator anim;
    private NetworkVariable<int> lives = new NetworkVariable<int>(3, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


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

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Hier sind die Leben:" + lives.Value);
        }


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

        //transform.rotation = Quaternion.LookRotation(moveDir);
        float moveSpeed = 9f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (!IsOwner)
        {
            return;
        }

        Debug.Log("ES KOLLIDIERT!!!!!");

        if (collision.gameObject.tag == "weapon")
        {
            Debug.Log("ES KOL MIT SCHWERT");
            lives.Value--;
        }
    }
}
