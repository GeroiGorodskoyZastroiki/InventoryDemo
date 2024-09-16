using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [SerializeField] private Item Item;

    private void Start()
    {
        Inventory inventory = GetComponent<Inventory>();
        Debug.Log(inventory.AddItem(Item));
        Debug.Log(inventory.ItemsVolume);
        inventory.TakeOutItem(Item);
        Debug.Log(inventory.ItemsVolume);
    }
}
