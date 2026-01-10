using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    private void OnCollisionEnter(Collision objectWeHit)
    {
      if (objectWeHit.gameObject.CompareTag("Target"))
      {
         print("hit " + objectWeHit.gameObject.name + "!");

         CreateBulletImpactEffect(objectWeHit);

         Destroy(gameObject);
      }

      if (objectWeHit.gameObject.CompareTag("Wall"))
      {
         print("hit a wall!");

         CreateBulletImpactEffect(objectWeHit);
         
         Destroy(gameObject);
      }

      
      if (objectWeHit.gameObject.CompareTag("Bottle"))
      {
         print("hit a coca cola bottle!");

         objectWeHit.gameObject.GetComponent<BottleScript>().Shatter();

      }

    }

    void CreateBulletImpactEffect(Collision objectWeHit)
   {
      ContactPoint contact = objectWeHit.contacts[0];

      GameObject hole = Instantiate(GlobalReferences.Instance.bulletImpactEffectPrefab, contact.point, Quaternion.LookRotation(contact.normal));

      hole.transform.SetParent(objectWeHit.gameObject.transform);
   }
}
