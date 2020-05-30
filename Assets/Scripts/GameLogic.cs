using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

    public string currentScene;
    public List<GameObject> activeEnemies;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
        PlayerHealth.playerDied += reloadCurrentLevel;

        activeEnemies = GameObject.FindGameObjectsWithTag("EnemyTank").ToList();

        HealthController.EnemyDied += RemoveEnemyFromList;

    }

    private void OnDestroy()
    {
        PlayerHealth.playerDied -= reloadCurrentLevel;
        HealthController.EnemyDied -= RemoveEnemyFromList;
    }

    // Update is called once per frame
    void Update()
    {

    }


    void reloadCurrentLevel()
    {
        StartCoroutine(restartLevel());
    }

    IEnumerator restartLevel()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(currentScene);
    }

    void RemoveEnemyFromList(GameObject objectToRemove)
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i] == objectToRemove)
            {
                activeEnemies.Remove(objectToRemove);
            }
        }

        if (activeEnemies.Count == 0)
        {
            reloadCurrentLevel();
        }
    }


}
