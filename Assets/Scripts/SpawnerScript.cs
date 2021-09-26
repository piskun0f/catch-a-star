using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject starTemplate;
    [SerializeField] float randomRange = 1f;
    [SerializeField] BorderChecker borderChecker;

    int starsCount = 0;

    Vector2 max;
    float offset = 0.8f;

    private void Start()
    {
        Vector2 borderMax = borderChecker.GetBorderMax();
        max = new Vector2(borderMax.x - offset, borderMax.y - offset);
        StartCoroutine(StarsCreation());
    }

    private IEnumerator StarsCreation()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (starsCount < 2)
            {
                SpawnStar();
            }
        }
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

    public void StarDeleted()
    {
        print(starsCount);
        starsCount--;
    }

    public GameObject SpawnStar()
    {
        starsCount++;
        print(starsCount);
        Vector2 position = new Vector2(0, 0);
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
        }

        GameObject star = Instantiate(starTemplate, position, Quaternion.identity);

        star.TryGetComponent(out StarScript starScipt);
        if (starScipt)
            starScipt.direction = getRandomVector2() - position;

        return star;
    }
}
