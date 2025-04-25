using UnityEngine;
using System.Collections;

public class MonsterBehavior : MonoBehaviour
{
    public Animator monsterAnimator;               // Animator for the monster 
    public TriggerMathHandler mathHandlerScript;   // Reference to your TriggerMathHandler
    public float fightDuration = 2f;               // Duration of fight animation

    public bool started;
    public GameObject winPanel;
    public GameObject losePanel;

    private void Start()
    {
        started = false;
        winPanel.SetActive(false); // Hide the win panel at the start
    }

    private void OnTriggerEnter(Collider other)
    {
       StartCoroutine(HandleFight());
    }

    IEnumerator HandleFight()
    {
        started = true;

        yield return new WaitForSecondsRealtime(fightDuration * 2);

        // If enough spawned objects, remove 4 and kill monster
        if (mathHandlerScript != null && mathHandlerScript.spawnedObjects.Count > 5)
        {
            for (int i = 0; i < 4; i++)
            {
                int lastIndex = mathHandlerScript.spawnedObjects.Count - 1;
                GameObject obj = mathHandlerScript.spawnedObjects[lastIndex];
                mathHandlerScript.spawnedObjects.RemoveAt(lastIndex);
                Destroy(obj);
            }

            monsterAnimator.SetTrigger("Dead");

            yield return new WaitForSecondsRealtime(fightDuration * 2);
            winPanel.SetActive(true); // Show the win panel
        }
        else
        {
            mathHandlerScript.RemoveObjects(mathHandlerScript.spawnedObjects.Count);

            monsterAnimator.SetTrigger("Win");
            yield return new WaitForSecondsRealtime(fightDuration * 2);
            losePanel.SetActive(true); // Show the lose panel
        }
    }
}
