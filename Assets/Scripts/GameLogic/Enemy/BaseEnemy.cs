using Spine.Unity;
using Spine.Unity.Examples;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour
{
  [SerializeField] private SkeletonRagdoll2D ragdoll;
    [SerializeField] private float HP;
    [SerializeField] private float Speed;

    private SkeletonAnimation anim;
    private EnemyManager _enemyPool;
    private Vector2 startRagdoll;

    private float lastDamage;
    private bool isTakeAnim;
    private bool inRagdoll;

    public Transform ToSpawnMove;
    public Transform Hand;
    //public Brick BrickHand;
    //public Brick tempBrick;
    public EnemyAnimation animation;

    public bool isStan;

    public Action Dead;
    public Action Damaged;
    public Action RestoredAfterHit;
    private PlayerAnimationController _playerAnimation;
    public bool isDead;

    public float speed;

    private void Awake()
    {
        anim = GetComponent<SkeletonAnimation>();
    }

    private void Start()
    {
        //if (ServiceLocator.Instance.GetService<PlayerAnimationController>() != null)
        //    _playerAnimation = ServiceLocator.Instance.GetService<PlayerAnimationController>();

        ragdoll = gameObject.GetComponent<SkeletonRagdoll2D>();
        //_enemyPool = ServiceLocator.Instance.GetService<EnemyManager>();
        speed = Speed;
    }

    private void Update()
    {
        Speed = speed;
        if (!isTakeAnim)
        {
            if (!isStan)
            {
                transform.position = Vector3.MoveTowards(transform.position, ToSpawnMove.transform.position,
                    Time.deltaTime * Speed);
                if (Vector3.Distance(this.transform.position, ToSpawnMove.transform.position) < 0.1)
                {

                    // WalkedAway?.Invoke();
                    StopAllCoroutines();
                    try
                    {
                        ragdoll.StopAllCoroutines();
                        ragdoll.Remove();
                    }
                    catch
                    {
                    }

                    //if (BrickHand != null)
                    //{
                    //    Destroy(BrickHand.gameObject);
                    //}

                    //_enemyPool.EnemyList.Remove(this);
                    //gameObject.SetActive(false);
                    StartCoroutine(DeadAnim(Vector3.back));

                }
            }
        }
    }

    private void OnEnable()
    {
        RestoredAfterHit?.Invoke();
        anim.AnimationName = animation.GetAnimName("Walk");
        if (gameObject.GetComponent<Rigidbody2D>() == null)
        {
            var r = gameObject.AddComponent<Rigidbody2D>();
            if (r != null)
            {
                r.isKinematic = true;
            }
        }

        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        var c = GetComponent<PolygonCollider2D>();
        if (c != null)
        {
            c.enabled = true;
        }


        isTakeAnim = false;
        isStan = false;
        inRagdoll = false;
    }


    //private void DropBrick(AbstractWeapon weapon)
    //{
    //    BrickHand.transform.SetParent(BrickHand.Container.FreeBricks.transform);
    //    BrickHand.currentWeapon = weapon;
    //    BrickHand.GetComponent<Collider2D>().enabled = true;
    //    BrickHand.AddRigidBody();
    //    Destroy(BrickHand.GetComponent<Canvas>());
    //    BrickHand = null;
    //}


    private IEnumerator DeadAnim(Vector3 dir)
    {
        //if (tempBrick != null)
        //{
        //    tempBrick.isTaking = false;
        //}

        isTakeAnim = false;
        isStan = true;
        anim.AnimationName = null;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        gameObject.GetComponent<Collider2D>().enabled = false;
        ragdoll.Apply();
        ragdoll.RootRigidbody.velocity = dir * 0.7f;
        //isDead = true;
        var estimatedPos = ragdoll.EstimatedSkeletonPosition;
        yield return new WaitForSeconds(1);

        ragdoll.RootRigidbody.isKinematic = false;

        var position = transform.position;
        ragdoll.SetSkeletonPosition(new Vector3(ragdoll.RootRigidbody.position.x, estimatedPos.y,
            position.z));
        anim.AnimationName = animation.GetAnimName("Walk");

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        ragdoll.RootRigidbody.isKinematic = true;
        ragdoll.SetSkeletonPosition(new Vector3(ragdoll.RootRigidbody.position.x, estimatedPos.y,
            position.z));

        yield return ragdoll.SmoothMix(0, 1f);
        ragdoll.Remove();
        var colliders = ragdoll.gameObject.GetComponentsInChildren<Collider2D>();
        foreach (var item in colliders)
        {
            //Destroy(item.GetComponent<EnemyBoneDamage>());
        }

        var r = gameObject.AddComponent<Rigidbody2D>();

        r.isKinematic = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        //if (BrickHand != null)
        //{
        //    anim.AnimationName = animation.GetAnimName("WalkWithBrick");
        //}
        //else
        //{
        //    anim.AnimationName = animation.GetAnimName("Walk");
        //}

        isTakeAnim = false;
        isStan = false;
        inRagdoll = false;

        //MainContainer.Instance.EnemyManager.ReinitMove(this);


        ragdoll.Remove();
        gameObject.SetActive(false);
        HP = 100;
        //_enemyPool.EnemyList.Remove(this);

        yield return new WaitForSeconds(5f);
    }


    private IEnumerator RestoreAfterHit(Vector3 velocity)
    {
        isTakeAnim = false;
        isStan = true;
        anim.AnimationName = null;
        inRagdoll = true;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        gameObject.GetComponent<Collider2D>().enabled = false;
        ragdoll.Apply();
        ragdoll.RootRigidbody.velocity = velocity * 1.2f;
        var colliders = ragdoll.gameObject.GetComponentsInChildren<Collider2D>();
        //foreach (var item in colliders)
        //{
        //    var b = item.gameObject.AddComponent<EnemyBoneDamage>();
        //    b.myEnemy = this;
        //}

        GetComponent<Collider2D>().enabled = false;
        //if (BrickHand != null)
        //{
        //    var f = transform.Find("hand_R");
        //    if (f != null)
        //    {
        //        BrickHand.transform.SetParent(transform.Find("hand_R"));
        //    }
        //}

        Vector3 estimatedPos = ragdoll.EstimatedSkeletonPosition;
        yield return new WaitForSeconds(1);
        ragdoll.RootRigidbody.isKinematic = true;
        ragdoll.SetSkeletonPosition(new Vector3(ragdoll.RootRigidbody.position.x, estimatedPos.y,
            transform.position.z));

        yield return ragdoll.SmoothMix(0, 1f);
        ragdoll.Remove();
        //foreach (var item in colliders)
        //{
        //    Destroy(item.GetComponent<EnemyBoneDamage>());
        //}

        //var r = this.gameObject.AddComponent<Rigidbody2D>();
        //if (BrickHand != null)
        //{
        //    BrickHand.transform.SetParent(Hand);
        //    BrickHand.transform.localPosition = new Vector3(0, 0, 0);
        //    if (BrickHand.GetComponent<Canvas>() != null)
        //    {
        //        BrickHand.GetComponent<Canvas>().enabled = true;
        //    }
        //    BrickHand.GetComponent<Image>().enabled = true;
        //}

        //r.isKinematic = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        //if (BrickHand != null)
        //{
        //    anim.AnimationName = animation.GetAnimName("WalkWithBrick");
        //}
        //else
        {
            anim.AnimationName = animation.GetAnimName("Walk");
        }

        RestoredAfterHit?.Invoke();
        isTakeAnim = false;
        isStan = false;
        inRagdoll = false;
        //MainContainer.Instance.EnemyManager.ReinitMove(this);
    }

    public void Damage(float damage, Vector3 dir, /*AbstractWeapon weapon,*/ Vector3 point)
    {
        if (Time.time > lastDamage + 0.3f)
        {
            Damaged?.Invoke();
            //MainContainer.Instance.DamageManager.CreateTextDamage(point, damage);
            lastDamage = Time.time;
            HP -= damage;
            if (HP <= 0 && gameObject.activeInHierarchy)
            {
                //Dead?.Invoke();
                //if (BrickHand != null)
                //{
                //    DropBrick(weapon);
                //}

                gameObject.GetComponent<PolygonCollider2D>().enabled = false;

                StopAllCoroutines();
                try
                {
                    ragdoll.StopAllCoroutines();
                    ragdoll.Remove();
                }
                catch
                {
                }
                StartCoroutine(DeadAnim(dir));
                isDead = true;
                HP = 1000;
            }
            else
            {
                if (!inRagdoll && gameObject.activeInHierarchy)
                {
                    StartCoroutine(RestoreAfterHit(dir));
                }
            }
        }
    }

    public void Move()
    {
        isTakeAnim = false;
    }

    public void TakeBrick(Action act)
    {

        isTakeAnim = true;
        var sk = this.gameObject.GetComponent<SkeletonAnimation>();
        var track = sk.AnimationState.SetAnimation(0, animation.GetAnimName("Steal"), false);

        //track.Complete += (e) =>
        //{
        //    MainContainer.Instance.BackgroundManager.ChangeHP();
        //    act?.Invoke();
        //    isTakeAnim = false;
        //    sk.AnimationState.SetAnimation(0, animation.GetAnimName("WalkWithBrick"), true);
        //    ServiceLocator.Instance.GetService<TowerAudioController>().ShatchBlock();
        //};

    }

    //public void TakedBrick(Brick br, TakeBrickContext context)
    //{
    //    if (br.GetComponent<TowerHammer>() != null)
    //        _playerAnimation.LoseHammerAnimation();

    //    br.transform.localPosition = new Vector3(0, 0, 0);
    //    br.transform.SetParent(Hand);
    //    br.transform.localPosition = new Vector3(0, 0, 0);
    //    var c = br.gameObject.GetComponent<Canvas>();
    //    if (c == null)
    //    {
    //        c = br.gameObject.AddComponent<Canvas>();
    //    }

    //    c.enabled = true;
    //    c.overrideSorting = true;
    //    c.sortingOrder = 2;
    //    BrickHand = br;
    //    BrickHand.GetComponent<Collider2D>().enabled = false;
    //    br.transform.SetParent(Hand);
    //    if (context == TakeBrickContext.FromFloor)
    //    {
    //        MainContainer.Instance.EnemyManager.ReinitMove(this);
    //    }
    //}
}

public enum TakeBrickContext
{
    FromLine,
    FromFloor
}