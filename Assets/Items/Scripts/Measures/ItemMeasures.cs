using UnityEngine;

[CreateAssetMenu(fileName = "ItemMeasures", menuName = "ScriptableObjects/ItemMeasures")]
public class ItemMeasures : ScriptableObject
{
    public float Weight;
    public Vector3 Dimensions;
    public float Volume;

    public void Initialize(float weight, Vector3 dimensions, float volume) 
    {
        Weight = weight;
        Dimensions = dimensions;
        Volume = volume;
    }
}
