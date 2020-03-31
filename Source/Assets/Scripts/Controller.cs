using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public Player player;

    Animator animator;
    Rigidbody rigidbody;
    Vector3 movement;
    AudioSource audio;

    public float speed;
    public float rSpeed;
    public float currentSpeed;
    
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        speed = 2.5f;
        rSpeed = speed * 2;
        rigidbody = GetComponent<Rigidbody>();
        currentSpeed = speed;
    }

    // Update is called once per frame
    void Update () {
        if (GameDirector.checkTogEffOn)
            audio.volume = GameDirector.eff * 0.01f;
        else
            audio.volume = 0;
    }
    private void FixedUpdate()
    {
        Attack();
        Move();
    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.A) == true)
        {
            audio = GameObject.Find("Slash").GetComponent<AudioSource>();
            audio.Play();
            animator.SetBool("inputAttack", true);
            InvokeRepeating("Slash", 0.7f, 0.7f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("inputRun", false);
            animator.SetBool("inputWalk", false);
        }
        else if (Input.GetKeyUp(KeyCode.A) == true
                || Input.GetKey(KeyCode.A)==false)
        {
            animator.SetBool("inputAttack", false);
            CancelInvoke("Slash");
        }
    }
    void Move()
    {
        speed = 2.5f + (float)player.Agi * 0.1f;
        rSpeed = speed * 2;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        movement.Set(h, 0, v);
        if(Input.GetKey(KeyCode.LeftShift))
            Run(movement,h,v);
        else if(Input.GetKey(KeyCode.LeftShift)==false)
            Walk(movement,h,v);
    }
    void Turn(Vector3 movement)
    {
        Quaternion quaternion = Quaternion.LookRotation(movement);
        rigidbody.rotation = Quaternion.Slerp(rigidbody.rotation, quaternion, rSpeed * Time.deltaTime);
    }
    void Run(Vector3 movement,float h,float v)
    {
        if (((Input.GetKey(KeyCode.UpArrow)
            || Input.GetKey(KeyCode.DownArrow)
            || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.RightArrow)))
            && animator.GetBool("inputAttack") == false)
        {
            currentSpeed = speed * 2;
            animator.SetBool("inputRun", true);
            animator.SetBool("inputWalk", false);
            movement = movement.normalized * currentSpeed * Time.deltaTime;
            Turn(movement);
            rigidbody.MovePosition(transform.position + movement);
        }

        else if (Input.GetKeyUp(KeyCode.UpArrow)
            || Input.GetKeyUp(KeyCode.DownArrow)
            || Input.GetKeyUp(KeyCode.LeftArrow)
            || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool("inputRun", false);
        }
    }
    void Walk(Vector3 movement, float h, float v)
    {
        if (((Input.GetKey(KeyCode.UpArrow)
            || Input.GetKey(KeyCode.DownArrow)
            || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.RightArrow)))
            && animator.GetBool("inputAttack") == false)
        {
            currentSpeed = speed;
            animator.SetBool("inputWalk", true);
            animator.SetBool("inputRun", false);
            movement = movement.normalized * currentSpeed * Time.deltaTime;
            Turn(movement);
            rigidbody.MovePosition(transform.position + movement);
        }

        else if (Input.GetKeyUp(KeyCode.UpArrow)
            || Input.GetKeyUp(KeyCode.DownArrow)
            || Input.GetKeyUp(KeyCode.LeftArrow)
            || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animator.SetBool("inputWalk", false);
            animator.SetBool("inputRun",false);
        }
    }
    public float GetSpeed()
    {
        return currentSpeed;
    }
    void Slash()
    {
        audio = GameObject.Find("Slash").GetComponent<AudioSource>();
        audio.Play();
    }
}
