using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Asteroid : MonoBehaviour, IPoolable<float, float, float, IMemoryPool>
{
    RectTransform rectTransform;
    IMemoryPool _pool;
    SignalBus _signalBus;
    AsteroidDeadSignal asteroidDeadSignal;

    float speed;
    public float hp;
    float damage;

    [Inject]
    public void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public GameObject[] asteroidImages;

    public void SetRandomImage()
    {
        foreach (var item in asteroidImages)
            item.SetActive(false);

        int index = UnityEngine.Random.Range(0, asteroidImages.Length);
        asteroidImages[index].SetActive(true);
    }

    public void OnDespawned()
    {
        _pool = null;
    }

    public void OnSpawned(float p1, float p2, float p3, IMemoryPool p4)
    {
        speed = p1;
        hp = p2;
        asteroidDeadSignal = new AsteroidDeadSignal() { ScoreCount = p2 };
        damage = p3;
        _pool = p4;

     //   SetRandomImage();
    }

    void Update()
    {
        transform.localPosition += Vector3.left * speed * Time.deltaTime;

        float x = transform.localPosition.x;
        float y = transform.localPosition.y;

        if (x < -Screen.width / 2 || x > Screen.width / 2 || y < -Screen.height / 2 || y > Screen.height / 2)
        {
            _pool.Despawn(this);
            _signalBus.Fire<AsteroidDeadSignal>();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(name);
        var player = other.GetComponent<PlayerUI>();

        if (player != null)
        {
            player.TakeDamage(damage);
            _signalBus.Fire<AsteroidDeadSignal>();
            _pool.Despawn(this);
        }
    }

    public class Factory : PlaceholderFactory<float, float, float, Asteroid>
    {
       
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            _signalBus.Fire(asteroidDeadSignal);
            _pool.Despawn(this);
        }
    }
}
