using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private Transform transform;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    private float shakeMagnitude = 0.1f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.5f;

    // The initial position of the GameObject
    Vector3 initialPosition;
    void Awake()
    {
        if (transform == null)
        {
            transform = GetComponent(typeof(RectTransform)) as RectTransform;
        }
    }
    void OnEnable()
    {
        initialPosition = transform.position;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.position = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.position = initialPosition;
        }
    }
    public void TriggerShake()
    {
        shakeDuration = 0.8f;
    }
}
