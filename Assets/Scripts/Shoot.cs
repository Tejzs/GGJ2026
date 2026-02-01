using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public SpriteRenderer spriteRenderer;
    public float fireRate = 0.25f;

    [Header("Ammo")]
    public int magazineSize = 6;
    public float reloadTime = 1.5f;

    private int currentAmmo;
    private bool isReloading;
    private float nextFireTime;

    void Start()
    {
        currentAmmo = magazineSize;
    }

    void Update()
    {
        if (isReloading)
            return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                StartCoroutine(Reload());
            }
        }
    }

    void Shoot()
    {
        currentAmmo--;

        Vector2 direction = spriteRenderer.flipX ? Vector2.left : Vector2.right;

        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.identity
        );

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetDirection(direction);

        // Optional bullet flip
        SpriteRenderer br = bullet.GetComponent<SpriteRenderer>();
        if (br != null)
            br.flipX = spriteRenderer.flipX;

        // Optional: trigger shoot animation here
        // animator.SetTrigger("shoot");
    }

    IEnumerator Reload()
    {
        if (isReloading)
            yield break;

        isReloading = true;
        // Optional: animator.SetTrigger("reload");

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = magazineSize;
        isReloading = false;
    }
}