using UnityEngine;
using UnityEngine.SceneManagement; // à ajouter en haut du script

public class PlayerMovement : MonoBehaviour
{
    // Réglages du mouvement
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 15f;
    
    // Gestion des vérifications au sol
    public Transform groundCheck;
    public LayerMask groundLayer;
    private bool isGrounded;

    // Composants
    private Rigidbody2D rb;
    private float horizontal;
    private bool isRunning;

    void Start()
{
    rb = GetComponent<Rigidbody2D>();

    // Récupère le nom de la scène actuelle
    string currentScene = SceneManager.GetActiveScene().name;

    // Change la vitesse si on est dans Level2
    if (currentScene == "Level2")
    {
        walkSpeed *= 0.6f;  // exemple : 40% plus lent
        runSpeed *= 0.6f;
        jumpForce *= 0.8f;  // ajustement du saut optionnel
    }
}

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Vérifie si le joueur est au sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Saut
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        FlipSprite();
    }

    void FixedUpdate()
    {
        // Mouvement horizontal avec gestion course/marche
        float moveSpeed = isRunning ? runSpeed : walkSpeed;
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    // Retourne le sprite selon la direction
    void FlipSprite()
    {
        if (horizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
