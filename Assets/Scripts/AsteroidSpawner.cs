using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float spawnRate = 10f;
    public int spawnAmount = 10;

    public Asteroid asteroidFab;
    public float spawnDistance = 5f;
    public float trajectory = 15f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, 10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Spawn() {
        for (int i = 0; i < this.spawnAmount; i++)
        { 
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance; // ???
            Vector3 spawnPoint = this.transform.position + spawnDirection; 

            float variance = Random.Range(-this.trajectory, this.trajectory);
            Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidFab, spawnPoint, rotation);
            asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);
            asteroid.InitiateAsteroid();
            asteroid.SetTrajectory(rotation * -spawnDirection);
        }
    }
}
