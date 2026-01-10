using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag("Target"))
      {
         print("hit " + collision.gameObject.name + "!");

         CreateBulletImpactEffect(collision);

         Destroy(gameObject);
      }

      if (collision.gameObject.CompareTag("Wall"))
      {
         print("hit a wall!");

         CreateBulletImpactEffect(collision);
         
         Destroy(gameObject);
      }
    }

    void CreateBulletImpactEffect(Collision collision)
    {
        // Placeholder for bullet impact effect logic
    }
}
