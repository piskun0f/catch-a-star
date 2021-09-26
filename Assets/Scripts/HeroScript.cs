using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroScript : MonoBehaviour
{
    [SerializeField] GameCounterScript counter;
    [SerializeField] Rigidbody2D heroBody;

    // is hero sticked on cloud
    [SerializeField]  bool isSticked = true;

    // movement
    Vector2 direction;
    float speed = 1f;
    float rotationSpeed = 100f;

    private void Die()
    {
        Stop();
        Destroy(gameObject);
    }

    private void Move()
    {
        // Move
        heroBody.velocity = direction * speed;

        // Rotate
        Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, -direction);
        heroBody.transform.rotation = Quaternion.RotateTowards(heroBody.transform.rotation, toRotate, rotationSpeed * Time.deltaTime);
    }


    private void Stop()
    {
        heroBody.velocity = Vector2.zero;
        heroBody.Sleep();
    }   

    private IEnumerator Moving()
    {
        while (true)
        {
            if (!isSticked)
            {
                yield return new WaitForEndOfFrame();
                Move();
            }
            else if (isSticked)
            {
                Stop();
                yield break;
            }
        }
    }

    private void OnMouseUp()
    {
        if (isSticked)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float x = -(mousePos - heroBody.position).x;
            float y = -(mousePos - heroBody.position).y;
            direction = new Vector2(x, y);
            
            isSticked = false;
            StartCoroutine(Moving());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Out of screen
        if (collision.collider.TryGetComponent(out Border border))
        {
            Die();
        }
        // chashed into cloud
        else if (collision.collider.TryGetComponent(out CloudScript cloud))
        {
            isSticked = true;
        }
        
        // caught a star
        if (collision.collider.TryGetComponent(out StarScript starScript))
        {
            Stop();
            Move();
            starScript.Die();
            Destroy(collision.collider.gameObject);
            counter++;
        }
    }
}
