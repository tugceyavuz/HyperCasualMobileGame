using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TriggerMathHandler : MonoBehaviour
{
    public GameObject Panel;
    public GameObject objectToSpawn;       // Prefab to spawn
    public Transform spawnPoint;           // Where to spawn objects
    public List<GameObject> spawnedObjects = new List<GameObject>();

    public AudioSource correct;
    public AudioSource wrong;
    public MonsterBehavior monsterBehavior;


    private void Start()
    {
        Time.timeScale = 1;
        Panel.SetActive(false); // Hide the panel at the start
        spawnedObjects.Add(objectToSpawn); // Add the initial object to the list
    }

    private void Update()
    {
        if(spawnedObjects.Count <= 0 && !monsterBehavior.started)
        {
            Panel.SetActive(true); // Show the panel when no objects are left
            Time.timeScale = 0; // Pause the game
        }
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
    }

    public void StartAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if (spawnedObjects.Count > 0)
            {
                int lastIndex = spawnedObjects.Count - 1;
                int last2Index = spawnedObjects.Count - 2;
                GameObject obj = spawnedObjects[lastIndex];
                GameObject obj2 = spawnedObjects[last2Index];
                spawnedObjects.RemoveAt(lastIndex);
                spawnedObjects.RemoveAt(last2Index);
                Destroy(obj);
                Destroy(obj2);
                Debug.Log("Obstacle hit! Removed one spawned object.");
            }
            return; // Don't continue to math logic
        }

        TargetArea target = other.GetComponent<TargetArea>();
        if (target != null)
        {
            int value = target.value;

            switch (target.operation)
            {
                case TargetArea.Operation.Add:
                    SpawnObjects(value);
                    break;

                case TargetArea.Operation.Subtract:
                    RemoveObjects(value);
                    break;

                case TargetArea.Operation.Multiply:
                    int multiplyAmount = Mathf.Max(spawnedObjects.Count * (value - 1), 0);
                    SpawnObjects(multiplyAmount);
                    break;

                case TargetArea.Operation.Divide:
                    if (value > 0)
                    {
                        int divideAmount = spawnedObjects.Count - Mathf.RoundToInt((float)spawnedObjects.Count / value);
                        RemoveObjects(divideAmount);
                    }
                    break;
            }

            if (target.good)
            {
                correct.Play();
            }
            else
            {
                wrong.Play();
            }

            Debug.Log("Total Objects: " + spawnedObjects.Count);
        }
    }

    void SpawnObjects(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
            GameObject obj = Instantiate(objectToSpawn, spawnPoint.position + offset, Quaternion.identity);
            spawnedObjects.Add(obj);
        }
    }

    public void RemoveObjects(int amount)
    {
        int count = Mathf.Min(amount, spawnedObjects.Count);

        for (int i = 0; i < count; i++)
        {
            int lastIndex = spawnedObjects.Count - 1;
            GameObject obj = spawnedObjects[lastIndex];
            spawnedObjects.RemoveAt(lastIndex);
            Destroy(obj);
        }

    }
}
