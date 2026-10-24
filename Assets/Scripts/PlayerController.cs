using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    public float force = 1f;
    private InputSystem inputActions;
    private Vector3 cursorPos;
    public LayerMask layer;
    public LineRenderer Tongue;
    private bool isTongued = false;
    public float maxForce = 5f;
    public Transform mouth;
    public float tongueRange = 25f;
    public AudioSource frogMouth;
    public AudioClip[] frogNoises;
    private float tongueTimer = 10f;

    void Awake()
    {
        inputActions = new InputSystem();
        rb = GetComponent<Rigidbody2D>();
        Tongue = GetComponentInChildren<LineRenderer>();
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
        HandleMovement();

        if (inputActions.Player.Jump.WasPressedThisFrame()) frogMouth.PlayOneShot(frogNoises[1]);
        if (inputActions.Player.Attack.WasPressedThisFrame()) frogMouth.PlayOneShot(frogNoises[0]);

        Tongue.SetPosition(0, transform.position);

        if (inputActions.Player.Attack.IsPressed())
        {
            OnAttack();
            isTongued = true;
        }
        else
        {
            Tongue.SetPosition(1, mouth.position);
            isTongued = false;
        }
    }

    void OnAttack()
    {
        if (tongueTimer > 0f)
        {
            tongueTimer -= Time.deltaTime;
        } 
        else
        {
            frogMouth.PlayOneShot(frogNoises[1]);
            Tongue.SetPosition(1, mouth.position);
            inputActions.Player.Attack.Disable();
            inputActions.Player.Attack.Enable();
            tongueTimer = 10f;
        }

        if(!isTongued)
        {
            tongueTimer = 10f;

            cursorPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            cursorPos.z = 0f;
            Vector2 rayDir = cursorPos - mouth.position;

            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, rayDir, tongueRange, layer);

            if (hitInfo != null)
            {
                Tongue.SetPosition(1, hitInfo.point);
            }
        }
    }
        

    void HandleMovement()
    {
        Vector2 moveDir = Tongue.GetPosition(1) - mouth.position;
        Vector2 forceVal = moveDir * force * Time.deltaTime;
        forceVal = Vector2.ClampMagnitude(forceVal, maxForce);

        rb.linearVelocity += forceVal;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Deadly")) SceneManager.LoadScene(0);

       
    }
}
