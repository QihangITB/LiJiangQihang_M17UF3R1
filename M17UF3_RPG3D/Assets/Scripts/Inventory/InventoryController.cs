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

        InitializeEmptyInventory();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstantValue.ItemTag) && !IsInTheInventory(other.gameObject))
        {
            AddItem(other.gameObject);
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
        GameObject item = GetPrefabByName(itemName);
        if (IsInTheInventory(item))
        {
            RemoveItem(item);
            RemoveItemInstance();
        }
    }

    private bool IsInTheInventory(GameObject item)
    {
        if (_myItems.ContainsKey(item.name))
        {
            return _myItems[item.name];
        }
        else
        {
            Debug.Log($"Item '{item.name}' not exists.");
            return false;
        }
    }

    private void AddItem(GameObject item)
    {
        _myItems[item.name] = true;

        // Activar UI
        int index = GetItemIndex(item);
        ItemCanvas[index].SetActive(true);
    }

    private void RemoveItem(GameObject item)
    {
        _myItems[item.name] = false;

        // Desactivar UI
        int index = GetItemIndex(item);
        ItemCanvas[index].SetActive(false);
    }

    private int GetItemIndex(GameObject item)
    {
        for (int i = 0; i < AllItems.Count; i++)
        {
            if (AllItems[i].name == item.name)
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

        if (IsInTheInventory(item))
        {
            GameObject itemInstance = Instantiate(item, RightHand);

            // Ajustar la posición y rotación del item
            itemInstance.transform.localPosition = Vector3.zero;
            itemInstance.transform.localRotation = item.transform.rotation;
        }
    }

    public void RemoveItemInstance()
    {
        foreach (Transform item in RightHand)
        {
            Destroy(item.gameObject);
        }
    }

    private GameObject GetPrefabByName(string name)
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
