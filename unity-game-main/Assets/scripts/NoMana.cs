using UnityEngine;
using UnityEngine.UI;

public class NoMana : MonoBehaviour
{
    public GameObject player;
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody2D playerRigidbody; // Reference to the player's Rigidbody2D
    public float maxDragDistance = 15f;
    public float minDistanceToPlayer = 1f;
    public float maxDistanceToPlayer = 15f;
    public float collisionThreshold = 0.1f; // Adjust this value based on your requirements

    private bool canUseAsGround = true;
    public float groundCooldown = 2f; // Adjust this value based on your requirements

    

    void Start()
    {
        playerRigidbody = player.GetComponent<Rigidbody2D>();
    }

   
    void OnMouseDown()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= maxDragDistance && distanceToPlayer >= minDistanceToPlayer)
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;

            // Disable the player's Rigidbody2D while dragging
            playerRigidbody.simulated = false;
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            

            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            newPosition.z = transform.position.z;

            float distanceToPlayer = Vector3.Distance(newPosition, player.transform.position);

            // Check if the distance is within the desired range
            if (distanceToPlayer >= minDistanceToPlayer && distanceToPlayer <= maxDistanceToPlayer)
            {
                // Check for collision with player to prevent getting too close
                if (IsCollidingWithPlayer(newPosition))
                {
                    // Handle the collision by adjusting the position
                    Vector3 directionToPlayer = player.transform.position - newPosition;
                    directionToPlayer.Normalize();
                    transform.position = player.transform.position - directionToPlayer * (maxDistanceToPlayer - collisionThreshold);
                }
                else
                {
                    transform.position = newPosition;
                }
            }
            else
            {
                // Adjust the position to stay within the desired range
                Vector3 directionToPlayer = player.transform.position - newPosition;
                directionToPlayer.Normalize();
                transform.position = player.transform.position - directionToPlayer * maxDistanceToPlayer;
                
            }
        }
    }

    void OnMouseUp()
    {
        isDragging = false;


        // Re-enable the player's Rigidbody2D when dragging is complete
        playerRigidbody.simulated = true;
    }

    bool IsCollidingWithPlayer(Vector3 position)
    {
        // Check if the object is colliding with the player using Physics2D.OverlapCircle
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, collisionThreshold);
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject == player)
            {
                if (canUseAsGround)
                {
                    canUseAsGround = false;
                    Invoke("ResetGroundCooldown", groundCooldown);
                    return true;
                }
                return false;
            }
        }
        return false;
    }

    void ResetGroundCooldown()
    {
        canUseAsGround = true;
    }

    

    
}


