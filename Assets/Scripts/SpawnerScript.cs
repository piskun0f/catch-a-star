using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject starTemplate;
    [SerializeField] float randomRange = 1f;
    [SerializeField] BorderChecker borderChecker;

    Vector2 max;
    float offset = 0.7f;

    private void Start()
    {        
        Vector2 borderMax = borderChecker.GetBorderMax();
        max = new Vector2(borderMax.x - offset, borderMax.y - offset);
        StartCoroutine(InitSpawn());
    }

    private IEnumerator InitSpawn()
    {
        yield return new WaitForSeconds(1);
        SpawnStar();
    }

    private int GetRandomSign()
    {
        if (Random.Range(0, 2) == 0)
            return 1;
        return -1;
    }

    private Vector2 getRandomVector2()
    {
        return new Vector2(Random.Range(-randomRange, randomRange), Random.Range(-randomRange, randomRange));
    }

    public GameObject SpawnStar()
    {
        Vector2 position = Vector2.zero;
        int sign = GetRandomSign();

        if (Random.Range(0, 2) == 0)
        {
            position.x = sign * max.x;
            position.y = Random.Range(-max.y, max.y);
        }
        else
        {
            position.x = Random.Range(-max.x, max.x);
            position.y = sign * max.y;
            print(max.x);
        }        

        GameObject star = Instantiate(starTemplate, position, Quaternion.identity);

        star.TryGetComponent(out StarScript starScipt);
        if (starScipt)
            starScipt.direction = getRandomVector2() - position;

        return star;
    }
}
