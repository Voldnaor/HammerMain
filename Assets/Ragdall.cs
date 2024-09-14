using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class Ragdall : MonoBehaviour
{
    public GameObject[] BodyParts;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Ragdoll();
        }
    }

    private void Ragdoll()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        //gameObject.RemovedComponent<Rigidbody2D>().enabled = false;

        for (int i = 0; i< BodyParts.Length; i++)
        {
            BodyParts[i].GetComponent<Rigidbody2D>().isKinematic = false;
            BodyParts[i].GetComponent<Collider2D>().enabled=true;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("hammer"))
        {
            Ragdoll();
            Destroy(collision.gameObject);
        }
    }
}
