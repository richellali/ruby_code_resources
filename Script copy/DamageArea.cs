using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Damage
/// </summary>

public class DamageArea : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        playercontroller pc = other.GetComponent<playercontroller>();

        if (pc != null)
        {
            pc.ChangeHealth(-1);
        }

    }
}
