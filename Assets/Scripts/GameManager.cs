using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int playerCount = 2;

    [SerializeField] private GameObject panelPlayers;

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
}
