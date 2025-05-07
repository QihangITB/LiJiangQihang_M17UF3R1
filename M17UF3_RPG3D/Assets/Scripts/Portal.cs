using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstantValue.PlayerTag))
        {
            string sceneName = SceneManager.GetActiveScene().name;

            if (sceneName == ConstantValue.Outside)
            {
                SceneManager.LoadScene(ConstantValue.Inside);
            }
            else
            {
                SceneManager.LoadScene(ConstantValue.Outside);
            }
        }
    }
}
