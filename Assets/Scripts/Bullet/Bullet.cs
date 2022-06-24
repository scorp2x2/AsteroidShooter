using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bullet : MonoBehaviour, IPoolable<float, float, IMemoryPool>
{
    protected IMemoryPool _pool;

    protected float speed;
    protected float damage;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        signalBus.Subscribe<PlayerDiedSignal>(OnPlayerDied);
    }

    private void OnPlayerDied()
    {
        if (_pool != null)
            _pool.Despawn(this);
    }

    public void OnDespawned()
    {
        _pool = null;
    }

    public void OnSpawned(float p1, float p2, IMemoryPool p3)
    {
        speed = p1;
        damage = p2;
        _pool = p3;
    }

    protected void CheckBoundsScreen()
    {
        float x = transform.localPosition.x;
        float y = transform.localPosition.y;

        if (x < -Screen.width / 2 || x > Screen.width / 2 || y < -Screen.height / 2 || y > Screen.height / 2)
        {
            _pool.Despawn(this);
        }
    }

   
}
