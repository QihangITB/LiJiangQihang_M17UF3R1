using UnityEngine;

public class FirstPOV : MonoBehaviour
{
    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag(ConstantValue.PlayerTag);
    }

    private void FixedUpdate()
    {
        _player.transform.forward = transform.forward;
    }
}
