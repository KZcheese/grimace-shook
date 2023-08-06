using System.Collections;
using UnityEngine;

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

        gunRenderer.sprite = shootingSprite;
        CurrentAmmo--;

        Transform gunTransform = transform;
        GameObject nugget = Instantiate(nuggetPrefab, gunTransform.position, gunTransform.rotation);
        Rigidbody rb = nugget.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);

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