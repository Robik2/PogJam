using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    [Header("Shooting variables")] 
    [SerializeField] private float fireRate;
    [SerializeField] private float shootRange;
    [SerializeField] private float bulletSpeed;
    
    [Header("Transforms etc.")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform gunToRotate;

    private float lastTimeShoot;

    private Vector3 mousePos;
    private Quaternion rotation;
    
    private void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = firePoint.position.z;
        
        Vector3 direction = (mousePos - firePoint.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        rotation = Quaternion.Euler(0, 0, angle - 90);

        WeaponLookAt();
        
        if (Input.GetButtonDown("Fire1") && Time.time - lastTimeShoot > fireRate) {
            Shoot();
        }
    }

    private void Shoot() {
        lastTimeShoot = Time.time;
        
        Projectile bullet = Instantiate(bulletPrefab, firePoint.position, rotation).GetComponent<Projectile>();
        bullet.Shoot(shootRange, bulletSpeed);
    }

    private void WeaponLookAt() {
        gunToRotate.rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + 180);
    }
}
