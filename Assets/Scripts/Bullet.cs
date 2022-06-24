using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public enum BulletType
{
    gun,
    laser
}

public class Bullet : MonoBehaviour, IPoolable<float, float, BulletType, IMemoryPool>
{
    BulletType bulletType;
    RectTransform rectTransform;
    IMemoryPool _pool;

    float speed;
    float damage;

    public void OnDespawned()
    {
        _pool = null;
    }

    public void OnSpawned(float p1, float p2, BulletType p3, IMemoryPool p4)
    {
        speed = p1;
        damage = p2;
        bulletType = p3;
        _pool = p4;
    }

    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

        float x = transform.localPosition.x;
        float y = transform.localPosition.y;

        if (x < -Screen.width / 2 || x > Screen.width / 2 || y < -Screen.height / 2 || y > Screen.height / 2)
        {
            _pool.Despawn(this);
        }
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(name);
        var asteroid = other.GetComponent<Asteroid>();
        Debug.Log(asteroid);
        if (asteroid != null)
        {
            asteroid.TakeDamage(damage);
            _pool.Despawn(this);
        }
    }

    public class Factory : PlaceholderFactory<float, float, BulletType, Bullet>
    {

    }
}
