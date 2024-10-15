using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject subditoPrefab;
    public float spawnInterval = 5f;
    public float spawnRadius = 5f;
    public Transform player;

    private void Start()
    {
        StartCoroutine(SpawnSubditos());
    }

    IEnumerator SpawnSubditos()
    {
        while (true)
        {
            Vector3 spawnPosition = transform.position + (Random.insideUnitSphere * spawnRadius);
            spawnPosition.y = transform.position.y;

           
            GameObject subdito = Instantiate(subditoPrefab, spawnPosition, Quaternion.identity);

        
            SubditoController subditoController = subdito.GetComponent<SubditoController>();
            subditoController.SetPlayer(player);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
