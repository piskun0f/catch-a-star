using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour
{
    [SerializeField] SpawnerScript spawner;

    public Rigidbody2D starBody;
    public Vector2 direction = new Vector2(1,1);

    bool isDead = false;
    float speed = 0.4f;


    public void Start()
    {
        StartCoroutine(Rutine());
    }

    public void Die()
    {
        print("diee");
        spawner.StarDeleted();
        Stop();
        isDead = true;
        Destroy(gameObject);
    }

    private bool isStarDead()
    {
        return isDead;
    }

    private void Move()
    {
        starBody.velocity = direction * speed;
    }

    private void Stop()
    {
        starBody.velocity = new Vector2(0, 0);
    }

    private IEnumerator Rutine()
    {
        while (true)
        {
            if (!isDead)
            {
                yield return new WaitForEndOfFrame();
                Move();
            }
            else
            {
                yield break;
            }
        }
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // chashed into cloud
        if (collision.collider.TryGetComponent(out CloudScript cloud))
        {
            cloud.Die();
            Die();
        }

        // Out of screen
        else if(collision.collider.TryGetComponent(out Border border))
        {
            Die();
        }
    }
}
