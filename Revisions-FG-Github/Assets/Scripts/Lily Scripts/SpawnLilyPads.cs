using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SpawnLilyPads : MonoBehaviour


{
    [Header("Lilypad Objects")]
    [SerializeField] GameObject[] waterRings;
    [SerializeField] GameObject[] lillies;

    [Header("Other Objects")]
    [SerializeField] GameObject portal;
    [SerializeField] GameObject star;

    [Header("Level Indicator")]
    [SerializeField] int level;

    private lily Lily;
    private int[] numLillies = { 20, 14, 7 };
    private Vector3[] lilyScales = {
            // local scale of lily pads to water rings, different for each ring
            new Vector3((float)0.3328913, (float)0.328198, 1), // first ring
            new Vector3((float)0.3505113, (float)0.3406977, 1), // second ring 
            new Vector3((float)0.4058551, (float)0.394492, 1)}; // third ring

    // ints represent the probability of spawning a lily at any given point on the water rings
    // the difficulty of each level requires a higher or lower probability for a lily to spawn
    // the ints are corresponding to the probability for each level (1,2,3,4,5)
    private float[] lilyProbabilityLevel = { 10, 8, 9, 10, 9 }; 


    void Start()
    {
        SpawnLevel(level);
    }

    private void SpawnLevel(int level)
    {
        float[] circleRadii = { lillies[0].transform.position.x, lillies[1].transform.position.x, lillies[2].transform.position.x };
        List<GameObject> spawnedLillies = new List<GameObject>();


        for (int i = 0; i < numLillies.Length; i++)
        {

            for (int j = 1; j < numLillies[i]; j++)
            {

                float xPosLily = circleRadii[i] * Mathf.Cos(2 * j * Mathf.PI / numLillies[i]);
                float yPosLily = circleRadii[i] * Mathf.Sin(2 * j * Mathf.PI / numLillies[i]);

                GameObject lily = null;


                float lilySpawnProbability = Random.Range(0, 10);
                if (lilySpawnProbability < lilyProbabilityLevel[level - 1])
                {
                    lily = lillies[i];
                }

                if (lily != null)
                {

                    lily = Instantiate(lillies[i], new Vector3(xPosLily, yPosLily, 1), Quaternion.identity);


                    lily.transform.parent = waterRings[i].transform;
                    lily.transform.localScale = lilyScales[i];

                    if (level > 2)
                    {
                        Lily = lily.GetComponent<lily>();

                        bool isEvil = Random.Range(0, 10) < 5;

                        Lily.SetIsEvil(isEvil);
                    }

                    spawnedLillies.Add(lily);
                }
            }
        }

        if (level == 4)
        {
            StartCoroutine(RespawnPortal(spawnedLillies));
        }

        if (level == 5)
        {
            for (int k = 0; k < 2; k++)
            {

                GameObject starLily1 = spawnedLillies[(int)Mathf.Floor(Random.Range(0, spawnedLillies.Count - 1))];
                GameObject star1 = Instantiate(star, starLily1.transform.position, Quaternion.identity);
                star1.transform.parent = starLily1.transform;
            }
        }
    }


    private IEnumerator RespawnPortal(List<GameObject> lillies)
    {
        GameObject portalLily = lillies[25];
        portalLily.SetActive(false);

        GameObject newPortal = Instantiate(portal, portalLily.transform.position, Quaternion.identity);
        newPortal.transform.parent = portalLily.transform.parent;

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(6, 10));


            portalLily.SetActive(true);
            Destroy(newPortal);


            portalLily = lillies[(int)Mathf.Floor(Random.Range(0, lillies.Count - 1))];
            portalLily.SetActive(false);


            newPortal = Instantiate(portal, portalLily.transform.position, Quaternion.identity);
            newPortal.transform.parent = portalLily.transform.parent;
        }
    }
}





