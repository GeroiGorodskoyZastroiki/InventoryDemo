using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemMeasuresGenerator))]
public class ItemMeasuresGeneratorEditor : Editor
{
    bool isInitialized = false;  // ���� ��� �������� ������� �������� ���������

    void OnEnable()
    {
        // ��������� ������������� � ������������� �������� meshProperty, ���� ��� ������
        if (!isInitialized)
        {
            ItemMeasuresGenerator generator = (ItemMeasuresGenerator)target;
            MeshFilter meshFilter = generator.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.sharedMesh != null)
                generator.Mesh = meshFilter.sharedMesh;
            isInitialized = true;  // ������������� ���� ��� �������������� ��������� �������������
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemMeasuresGenerator generator = (ItemMeasuresGenerator)target;

        if (GUILayout.Button("Calculate Measures"))
        {
            generator.CalculateMeasures();
            EditorUtility.SetDirty(generator); // �������� ������ � ���������
        }

        if (GUILayout.Button("Generate ItemMeasures Scriptable Object Asset"))
            GenerateItemMeasuresAsset(generator);
    }

    private void GenerateItemMeasuresAsset(ItemMeasuresGenerator generator)
    {
        // ������� ����� ���� ��� �� ����������
        if (!AssetDatabase.IsValidFolder(ItemMeasuresGenerator.AssetFolderPath))
            AssetDatabase.CreateFolder("Assets", "ItemMeasures");

        // ������� �����
        ItemMeasures itemMeasures = CreateInstance<ItemMeasures>();
        itemMeasures.Initialize(generator.Weight, generator.Dimensions, generator.Volume);

        // ���������� ���� � ��� �����
        string assetPath = $"{ItemMeasuresGenerator.AssetFolderPath}/{generator.gameObject.name}.asset";

        if (AssetDatabase.AssetPathExists(assetPath))
            AssetDatabase.DeleteAsset(assetPath);

        //��������� �����
        AssetDatabase.CreateAsset(itemMeasures, assetPath);
        AssetDatabase.SaveAssets();

        // ��������� ��� �������� ��������� Item
        Item itemComponent = generator.GetComponent<Item>();
        if (!itemComponent)
            itemComponent = generator.gameObject.AddComponent<Item>();

        // ������������� ������ �� ��������� �����
        itemComponent.Measures = itemMeasures;

        // ������� ��������� ItemMeasuresGenerator
        DestroyImmediate(generator);

        // ��������� ������
        AssetDatabase.Refresh();
    }
}
