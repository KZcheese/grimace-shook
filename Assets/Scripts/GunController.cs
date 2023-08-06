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

    private void Start()
    {
        CurrentAmmo = maxAmmo;
        _idleSprite = gunRenderer.sprite;
    }

    private void Update()
    {
        if(input.shoot && !_isReloading) StartCoroutine(Fire());
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

        gunRenderer.sprite = shootingSprite;
        CurrentAmmo--;

        GameObject nugget = Instantiate(projectilePrefab, gunTip.position, transform.rotation);
        Rigidbody rb = nugget.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * shotForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(fireRate);

        SetIdleSprite();
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