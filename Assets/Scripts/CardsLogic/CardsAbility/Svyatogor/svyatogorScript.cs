using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class svyatogorScript : MonoBehaviour
{
    public GameObject svyatogorSprite;
    //public float delaySvyatorog = 0.5f;
    

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {

       

    }

    public void AttackEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<GameObject> targets = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (IsVisible(enemy.transform))
            {
                targets.Add(enemy);
            }
        }

        if (targets.Count > 0)
        {
            
            GameObject randomEnemy = targets[Random.Range(0, targets.Count)];

            
            GameObject svyatogor = Instantiate(svyatogorSprite, randomEnemy.transform.position, Quaternion.identity);

            
            Destroy(randomEnemy);

            
            //StartCoroutine(HideAbilitySprite(svyatogor));
        }
    }

    bool IsVisible(Transform enemyTransform)
    {
        
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(enemyTransform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
    }

    //IEnumerator HideAbilitySprite(GameObject sprite)
    //{
    //    yield return new WaitForSeconds(delaySvyatorog);
    //    Destroy(sprite);
    //}
}

