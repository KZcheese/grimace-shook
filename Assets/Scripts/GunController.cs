using System.Collections;
using InputSystem;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public int maxAmmo = 10;
    private int _currentAmmo;
    public float fireRate = 0.1f;
    public float reloadTime = 1f;
    private bool _isReloading;

    public Camera gunCam;

    public StarterAssetsInputs input;

    // public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public SpriteRenderer gunRenderer;
    private Sprite _idleSprite;
    public Sprite shootingSprite;
    public Sprite reloadingSprite;
    public Sprite emptySprite;


    private void Start()
    {
        _currentAmmo = maxAmmo;
        _idleSprite = gunRenderer.sprite;
    }

    private void Update()
    {
        if(input.shoot) StartCoroutine(Fire());
        if(input.reload && !_isReloading) StartCoroutine(Reload());
    }

    private IEnumerator Fire()
    {
        if(_currentAmmo < 1) yield break;

        input.shoot = false;

        // muzzleFlash.Play();
        gunRenderer.sprite = shootingSprite;
        _currentAmmo--;


        Transform camTransform = gunCam.transform;
        if(!Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, range))
            yield break;

        // Debug.Log(hit.transform.name);

        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 2f);

        Target target = hit.transform.GetComponent<Target>();
        if(target) target.TakeDamage(damage);

        yield return new WaitForSeconds(fireRate);

        gunRenderer.sprite = _currentAmmo < 1 ? emptySprite : _idleSprite;
    }

    private IEnumerator Reload()
    {
        gunRenderer.sprite = reloadingSprite;
        _isReloading = true;
        
        yield return new WaitForSeconds(reloadTime);
        
        gunRenderer.sprite = _idleSprite;
        _currentAmmo = maxAmmo;
        _isReloading = false;
        input.reload = false;
        input.shoot = false;
    }
}