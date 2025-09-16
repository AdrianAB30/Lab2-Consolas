using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int playerCount = 2;

    [SerializeField] private GameObject panelPlayers;

    private void OnEnable()
    {
        PowerUps.OnEffectFinished += HandlePowerUpFinished;
    }
    private void OnDisable()
    {
        PowerUps.OnEffectFinished -= HandlePowerUpFinished;
    }

    private void Start()
    {
        if (panelPlayers != null)
        {
            panelPlayers.SetActive(false);
        }
    }

    public void PlayGame()
    {
        if (panelPlayers != null)
        {
            panelPlayers.SetActive(true);
        }
    }
    public void PlayTwoPlayers()
    {
        playerCount = 2;
        SceneManager.LoadScene("Game");
    }

    public void PlayFourPlayers()
    {
        playerCount = 4;
        SceneManager.LoadScene("Game");
    }

    public void PVP()
    {
        playerCount = 4;
        SceneManager.LoadScene("PVP");
    }

    private void HandlePowerUpFinished(PowerUps powerUp)
    {
        powerUp.gameObject.SetActive(false);
    }
}
