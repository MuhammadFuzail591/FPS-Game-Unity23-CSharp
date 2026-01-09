using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public float bulletVelocity = 30;

    public float bulletPrefabLifetime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
      {
         
         FireWeapon();
      }


    }

    private void FireWeapon()
   {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, quaternion.identity);

        bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward.normalized * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));
   }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }


}
