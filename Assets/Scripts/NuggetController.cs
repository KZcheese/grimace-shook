using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class NuggetController : GunController
{
    public Sprite fourShots;
    public Sprite threeShots;
    public Sprite twoShots;
    public Sprite oneShots;

    public float throwForce = 40f;
    public GameObject nuggetPrefab;
    
    protected override IEnumerator Fire()
    {
        if(CurrentAmmo < 1) yield break;

        input.shoot = false;

        // muzzleFlash.Play();
        gunRenderer.sprite = shootingSprite;
        CurrentAmmo--;


        Transform gunTransform = transform;
        GameObject nugget = Instantiate(nuggetPrefab, gunTransform.position, gunTransform.rotation);
        Rigidbody rb = nugget.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        
        // Transform camTransform = gunCam.transform;
        // if(!Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, range))
        //     yield break;
        //
        // // Debug.Log(hit.transform.name);
        //
        // GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        // Destroy(impact, 2f);
        //
        // Target target = hit.transform.GetComponent<Target>();
        // if(target) target.TakeDamage(damage);

        yield return new WaitForSeconds(fireRate);

        SetIdleSprite();
    }


    protected override void SetIdleSprite()
    {
        gunRenderer.sprite = CurrentAmmo switch
        {
            4 => fourShots,
            3 => threeShots,
            2 => twoShots,
            1 => oneShots,
            0 => emptySprite,
            _ => gunRenderer.sprite
        };
    }
}