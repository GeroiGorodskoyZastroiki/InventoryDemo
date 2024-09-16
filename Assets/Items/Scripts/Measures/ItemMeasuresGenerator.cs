using UnityEngine;

public class ItemMeasuresGenerator : MonoBehaviour
{
    public Mesh Mesh;

    public float Weight;
    public Vector3 Dimensions;
    public float Volume;

    public bool _calculateFromDimensions;
    public static string AssetFolderPath = "Assets/Items/ItemMeasures";

    public void CalculateMeasures()
    {
        Dimensions = GetBounds(Mesh);
        Volume = _calculateFromDimensions ? CalculateVolume(Dimensions) : CalculateVolume(Mesh);
    }

    private Vector3 GetBounds(Mesh mesh) =>
        mesh.bounds.size;

    private float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        var v321 = p3.x * p2.y * p1.z;
        var v231 = p2.x * p3.y * p1.z;
        var v312 = p3.x * p1.y * p2.z;
        var v132 = p1.x * p3.y * p2.z;
        var v213 = p2.x * p1.y * p3.z;
        var v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }

    private float CalculateVolume(Mesh mesh)
    {
        float volume = 0;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        return Mathf.Abs(volume);
    }

    private float CalculateVolume(Vector3 dimensions) =>
        dimensions.x * dimensions.y * dimensions.z;
}
