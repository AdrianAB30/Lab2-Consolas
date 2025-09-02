using UnityEngine;
using static MenuManager;

public class TankSpawner : MonoBehaviour
{
    public GameObject tankPrefab;
    public Transform[] spawnPoints; 

    void Start()
    {
        if (PlayerConfig.SelectedMode == 2)
        {
            Instantiate(tankPrefab, spawnPoints[0].position, spawnPoints[0].rotation);
        }
        else if (PlayerConfig.SelectedMode == 4)
        {
            
            Instantiate(tankPrefab, spawnPoints[0].position, spawnPoints[0].rotation);
            Instantiate(tankPrefab, spawnPoints[1].position, spawnPoints[1].rotation);
        }
    }
}
