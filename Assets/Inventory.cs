using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private float _maxWeight;
    [SerializeField] private Vector3 _maxDimensions;
    [SerializeField] private float _maxVolume;

    [SerializeField] private float _maxWeightDeviation;
    [SerializeField] private Vector3 _maxDimensionsDeviation;
    [SerializeField] private float _maxVolumeDeviation;

    public float ItemsWeight { get => _items.Sum(i => i.Measures.Weight); }
    public float ItemsVolume { get => _items.Sum(i => i.Measures.Volume); }

    [SerializeField] private List<Item> _items;

    private bool IsFits(Item item)
    {
        if (item.Measures.Weight > (_maxWeight + _maxWeightDeviation)) return false;
        if (item.Measures.Volume > (_maxVolume + _maxVolumeDeviation)) return false;
        var orderedInventoryDimensions = _maxDimensions.ToArray().Zip(_maxDimensionsDeviation.ToArray(), (x, y) => x + y).OrderByDescending(x => x);
        var orderedItemDimensions = item.Measures.Dimensions.ToArray().OrderByDescending(x => x);
        return orderedInventoryDimensions.Zip(orderedItemDimensions, (invDim, itmDim) => invDim > itmDim).All(res => res);
    }

    public bool AddItem(Item item)
    {
        if (IsFits(item) && item.gameObject != gameObject)
        {
            _items.Insert(0, item);
            return true;
        }
        return false;
    }

    public void TakeOutItem(Item item)
    {
        _items.Remove(item);
    }
}
