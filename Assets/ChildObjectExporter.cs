using UnityEngine;
using System.IO;

public class ChildObjectExporter : MonoBehaviour
{
    // ���� ��� - Assets ���� ���� ��� ���
    public string filePath = "Assets/ChildObjectsData.txt";

    void Start()
    {
        ExportChildObjectsData();
    }

    void ExportChildObjectsData()
    {
        // ���Ͽ� ������ ����
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (Transform child in transform)
            {
                // ������Ʈ�� �̸�, ��ġ, ȸ����, ũ�⸦ ���Ͽ� ���
                writer.WriteLine("Object Name: " + child.name);
                writer.WriteLine("Position: " + child.position);
                writer.WriteLine("Rotation: " + child.rotation.eulerAngles);
                writer.WriteLine("Scale: " + child.localScale);
                writer.WriteLine(); // ������ ���� �� �� �߰�
            }
        }

        Debug.Log("Child objects data exported to " + filePath);

        // ���� ������ �ֿܼ� ���
        PrintFileContents(filePath);

        // "�Ϸ�!" �޽��� ���
        Debug.Log("�Ϸ�!");
    }

    // ���� ������ �ֿܼ� ����ϴ� �Լ�
    void PrintFileContents(string path)
    {
        try
        {
            // ������ �о �ֿܼ� ���
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                Debug.Log(line);
            }
        }
        catch (IOException e)
        {
            Debug.LogError("������ �д� �� ������ �߻��߽��ϴ�: " + e.Message);
        }
    }
}
