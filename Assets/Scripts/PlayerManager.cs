using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float health = 100f;
    public float knockBackForce = 10f;

    public float knockBackRadius = 10f;
    public int numItems = 5;
    private int _collectedItems;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            MobController mob = other.gameObject.GetComponent<MobController>();
            if(mob) TakeDamage(mob.damage);
        }
        else if(other.gameObject.CompareTag("Collectable"))
        {
            if(++_collectedItems >= numItems)
            {
                Debug.Log("WIN!");
            }
        }
    }

    private void TakeDamage(float damage)
    {
        health -= damage;

        // characterController.attachedRigidbody.AddExplosionForce(knockBackForce, position, knockBackRadius);

        if(health < 0) Die();
    }

    private static void Die()
    {
        Debug.Log("Game Over!");
    }
}