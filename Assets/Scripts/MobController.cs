using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MobController : Target
{
    public float damage = 5f;

    public AudioSource audioSource;
    public AudioClip deathSound;
    public AudioClip attackSound;

    public NavMeshAgent agent;
    public Transform player;

    public void Update()
    {
        if(!Dead)
            ChasePlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    protected override IEnumerator Die()
    {
        agent.isStopped = true;
        audioSource.PlayOneShot(deathSound);
        return base.Die();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player")) return;

        audioSource.PlayOneShot(attackSound);

        PlayerManager playerManager = other.gameObject.GetComponent<PlayerManager>();
        if(playerManager)
            body.AddExplosionForce(playerManager.knockBackForce, playerManager.transform.position, playerManager.knockBackRadius);
    }
}