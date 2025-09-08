using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject panelPlayers;

    [Header("Tanks (prefabs en la escena)")]
    [SerializeField] private GameObject tank1Prefab;
    [SerializeField] private GameObject tank2Prefab;

    [Header("Cámaras")]
    [SerializeField] private Camera camTank1;
    [SerializeField] private Camera camTurret1;
    [SerializeField] private Camera camTank2;
    [SerializeField] private Camera camTurret2;

    private static GameManager Instance;

    private int maxPlayers = 2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    private void Start()
    {
        if (panelPlayers != null)
        {
            panelPlayers.SetActive(false);
        }
        tank2Prefab.SetActive(false);

        camTank2.gameObject.SetActive(false);
        camTurret2.gameObject.SetActive(false);
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
        maxPlayers = 2;

        tank1Prefab.SetActive(true);
        tank2Prefab.SetActive(false);

        camTank1.gameObject.SetActive(true);
        camTurret1.gameObject.SetActive(true);
        camTank2.gameObject.SetActive(false);
        camTurret2.gameObject.SetActive(false);

        camTank1.rect = new Rect(0f, 0f, 1f, 0.5f);
        camTurret1.rect = new Rect(0f, 0.5f, 1f, 0.5f);

        SceneManager.LoadScene("Game");
    }

    public void PlayFourPlayers()
    {
        maxPlayers = 4;

        tank1Prefab.SetActive(true);
        tank2Prefab.SetActive(true);

        camTank1.gameObject.SetActive(true);
        camTurret1.gameObject.SetActive(true);
        camTank2.gameObject.SetActive(true);
        camTurret2.gameObject.SetActive(true);

        camTank1.rect = new Rect(0f, 0f, 0.5f, 0.5f);
        camTurret1.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
        camTank2.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
        camTurret2.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);

        SceneManager.LoadScene("Game");

    }

    private void OnEnable()
    {
        var manager = FindFirstObjectByType<PlayerInputManager>();
        if (manager != null)
        {
            manager.onPlayerJoined += OnPlayerJoined;
        }
    }

    private void OnDisable()
    {
        var manager = FindFirstObjectByType<PlayerInputManager>();
        if (manager != null)
        {
            manager.onPlayerJoined -= OnPlayerJoined;
        }
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Jugador unido: " + playerInput.playerIndex);

        if (playerInput.playerIndex >= maxPlayers)
        {
            Destroy(playerInput.gameObject); 
            return;
        }

    }
}
