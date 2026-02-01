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
    public int magazineSize = 13;
    public float reloadTime = 1.5f;

    [Header("Card Sprites")]
    public Sprite[] cardSprites = new Sprite[13]; // Ace, 2, 3, 4, 5, 6, 7, 8, 9, 10, Jack, Queen, King

    private int currentAmmo;
    private bool isReloading;
    private float nextFireTime;
    private int currentCardIndex = 0; // Tracks which card to shoot next

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

        // Set the card sprite on the bullet
        SpriteRenderer br = bullet.GetComponent<SpriteRenderer>();
        if (br != null)
        {
            if (cardSprites.Length > 0 && cardSprites[currentCardIndex] != null)
            {
                br.sprite = cardSprites[currentCardIndex];
            }
            br.flipX = spriteRenderer.flipX;
        }

        // Move to next card (cycle through 0-12)
        currentCardIndex = (currentCardIndex + 1) % 13;

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