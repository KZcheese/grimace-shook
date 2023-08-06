using System.Collections;
using InputSystem;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public int maxAmmo = 10;
    protected int CurrentAmmo;
    public float fireRate = 0.1f;
    public float reloadTime = 1f;
    private bool _isReloading;
    private bool _isFiring;

    public GameObject projectilePrefab;
    public float shotForce = 40f;

    public StarterAssetsInputs input;

    // public ParticleSystem muzzleFlash;
    public SpriteRenderer gunRenderer;
    private Sprite _idleSprite;
    public Sprite shootingSprite;
    public Sprite reloadingSprite;
    public Sprite emptySprite;
    public Transform gunTip;
    public AudioSource audioSource;
    public AudioClip emptySound;
    public AudioClip reloadSound;
    public Sprite cursor;

    private void Start()
    {
        CurrentAmmo = maxAmmo;
        _idleSprite = gunRenderer.sprite;
        ResetWeapon();
    }

    private void OnEnable()
    {
        ResetWeapon();
    }

    private void ResetWeapon()
    {
        input.shoot = false;
        input.reload = false;
        _isFiring = false;
        _isReloading = false;
    }

    private void Update()
    {
        if(input.shoot && !_isReloading && !_isFiring) StartCoroutine(Fire());
        if(input.reload && !_isReloading) StartCoroutine(Reload());
        input.shoot = false;
        input.reload = false;
    }

    private IEnumerator Fire()
    {
        if(CurrentAmmo < 1)
        {
            audioSource.PlayOneShot(emptySound);
            yield break;
        }

        _isFiring = true;

        gunRenderer.sprite = shootingSprite;
        CurrentAmmo--;

        GameObject projectile = Instantiate(projectilePrefab, gunTip.position, gunTip.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shotForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(fireRate);

        SetIdleSprite();
        _isFiring = false;
    }

    protected virtual void SetIdleSprite()
    {
        gunRenderer.sprite = CurrentAmmo < 1 ? emptySprite : _idleSprite;
    }

    private IEnumerator Reload()
    {
        audioSource.PlayOneShot(reloadSound);
        gunRenderer.sprite = reloadingSprite;
        _isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        gunRenderer.sprite = _idleSprite;
        CurrentAmmo = maxAmmo;
        _isReloading = false;
    }
}