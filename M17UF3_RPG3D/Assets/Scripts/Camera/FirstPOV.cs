using UnityEngine;

public class FirstPOV : MonoBehaviour
{
    public GameObject playerRender;
    public GameObject playerBody;

    public Transform point;

    private void OnEnable()
    {
        playerRender.SetActive(false);
        playerBody.SetActive(false);

        //string name = InventoryController.Instance.GetItemInstance().name.Replace("(Clone)","");
        //GameObject weapon = InventoryController.Instance.GetPrefabByName(name);

        //GameObject instance = Instantiate(weapon, point);
        //instance.transform.localPosition = Vector3.zero;
        //instance.transform.localRotation = weapon.transform.rotation;
    }

    private void OnDisable()
    {
        playerRender.SetActive(true);
        playerBody.SetActive(true);
    }

    private void FixedUpdate()
    {
        playerRender.transform.forward = transform.forward;

        point.position = Camera.main.transform.position + Camera.main.transform.forward * 1.0f; // 1.0f es la distancia hacia adelante
        point.forward = Camera.main.transform.forward;
    }
}
