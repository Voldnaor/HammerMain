using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Brick[] bricks;
    public TowerPoint[] towerPoints;

    public float brickReturningSpeed;

    private void Start()
    {
        
    }

    private void ResetTower()
    {
        //for(int i = 0; i < bricks.Length; i++ )
        //{
        //    foreach (var brick in bricks)
        //    { bricks[i].transform.position = towerPoints[i].transform.localScale; }
        //}
    }
}
