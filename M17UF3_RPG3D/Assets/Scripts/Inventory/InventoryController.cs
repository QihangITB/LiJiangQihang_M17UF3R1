using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;

    // El orden de los items de la lista han de ser el mismo
    public List<GameObject> AllItems = new List<GameObject>();
    public List<GameObject> ItemCanvas = new List<GameObject>();

    public Transform LeftHand;
    public Transform RightHand;

    private Dictionary<string, bool> _myItems = new Dictionary<string, bool>();

    public Dictionary<string, bool> MyItems { get { return _myItems; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
            InitializeEmptyInventory();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstantValue.ItemTag) && !IsInTheInventory(other.gameObject.name))
        {
            AddItem(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }

    private void InitializeEmptyInventory()
    {
        foreach (GameObject item in AllItems)
        {
            _myItems.Add(item.name, false);
        }

        foreach (GameObject canvas in ItemCanvas)
        {
            canvas.SetActive(false);
        }
    }

    public void DeleteEquippedItem(string itemName)
    {
        if (IsInTheInventory(itemName))
        {
            RemoveItem(itemName);
            RemoveItemInstance();
        }
    }

    private bool IsInTheInventory(string itemName)
    {
        if (_myItems.ContainsKey(itemName))
        {
            return _myItems[itemName];
        }
        else
        {
            Debug.Log($"Item '{itemName}' not exists.");
            return false;
        }
    }

    public void AddItem(string itemName)
    {
        _myItems[itemName] = true;

        // Activar UI
        int index = GetItemIndexByName(itemName);
        ItemCanvas[index].SetActive(true);
    }

    public void RemoveItem(string itemName)
    {
        _myItems[itemName] = false;

        // Desactivar UI
        int index = GetItemIndexByName(itemName);
        ItemCanvas[index].SetActive(false);
    }

    private int GetItemIndexByName(string itemName)
    {
        for (int i = 0; i < AllItems.Count; i++)
        {
            if (AllItems[i].name == itemName)
            {
                return i;
            }
        }
        return ConstantValue.NotFound;
    }

    // PREFAB
    public void CreateItemInstance(string itemName)
    {
        GameObject item = GetPrefabByName(itemName);

        if (IsInTheInventory(itemName))
        {
            GameObject itemInstance = Instantiate(item, RightHand);

            // Ajustar la posición y rotación del item
            itemInstance.transform.localPosition = Vector3.zero;
            itemInstance.transform.localRotation = item.transform.rotation;

            // El rifle tiene su propia animacion
            if (item.name == ConstantValue.Rifle)
            {
                PlayerAnimationController.Instance.ActiveAim();
            }
            else
            {
                PlayerAnimationController.Instance.DeactiveAim();
            }
        }
        else
        {
            PlayerAnimationController.Instance.DeactiveAim();
        }
    }

    public void RemoveItemInstance()
    {
        foreach (Transform item in RightHand)
        {
            Destroy(item.gameObject);
        }
    }

    public GameObject GetItemInstance()
    {
        if (RightHand.childCount == 0)
            return null;

        GameObject item = RightHand.GetChild(0).gameObject;
        return item;
    }


    public GameObject GetPrefabByName(string name)
    {
        foreach (GameObject item in AllItems)
        {
            if (item.name == name)
            {
                return item;
            }
        }
        return null;
    }
}
