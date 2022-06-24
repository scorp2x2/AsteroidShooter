using UnityEngine;
using Zenject;

public class BulletGun: Bullet
{
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        CheckBoundsScreen();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        var asteroid = other.transform.parent.GetComponent<Asteroid>();

        if (asteroid != null && _pool != null)
        {
            asteroid.TakeDamage(damage);
            _pool.Despawn(this);
        }
    }

    public class Factory : PlaceholderFactory<float, float, BulletGun>
    {

    }
}
