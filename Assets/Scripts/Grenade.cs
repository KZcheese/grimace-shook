using System.Collections;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3;
    public float radius = 5f;
    public float force = 700f;
    public float damage = 50;

    private float _countdown;

    private bool _exploded;

    public SpriteRenderer nuggetRenderer;
    public Sprite explosionSprite;
    public Rigidbody nuggetBody;

    // Start is called before the first frame update
    private void Start()
    {
        _countdown = delay;
    }

    // Update is called once per frame
    private void Update()
    {
        _countdown -= Time.deltaTime;
        if(_countdown <= 0f && !_exploded)
        {
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode()
    {
        nuggetRenderer.sprite = explosionSprite;
        _exploded = true;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb) rb.AddExplosionForce(force, transform.position, radius);

            Target target = nearbyObject.transform.GetComponent<Target>();
            if(target) target.TakeDamage(damage);
        }

        // Debug.Log("BOOM!");
        nuggetBody.constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}