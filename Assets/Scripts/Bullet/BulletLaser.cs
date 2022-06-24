using UnityEngine;
using Zenject;

public class BulletLaser: Bullet, IPoolable<float, float, Player, IMemoryPool>
{
    InputState _inputState;
    Player _player;

    [Inject]
    public void Construct(InputState inputState)
    {
        _inputState = inputState;
    }

    public void OnSpawned(float p1, float p2, Player p3, IMemoryPool p4)
    {

        _player = p3;
        base.OnSpawned(p1, p2, p4);
        transform.localPosition = _player.Position;

    }

    void Update()
    {
        if (!_inputState.ShootLaser)
            _pool.Despawn(this);
        transform.localPosition = _player.Position;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        var asteroid = other.transform.parent.GetComponent<Asteroid>();

        if (asteroid != null && _pool != null)
        {
            asteroid.TakeDamage(damage);
        }
    }

    public class Factory : PlaceholderFactory<float, float, Player, BulletLaser>
    {

    }
}