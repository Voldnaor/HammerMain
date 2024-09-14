using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Brick : MonoBehaviour
{
    private Vector3 smallSize;
    private Vector3 normalSize;

    public bool isFinalHammer;

    private Transform tower;

    private bool canChangeSize = true;
    private bool changeSizeProcessToSmall;

    public Transform localToverPosition;
    [SerializeField] private float returnSpeed;
    private bool goingToTowerPoint;

    private Rigidbody2D rb;

    private void Start()
    {
        returnSpeed = FindObjectOfType<Tower>().brickReturningSpeed;
        rb = GetComponent<Rigidbody2D>();

        normalSize = transform.localScale;
        smallSize = transform.localScale/2;

        if (!isFinalHammer)
        {
            tower = FindObjectOfType<Tower>().gameObject.transform;
            transform.position = localToverPosition.position;
            InTower();
        }
    }

    //public void CheckBrickStatus()
    //{

    //}

    private void Update()
    {

        //if (canChangeSize)
        //{
        //    if (changeSizeProcessToSmall)
        //    {
        //        transform.localScale = Vector3.Lerp(transform.localScale, smallSize, 1f);
        //    }
        //    else if (!changeSizeProcessToSmall)
        //    {
        //        transform.localScale = Vector3.Lerp(transform.localScale, normalSize, 1f);
        //    }
        //}

        if(goingToTowerPoint)
        {
            if (Vector2.Distance(transform.position, localToverPosition.position) > 0.03f)
            {
                rb.velocity = Vector2.zero;
                transform.position = Vector3.Lerp(transform.position, localToverPosition.position, returnSpeed * Time.deltaTime);
            }
            else if(Vector2.Distance(transform.position, localToverPosition.position) <= 0.03f)
            {
                goingToTowerPoint = false;
                InTower();
            }
        }
        
    }

    public void Stolen()
    {
        Debug.Log("stolen");
        NormalSize();
        Interactable();
        rb.isKinematic = false;
        transform.parent = null;
    }

    public void InHands(Transform handPosition)
    {
        if (isFinalHammer)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            FindObjectOfType<PlayerAnimationController>().LoseHammerAnimation();
        }
        NotInteractable();
        rb.isKinematic = true;
        SmallSize();

        transform.position = handPosition.position;
        transform.SetParent(handPosition, true);////////////////////////
        GetComponent<SpriteRenderer>().sortingOrder = handPosition.parent.GetComponent<MeshRenderer>().sortingOrder - 1;
    }
    public void InTower()
    {
        rb.simulated = true;
        Interactable();
        NormalSize();
    }
    public void Leviosa()
    {
        rb.isKinematic = true;
        NotInteractable();
        rb.velocity = Vector3.zero;
        NormalSize();
        goingToTowerPoint = true;
        transform.rotation = Quaternion.identity;
    }

    private void NormalSize()
    {
        if (!isFinalHammer)
        {
            //changeSizeProcessToSmall = false;
            transform.localScale = normalSize;
        }
    }

    private void SmallSize()/*потом передавать нынешнее размер для lerp*/
    {
        if (!isFinalHammer)
        {
            transform.localScale = smallSize;
            gameObject.transform.localScale = new Vector2(smallSize.x, smallSize.y);
        }
    }

    private void Interactable()
    {
        Collider2D[] collider2Ds = GetComponents<Collider2D>();
        foreach (Collider2D collider2D in collider2Ds) { collider2D.enabled = true; }
    }

    private void NotInteractable()
    {
        Collider2D[] collider2Ds = GetComponents<Collider2D>();
        foreach (Collider2D collider2D in collider2Ds) { collider2D.enabled = false; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HammerBounce>())
        {
            Leviosa();
        }
    }
}
