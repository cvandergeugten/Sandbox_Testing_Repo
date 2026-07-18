using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionColorChanger : MonoBehaviour
{
    public Color collisionColor = Color.red;
    private Renderer objectRenderer;
    private Color originalColor;

    void Awake()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Test_Dummy"))
        {
            SetColor(collisionColor);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Test_Dummy"))
        {
            SetColor(collisionColor);
        }
    }

    private void SetColor(Color color)
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = color;
        }
    }

    public void ResetColor()
    {
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor;
        }
    }
}
