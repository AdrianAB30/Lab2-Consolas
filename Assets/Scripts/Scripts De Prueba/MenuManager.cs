using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject[] tanks;
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
            if (tanks.Length >= 1)
            {
                GameObject t1 = tanks[0];
                t1.SetActive(true);
                camSetup.RegisterTankCameras(t1);
                t1.GetComponent<TankPlayerAssigner>().SetTankID(1);
            }
        }
        else if (players == 4)
        {
            if (tanks.Length >= 2)
            {
                GameObject t1 = tanks[0];
                t1.SetActive(true);
                camSetup.RegisterTankCameras(t1);
                t1.GetComponent<TankPlayerAssigner>().SetTankID(1);

                GameObject t2 = tanks[1];
                t2.SetActive(true);
                camSetup.RegisterTankCameras(t2);
                t2.GetComponent<TankPlayerAssigner>().SetTankID(2);
            }
        }

        camSetup.ConfigureCameras(players);

        if (enemySpawner != null)
            enemySpawner.enabled = true;
    }
}
