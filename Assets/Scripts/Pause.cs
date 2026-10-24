using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    private InputSystem inputActions;
    public bool isPaused = false;
    public GameObject pauseMenu;
    public PlayerController pc;

    void Awake()
    {
        inputActions = new InputSystem();
        pc = GetComponent<PlayerController>();
    }

    void OnEnable() => inputActions.Player.Enable();
    void OnDisable() => inputActions.Player.Disable();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputActions.Player.Pause.WasPressedThisFrame())
        {
            isPaused = !isPaused;

            if(isPaused)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                pc.enabled = false;
            }
            else
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                pc.enabled = true;
            }
        }

        if (!pauseMenu.activeSelf)
        {
            Time.timeScale = 1f;
            pc.enabled = true;
        }
    }
}
