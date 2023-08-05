using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public float deadTime = 5f;
    public Rigidbody body;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if(health <= 0f) StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        body.constraints = RigidbodyConstraints.None;

        yield return new WaitForSeconds(deadTime);

        Destroy(gameObject);
    }
}