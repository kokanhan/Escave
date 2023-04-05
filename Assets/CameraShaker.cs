using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    // Camera Information
    private Vector3 orignalCameraPos;

    // Shake Parameters
    public float shakeDuration = 2f;
    public float shakeAmount = 0.7f;

    private bool canShake = false;
    private float _shakeTimer;

    // Start is called before the first frame update
    void Start()
    {
        orignalCameraPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (canShake)
        {
            StartCameraShakeEffect();
        }
    }

    public void ShakeCamera()
    {
        canShake = true;
        _shakeTimer = shakeDuration;
    }

    public void StartCameraShakeEffect()
    {
        if (_shakeTimer > 0)
        {
            if(_shakeTimer < shakeDuration / 2)
            {
              transform.localPosition = orignalCameraPos + Random.insideUnitSphere * shakeAmount * outCircleLerp(_shakeTimer * 2f / shakeDuration);
            }
            else
            {
              transform.localPosition = orignalCameraPos + Random.insideUnitSphere * shakeAmount * outCircleLerp((shakeDuration - _shakeTimer) * 2f / shakeDuration);
            }

            _shakeTimer -= Time.deltaTime;
        }
        else
        {
            _shakeTimer = 0f;
            transform.localPosition = orignalCameraPos;
            canShake = false;
        }
    }

    private float outCircleLerp(float a)
    {
      return Mathf.Sqrt(1f - Mathf.Pow(a - 1f, 2));
    }
}
