using InputSystem;
using UnityEngine;

public class NuggetThrower : MonoBehaviour
{
    public float throwForce = 20f;

    public GameObject nuggetPrefab;

    public StarterAssetsInputs input;


    // Update is called once per frame
    private void Update()
    {
        if(!input.shoot) return;

        ThrowNugget();
        input.shoot = false;
    }

    private void ThrowNugget()
    {
        Transform camTransform = transform;
        GameObject nugget = Instantiate(nuggetPrefab, camTransform.position, camTransform.rotation);
        Rigidbody rb = nugget.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}