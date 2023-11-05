using Unity.VisualScripting;
using UnityEngine;

public class SinWaveMovement : MonoBehaviour
{
    private float amplitude = 0.12f;
    private float frequency = 0.2f;
   // public float speed = 1.0f;

    private float startTime;
    private Vector3 initialPosition;

    void Start()
    {
        startTime = Time.time;
        initialPosition = transform.position; 
    }

    void Update()
    {
   
        float time = Time.time - startTime;
        float yOffset = Mathf.Sin(time * frequency * Mathf.PI * 2) * amplitude;

        Vector3 newPosition = initialPosition + new Vector3(0, yOffset, 0);

        transform.position = newPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
