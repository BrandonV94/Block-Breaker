using UnityEngine;

public class Ball : MonoBehaviour
{
    // Config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.2f;

    // State paramters
    Vector3 paddleToBallVector;
    bool hasStarted = false;

    // Cached component ref
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    void Start()
    {
        // Determines how far away the ball is from the paddle and sets it as a vector.
        paddleToBallVector = transform.position - paddle1.transform.position;
        // Sets the AudioSource component to the variable myAudioSource.
        myAudioSource = GetComponent<AudioSource>();
        // Sets the Rigidbody component to the variable myRigidBody2D.
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Determines when the game has begun by checking if the mouse has been clicked.
    void Update()
    {
        if(!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }
    }

    // Launches the ball when the left mouse is clicked down.
    private void LaunchOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            // Moves the ball from its inistial position.
            myRigidBody2D.velocity = new Vector2(xPush, yPush); 
        }
    }

    // Locks the ball in position above the paddle.
    private void LockBallToPaddle()
    {
        Vector3 paddlePos = new Vector3(paddle1.transform.position.x, paddle1.transform.position.y, -1);
        transform.position = paddlePos + paddleToBallVector;
    }

    // Allows the ball to bounce off the paddle in a semi-random posistion.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Variable used to have the ball bounce in a random direction.
        Vector2 velocityTweak = new Vector2
            (Random.Range(0f, randomFactor), 
            Random.Range(0f, randomFactor));

        // Plays the bounce sounds when the game has begun.
        if(hasStarted)
        {
            // Plays a random audio clip from the array.
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            // Play the sound one time and destroy the game object from the hiarchy.
            myAudioSource.PlayOneShot(clip);
            // Adds the random factor to the ball bounce.
            myRigidBody2D.velocity += velocityTweak;
        }
    }
}
