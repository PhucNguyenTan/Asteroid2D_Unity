using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 500f;

    public float maxLifeTime = 10f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction) {
        _rigidbody.AddForce(direction * this.speed);
        Destroy(this.gameObject, this.maxLifeTime);
    }
}
