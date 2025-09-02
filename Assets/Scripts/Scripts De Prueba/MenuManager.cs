using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private Camera MainCamara;

    private CameraSetup camSetup;

    void Awake()
    {
        camSetup = Object.FindFirstObjectByType<CameraSetup>();
    }

    public void PlayTwoPlayers()
    {
        StartGame(2);
    }

    public void PlayFourPlayers()
    {
        StartGame(4);
    }

    private void StartGame(int players)
    {
        menuPanel.SetActive(false);
        MainCamara.gameObject.SetActive(false);

        if (players == 2)
        {
            GameObject t1 = Instantiate(tankPrefab, spawnPoints[0].position, spawnPoints[0].rotation);
            camSetup.RegisterTankCameras(t1);

            // Asignar ID al primer tanque
            t1.GetComponent<TankPlayerAssigner>().SetTankID(1);
        }
        else if (players == 4)
        {
            GameObject t1 = Instantiate(tankPrefab, spawnPoints[0].position, spawnPoints[0].rotation);
            GameObject t2 = Instantiate(tankPrefab, spawnPoints[1].position, spawnPoints[1].rotation);

            camSetup.RegisterTankCameras(t1);
            camSetup.RegisterTankCameras(t2);

            // IDs diferentes
            t1.GetComponent<TankPlayerAssigner>().SetTankID(1);
            t2.GetComponent<TankPlayerAssigner>().SetTankID(2);
        }

        camSetup.ConfigureCameras(players);

        if (enemySpawner != null)
            enemySpawner.enabled = true;
    }
}
