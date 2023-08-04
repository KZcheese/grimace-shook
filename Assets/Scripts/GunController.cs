using Cinemachine;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public int ammo = 10;

    public Camera gunCam;
    public StarterAssetsInputs input;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private void Update()
    {
        if(input.shoot) Fire();
    }

    private void Fire()
    {
        input.shoot = false;
        muzzleFlash.Play();

        Transform camTransform = gunCam.transform;
        if(!Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, range)) return;

        // Debug.Log(hit.transform.name);

        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 2f);
        
        Target target = hit.transform.GetComponent<Target>();
        if(!target) return;

        target.TakeDamage(damage);
    }
}