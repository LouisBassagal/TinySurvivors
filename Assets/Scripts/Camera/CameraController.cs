using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineCamera))]
[RequireComponent(typeof(CinemachineImpulseSource))]
public class CameraController : MonoBehaviour
{
    static public CameraController Instance;

    private CinemachineCamera m_camera;
    private CinemachineImpulseSource m_impulseSource;
    private Coroutine m_zoomCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }

        m_camera = GetComponent<CinemachineCamera>();
        m_impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeCamera(float intensity)
    {
        m_impulseSource.GenerateImpulse(intensity);
    }

    public void Zoom(float value)
    {
        if (m_zoomCoroutine != null)
        {
            StopCoroutine(m_zoomCoroutine);
        }
        m_zoomCoroutine = StartCoroutine(ZoomCoroutine(value));
    }

    public Vector3 GetCameraBounds()
    {
        float height = 2f * m_camera.Lens.OrthographicSize;
        float width = height * m_camera.Lens.Aspect;
        return new Vector3(width, height, 0f);
    }

    public Vector3 GetCameraCenter()
    {
        return m_camera.transform.position;
    }

    private IEnumerator ZoomCoroutine(float value)
    {
        float startSize = m_camera.Lens.OrthographicSize;
        AnimationCurve curve = AnimationCurve.EaseInOut(0f, startSize, 1f, value);
        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newSize = curve.Evaluate(elapsed / duration);
            m_camera.Lens.OrthographicSize = newSize;
            yield return null;
        }

        m_camera.Lens.OrthographicSize = value;
        m_zoomCoroutine = null;
    }
}
