using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Strawberries collides with the player
/// </summary>
public class Collectable : MonoBehaviour
{
    public ParticleSystem collectEffect;

    public AudioClip collectClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// detect collison
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D other) {
        playercontroller pc = other.GetComponent<playercontroller>();

        if (pc != null)
        {
            if (pc.MyCurrentHealth < pc.MyMaxHealth)
            {
                pc.ChangeHealth(1);
                Instantiate(collectEffect, transform.position,
                    Quaternion.identity);
                audioManager.instance.AudioPlay(collectClip);
                Destroy(this.gameObject);
            }
        }
      
        
    }
}
