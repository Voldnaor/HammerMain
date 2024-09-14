using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshrutkaMove : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 currentDirection = Vector3.right;
    public bool ignoreRightCollision = false;
    private float delayTimer = 5f; // Время задержки в секундах

    public float marshrutkaDamage;
    public HammerUse hammerUse;
    public PlayerController playerController;
    public int finalMarshrutkaDamage;

    private bool hasStartedMoving = false;

    public void Start()
    {
        hammerUse = GameObject.Find("HammerControll").GetComponent<HammerUse>();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        marshrutkaDamage = hammerUse.baseDamage * 200 * (100 % +40 * playerController.marshrutkaCardsPoint - 1);
        finalMarshrutkaDamage = Mathf.RoundToInt(marshrutkaDamage);
        Debug.Log(finalMarshrutkaDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Right") && !ignoreRightCollision)
        {
            currentDirection = Vector3.left;
            ignoreRightCollision = true;
        }

        if (collision.CompareTag("Left") && !ignoreRightCollision)
        {
            ignoreRightCollision = true;
        }
    }

    public void Update()
    {
        if (!hasStartedMoving)
        {
            delayTimer -= Time.deltaTime;
            if (delayTimer <= 0)
            {
                hasStartedMoving = true;
            }
        }

        if (hasStartedMoving) 
        {
            transform.Translate(currentDirection * speed * Time.deltaTime);
        }
    }

    void OnBecameInvisible()
    {
        hasStartedMoving = false;
    }
}
