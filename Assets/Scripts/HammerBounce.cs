using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBounce : MonoBehaviour
{
    public int bounce;
    public bool isOnGround = false;

    HammerUse hammerUse;
    // Start is called before the first frame update
    void Start()
    {

        hammerUse = GameObject.Find("HammerControll").GetComponent<HammerUse>();
        hammerUse.initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bounce++;

        if(bounce >= 5)
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            hammerUse.throwDistance = Vector3.Distance(hammerUse.initialPos, transform.position);
            hammerUse.addDamage = Mathf.Lerp(0, hammerUse.maxAdditionalDamage, hammerUse.throwDistance / hammerUse.maxDistance);
            hammerUse.damage = hammerUse.baseDamage + hammerUse.addDamage;

            hammerUse.finalDamage = Mathf.RoundToInt(hammerUse.damage);

            Debug.Log(hammerUse.finalDamage);
            //Debug.Log("Enemy kill");


        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnGround = true;
            
            StartCoroutine(DestroyAfterDelay(3f));
        }
    }

    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isOnGround)
        {
            Destroy(this.gameObject);
            isOnGround = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Floor"))
        {
            isOnGround = false;
            
            StopCoroutine("DestroyAfterDelay");
        }
    }
}
