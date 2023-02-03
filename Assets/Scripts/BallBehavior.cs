using System.Collections;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    #region Variables
    
    [SerializeField] public float speed = 500;
    [SerializeField] private float acceleration = 1.1f;
    private Vector3 velocity;
    
    [SerializeField] private Direction ballDirection;
    [SerializeField] private Vector2 initialPos;
    [SerializeField] private GameObject score;
    
    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private Color color3;
    [SerializeField] private Color color4;
    
    [SerializeField] private CameraShake cameraShake;

    private int currentColor = 0;
    private Renderer sphereRenderer;

    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public static bool Play = true;
    private static readonly int Color1 = Shader.PropertyToID("_Color");

    #endregion

    #region Init
    private void Start()
    {
        score.GetComponent<ScoreHandler>().UpdateScore();
        sphereRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        ballDirection = Random.Range(0f, 1f) < 0.5 ? Direction.Left : Direction.Right;
        
        ResetBallPosition();
        StartMovement();
    }

    private void Update()
        => velocity = rb.velocity;

    #endregion

    #region Methods
    private void ResetBallPosition()
    {
        velocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = initialPos;
    }

    private void StartMovement()
        => rb.AddForce(new Vector2(1, ballDirection == Direction.Left ? -1f : 1f) * speed);

    private void OnPaddleCollision()
        => rb.velocity *= acceleration;

    private IEnumerator OnPlayerLose()
    {
        ResetBallPosition();
        yield return new WaitForSeconds(2);
        
        // if game is not over
        if (Play) StartMovement();
        else score.GetComponent<ScoreHandler>().UpdateScore();
    }

    private void ChangeColor()
    {
        currentColor = (currentColor + 1) % 4;
        switch (currentColor)
        {
            case 0:
                sphereRenderer.material.SetColor(Color1, color1);
                break;
            case 1:
                sphereRenderer.material.SetColor(Color1, color2);
                break;
            case 2:
                sphereRenderer.material.SetColor(Color1, color3);
                break;
            case 3:
                sphereRenderer.material.SetColor(Color1, color4);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ChangeColor();
        StartCoroutine(cameraShake.Shake(0.1f, 0.5f));
        
        if (collision.gameObject.CompareTag("Lose"))
        {
            Player player = collision.gameObject.GetComponent<PlayerScore>().player;
            score.GetComponent<ScoreHandler>().UpdateScore(player);

            ballDirection = player == Player.Player1 ? Direction.Left : Direction.Right;
            StartCoroutine(OnPlayerLose());
        }
            
        
        var velocityMagnitude = velocity.magnitude;
        // reflect function gets the normal angle of the collision
        var direction = Vector3.Reflect(velocity.normalized, collision.contacts[0].normal);
        rb.velocity = direction * Mathf.Max(velocityMagnitude, 0f);
        
        // increase speed if touch a paddle
        if (collision.gameObject.CompareTag("Paddle"))
            OnPaddleCollision();
    }

    #endregion
}
