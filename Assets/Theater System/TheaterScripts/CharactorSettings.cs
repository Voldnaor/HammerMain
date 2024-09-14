using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorSettings : MonoBehaviour
{

    [SerializeField] float speed;
    [Header("Waypoints")]
    WaypontsSystem wpoints;
    [SerializeField] int waypointindex;
    [SerializeField] bool moving = true;


    // public Animator animator;
    private void Start()
    {
        wpoints = GetComponent<WaypontsSystem>();
        // wpoints = GameObject.FindGameObjectWithTag("WaypointsTheater").GetComponent<WaypontsSystem>();
    }
    private void Update()
    {
        if (moving)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                wpoints.waypoints[waypointindex].position,
                speed * Time.deltaTime);
        }
        if (Vector2.Distance(transform.position, wpoints.waypoints[waypointindex].position) < 0.01f)
        {
            if (moving)
            {
                //StartCoroutine(Waiting(false));
            }

        }
    }
    /*
    IEnumerator Waiting(bool wait = true)
    {

        moving = false;
        yield return new WaitForSeconds(wait ? 2 : 0);
        if (waypointindex < wpoints.waypoints.Length - 1) { waypointindex++; }
        moving = true;
    }*/
}
