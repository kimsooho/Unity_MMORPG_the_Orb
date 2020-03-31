using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HobScript : MonoBehaviour {
    private Transform mTr;
    private Transform pTr;
    private Transform sTr;
    private NavMeshAgent navmesh;
    private Player player;

    Animator animator;
    AudioSource audio;


    int hp;
    int exp;
    int str;
    float coolTime;
    bool roar;
    // Use this for initialization
    void Start()
    {
        hp = 150;
        exp = 30;
        str = 15;
        coolTime = 0;
        animator = GetComponent<Animator>();
        roar = false;
        mTr = gameObject.GetComponent<Transform>();
        pTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        sTr = gameObject.transform.parent.transform;
        navmesh = gameObject.GetComponent<NavMeshAgent>();
        player = pTr.gameObject.GetComponent<Player>();

        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameDirector.checkTogEffOn)
            audio.volume = GameDirector.eff * 0.01f;
        else
            audio.volume = 0;
        if (animator.GetBool("isDead") == false)
        {
            coolTime += Time.deltaTime;
            Move();
            Attack();
        }
    }
    private void Attack()
    {
        if (Vector3.Distance(mTr.position, pTr.position) <= 2f
            && coolTime >= 3)
        {
            coolTime = 0;
            animator.SetBool("isAttacking", true);

            Invoke("Delay", 0.5f);
        }
    }
    private void Move()
    {
        if (Vector3.Distance(mTr.position, sTr.position) <= 2f
            && Vector3.Distance(mTr.position, pTr.position) >= 10f)
        {
            navmesh.isStopped = true;
            navmesh.ResetPath();
            animator.SetBool("isRunning", false);
        }
        else if (Vector3.Distance(mTr.position, pTr.position) <= 2f)
        {
            navmesh.isStopped = true;
            navmesh.ResetPath();
            animator.SetBool("isRunning", false);
        }
        else
        {
            if (Vector3.Distance(mTr.position, pTr.position) <= 10f)
            {
                navmesh.destination = pTr.position;
                if (roar == false)
                {
                    audio = GameObject.Find("MonsterRoar").GetComponent<AudioSource>();
                    audio.Play();
                    roar = true;
                }
            }
            else
            {
                navmesh.destination = sTr.position;
                roar = false;
            }
            animator.SetBool("isRunning", true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "weapon"
            && !animator.GetBool("isDead"))
        {
            animator.SetBool("isDamaged", true);
            audio = GameObject.Find("MonsterPain").GetComponent<AudioSource>();
            audio.Play();
            hp -= player.Str;
            if (hp <= 0)
            {
                audio = GameObject.Find("MonsterDie").GetComponent<AudioSource>();
                audio.Play();
                navmesh.enabled = false;
                player.Exp += exp;
                Dead();
                Invoke("DestroyDelay", 3);
            }
            else
            {
                Invoke("Recovery", 0.01f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "blade"
            && !animator.GetBool("isDead"))
        {
            animator.SetBool("isDamaged", true);
            audio = GameObject.Find("MonsterPain").GetComponent<AudioSource>();
            audio.Play();
            hp -= player.Str * 5;
            if (hp <= 0)
            {
                audio = GameObject.Find("MonsterDie").GetComponent<AudioSource>();
                audio.Play();
                navmesh.enabled = false;
                player.Exp += exp;
                Dead();
                Invoke("DestroyDelay", 3);
            }
            else
            {
                Invoke("Recovery", 0.01f);
            }
        }
    }
    void Dead()
    {
        animator.SetBool("isDamaged", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", false);
        animator.SetBool("isDead", true);
    }
    void DestroyDelay()
    {
        MonsterManager.monsters.Remove(gameObject);
        Destroy(gameObject);
    }

    void Recovery()
    {
        animator.SetBool("isDamaged", false);
    }
    void Delay()
    {
        audio = GameObject.Find("MonsterAttack").GetComponent<AudioSource>();
        audio.Play();
        if (player.IsInvincible == false)
            player.Hp -= str;
        animator.SetBool("isAttacking", false);
    }
}
