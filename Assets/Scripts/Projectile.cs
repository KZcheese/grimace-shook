using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float delay = 3;
    public float radius = 5f;
    public float force = 50f;
    public float damage = 50;
    public bool explodeOnImpact = true;

    private float _countdown;

    private bool _exploded;

    public SpriteRenderer projectileRenderer;
    public Sprite explosionSprite;
    public Rigidbody projectileBody;

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

    private void OnCollisionEnter()
    {
        if(explodeOnImpact) _countdown = 0;
    }

    private IEnumerator Explode()
    {
        projectileRenderer.sprite = explosionSprite;
        _exploded = true;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Target target = nearbyObject.transform.GetComponent<Target>();
            if(target) target.TakeDamage(damage);

            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb) rb.AddExplosionForce(force, transform.position, radius);
        }

        // Debug.Log("BOOM!");
        projectileBody.constraints = RigidbodyConstraints.FreezeAll;

        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}