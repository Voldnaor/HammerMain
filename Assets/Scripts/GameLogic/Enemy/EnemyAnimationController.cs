using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private SkeletonAnimation anim;
    private EnemyAnimation animation;
    void Start()
    {
        anim.AnimationName = animation.GetAnimName("Walk");
    }

    private void Awake()
    {
        anim = GetComponent<SkeletonAnimation>();
    }
}
