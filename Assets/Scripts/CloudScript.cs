using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    public ParticleSystem cloudParticle;
    public void Die()
    {
        Instantiate(cloudParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
