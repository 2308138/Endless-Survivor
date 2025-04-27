using UnityEngine;
using Unity.Cinemachine;
using UnityEditor.Search;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;
    private Transform camTransform;
    private float shakeDuration = 0F;
    private float shakeMagnitude = 0.1F;
    private float dampingSpeed = 1.0F;
    private Vector3 initialPosition;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        camTransform = Camera.main.transform;
        initialPosition = camTransform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;
            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0F;
            camTransform.localPosition = initialPosition;
        }
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}