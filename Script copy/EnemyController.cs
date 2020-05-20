using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy controller
/// </summary>

public class EnemyController : MonoBehaviour
{
    public float speed = 3; // movement

    public float changeDirectionTime = 2f; // time of direction change

    private float changeTimer;

    public bool isVertical; // Vertical direction

    private Vector2 moveDirection;

    public ParticleSystem brokenEffect;

    public AudioClip fixedClip;

    private bool isFixed;

    private Rigidbody2D rbody;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        moveDirection = isVertical ? Vector2.up : Vector2.right;

        changeTimer = changeDirectionTime;

        isFixed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFixed) return;

        changeTimer -= Time.deltaTime;

        if (changeTimer < 0)
        {
            moveDirection *= -1;
            changeTimer = changeDirectionTime;
        }

        Vector2 position = rbody.position;
        position.x += moveDirection.x * speed * Time.deltaTime;
        position.y += moveDirection.y * speed * Time.deltaTime;
        rbody.MovePosition(position);
        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);
    }
    // collision detector
   void OnCollisionEnter2D(Collision2D other)
    {
        playercontroller pc = other.gameObject.GetComponent<playercontroller>();
        if (pc != null)
        {
            pc.ChangeHealth(-1);
        }
    }

    /// <summary>
    /// Kill the enemy
    /// </summary>
    
    public void Fixed()
    {
        isFixed = true;

        if (brokenEffect.isPlaying == true)
        {
            brokenEffect.Stop();
        }
        audioManager.instance.AudioPlay(fixedClip);
        rbody.simulated = false;
        anim.SetTrigger("fix");
    }
}
