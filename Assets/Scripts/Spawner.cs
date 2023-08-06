using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float interval = 5f;
    private float _timer;
    public bool spawnImmediately = true;

    private void Start()
    {
        if(spawnImmediately)
            SpawnObject();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= interval)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        Transform spawnTransform = transform;
        Instantiate(spawnObject, spawnTransform.position, spawnTransform.rotation);
    }
}