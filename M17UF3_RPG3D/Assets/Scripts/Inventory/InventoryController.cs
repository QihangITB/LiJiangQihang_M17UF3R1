using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public static InventoryController Instance;

    // El orden de los items de la lista han de ser el mismo
    public List<GameObject> AllItems = new List<GameObject>();
    public List<GameObject> ItemCanvas = new List<GameObject>();

    private Dictionary<GameObject, bool> _myItems = new Dictionary<GameObject, bool>();

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
        Debug.Log($"Trigger Entered with {other.name}");
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
            _myItems.Add(item, false);
        }

        foreach (GameObject canvas in ItemCanvas)
        {
            canvas.SetActive(false);
        }
    }

    public void DeleteEquippedItem(GameObject item)
    {
        if(IsInTheInventory(item))
        {
            RemoveItem(item);
        }
    }

    private bool IsInTheInventory(GameObject item)
    {
        if (_myItems.ContainsKey(item))
        {
            return _myItems[item];
        }
        else
        {
            Debug.LogError($"Item '{item.name}' not found in inventory.");
            return false;
        }
    }

    private void AddItem(GameObject item)
    {
        _myItems[item] = true;

        // Activar UI
        int index = AllItems.IndexOf(item);
        ItemCanvas[index].SetActive(true);
    }

    private void RemoveItem(GameObject item)
    {
        _myItems[item] = false;

        // Desactivar UI
        int index = AllItems.IndexOf(item);
        ItemCanvas[index].SetActive(false);
    }
}
