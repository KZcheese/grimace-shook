using Cinemachine;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera gunCam;
    public StarterAssetsInputs input;

    private void Update()
    {
        if(input.shoot) Fire();
    }

    private void Fire()
    {
        input.shoot = false;

        Transform camTransform = gunCam.transform;
        if(!Physics.Raycast(camTransform.position, camTransform.forward, out RaycastHit hit, range)) return;

        Debug.Log(hit.transform.name);

        Target target = hit.transform.GetComponent<Target>();
        if(!target) return;

        target.TakeDamage(damage);


    }
}