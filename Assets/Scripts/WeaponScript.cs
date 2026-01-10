using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Camera PlayerCamera;

    public bool isShooting, readyToShoot;

    bool allowReset = true;
    public float shootingDelay = 2f;

    public int bulletsPerBurst = 3;
    public int burstBulletsLeft;
    
    public float spreadIntensity;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    public float bulletVelocity = 30;

    public float bulletPrefabLifetime = 3f;
 
    public enum ShootingMode
    {
        Single,
        Burst,
        Auto
    }

    public ShootingMode currentShootingMode;

    private void Awake()
    {
        readyToShoot = true;
        burstBulletsLeft = bulletsPerBurst;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(currentShootingMode == ShootingMode.Auto)
      {
         isShooting = Input.GetKey(KeyCode.Mouse0);
      }
      else if(currentShootingMode == ShootingMode.Single || currentShootingMode == ShootingMode.Burst)
      {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
      }
      if (readyToShoot && isShooting)
      {
         burstBulletsLeft = bulletsPerBurst;
            FireWeapon();
      }


    }

    private void FireWeapon()
   {
        readyToShoot = false;

        Vector3 shootingDirection = CalculateDirectionAndSpread().normalized;
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, quaternion.identity);


        bullet.transform.forward = shootingDirection;
        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletVelocity, ForceMode.Impulse);

        StartCoroutine(DestroyBulletAfterTime(bullet, bulletPrefabLifetime));


      if (allowReset)
      {
         Invoke("ResetShot", shootingDelay);
         allowReset = false;
      }
      if(currentShootingMode == ShootingMode.Burst && burstBulletsLeft > 1)
      {
         burstBulletsLeft--;
         Invoke("FireWeapon", shootingDelay);
      }

   }

    private void ResetShot()
    {
        readyToShoot = true;
        allowReset = true;
    }

   public Vector3 CalculateDirectionAndSpread()
   {
        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
      {
         targetPoint = hit.point;
      }
      else
      {
         targetPoint = ray.GetPoint(100);
      }

        Vector3 direction = targetPoint - bulletSpawn.position;
        float x = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);
        float y = UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction + new Vector3(x, y, 0);
   }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }


}
