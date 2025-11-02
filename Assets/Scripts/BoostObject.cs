using UnityEngine;

public class BoostObject : MonoBehaviour
{
    [Header("Config Boost")]
    public float boostForce = 5f; // Total do boost aplicano na bola

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica objeto que colidiu
        if (collision.gameObject.CompareTag("Ball"))
            ApplyBoost(collision.gameObject);
    }

    private void ApplyBoost(GameObject ball)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            Vector2 currentDirection = rb.linearVelocity.normalized;

            rb.AddForce(currentDirection * boostForce, ForceMode2D.Impulse);

            Debug.Log("Impulso aplicado");
        }
    }
}
