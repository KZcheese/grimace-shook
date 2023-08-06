using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public float deadTime = 1f;
    protected bool Dead;
    public Rigidbody body;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f) StartCoroutine(Die());
    }

    protected virtual IEnumerator Die()
    {
        Dead = true;
        body.constraints = RigidbodyConstraints.None;

        yield return new WaitForSeconds(deadTime);

        Destroy(gameObject);
    }
}