using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBag : MonoBehaviour
{
    public int bulletCount;

    public ParticleSystem collectEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        playercontroller pc = other.GetComponent<playercontroller>();

        if (pc != null)
        {
            if (pc.MyCurBulletCount < pc.MyMaxBulletCount)
            {
                pc.ChangeBulletCount(bulletCount);
                Instantiate(collectEffect, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
