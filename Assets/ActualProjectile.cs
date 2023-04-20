using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActualProjectile : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("droid"))
        {
            Destroy(collision.gameObject); // destroy the droid
            Destroy(gameObject); // destroy the projectile
        }
    }
}
