using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;

    public GameObject ThirdPerson;
    public GameObject FirstPerson;
    public GameObject FrontPerson;

    private Dictionary<string, GameObject> _cameras;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _cameras = new Dictionary<string, GameObject>
        {
            { ConstantValue.ThirdPerson, ThirdPerson },
            { ConstantValue.FirstPerson, FirstPerson },
            { ConstantValue.FrontPerson, FrontPerson }
        };
    }

    public void SetCameraByName(string name)
    {
        foreach (var camera in _cameras)
        {
            camera.Value.SetActive(false);
        }

        if (_cameras.ContainsKey(name))
        {
            _cameras[name].SetActive(true);
        }
        else
        {
            Debug.LogWarning($"Camera '{name}' not found.");
        }
    }

    public void SetTemporalCameraByName(string name, float time)
    {
        string previousCamera = GetActiveCameraName();

        foreach (var camera in _cameras)
        {
            camera.Value.SetActive(false);
        }

        if (_cameras.ContainsKey(name))
        {
            _cameras[name].SetActive(true);
            StartCoroutine(DisableCameraAfterTime(previousCamera, name, time));
        }
        else
        {
            Debug.LogWarning($"Camera '{name}' not found.");
        }
    }

    private IEnumerator DisableCameraAfterTime(string previousCam, string cam, float time)
    {
        _cameras[cam].SetActive(true);

        yield return new WaitForSeconds(time);

        _cameras[cam].SetActive(false);
        _cameras[previousCam].SetActive(true);
    }

    public string GetActiveCameraName()
    {
        foreach (var camera in _cameras)
        {
            if (camera.Value.activeSelf)
            {
                return camera.Key;
            }
        }

        return null; // No active camera found
    }
}
