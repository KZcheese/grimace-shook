using System.Collections;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public AudioSource audioSource;
    private bool _collected;
    public SpriteRenderer itemGraphic;

    private void OnTriggerEnter(Collider other)
    {
        if(!_collected && other.CompareTag("Player"))
        {
            _collected = true;
            StartCoroutine(Collect());
        }
    }

    private IEnumerator Collect()
    {
        _collected = true;
        itemGraphic.enabled = false;
        audioSource.Play();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}