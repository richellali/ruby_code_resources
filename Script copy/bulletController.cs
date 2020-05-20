using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// control the movement and collision of bullets
/// </summary>

public class bulletController : MonoBehaviour
{
    Rigidbody2D rbody;

    public AudioClip hitClip;
    // Start is called before the first frame update
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// movement of bullet
    /// </summary>

    public void Move(Vector2 moveDirection, float moveForce)
    {
        rbody.AddForce(moveDirection * moveForce);
    }

    // collision detector
    void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController ec = other.gameObject.GetComponent<EnemyController>();
        if (ec != null)
        {
            ec.Fixed();
        }
        audioManager.instance.AudioPlay(hitClip); 
        Destroy(this.gameObject);
    }
}
