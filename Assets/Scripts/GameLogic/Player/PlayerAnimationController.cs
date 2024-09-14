using System;
using System.Collections;
using Spine.Unity;
using UnityEngine;
using Random = System.Random;

public class PlayerAnimationController : MonoBehaviour
{
    private SkeletonAnimation animator;
    private string anim;
    [SerializeField] private Transform hammer;

    private void Awake()
    {
        //ServiceLocator.Instance.RegisterService(this);
    }

    private void Start()
    {
        animator = gameObject.GetComponent<SkeletonAnimation>();
        StartCoroutine(AnimateIdle());
    }

    private void OnDestroy()
    {
        //ServiceLocator.Instance.DeregisterService<PlayerAnimationController>();
    }

    private IEnumerator AnimateIdle()
    {
        while (true)
        {
            animator.AnimationName = "idle_fast";
            if (animator.AnimationName =="idle_fast")
            {
                yield return new WaitForSeconds(8);
                animator.AnimationName = RandomAnimation();
                anim = animator.AnimationName;
            }

            if (animator.AnimationName==anim)
            {
                yield return new WaitForSeconds(2);
                animator.AnimationName = "idle_fast";
            }

        }
    }

     private string RandomAnimation()
     {
         var r = UnityEngine.Random.Range(1, 4);
         if (r==1)
         {
             return "hit";
         }
         if (r==2)
         {
             return "victory_fast";
             
         }
         return "tired";
         
     }

     public void LoseHammerAnimation()
     {
         StopAllCoroutines();
         animator.AnimationName = "lose_hammer";
     }
     
     public enum TakeBrickContext
     {
         tired,
         hit
     }
     
}

