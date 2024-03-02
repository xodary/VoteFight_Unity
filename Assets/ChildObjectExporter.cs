using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ChildObjectExporter : MonoBehaviour
{
    // ���� ��� - Assets ���� ���� ��� ���
    public string textFilePath = "Assets/ChildObjectsData.txt";
    public string binaryFilePath = "Assets/ChildObjectsData.bin";

    void Start()
    {
        ExportChildObjectsData();
    }

    void ExportChildObjectsData()
    {
        // ���� ������Ʈ�� ���� ���� ����
        int childCount = transform.childCount;

        // �ؽ�Ʈ ���Ͽ� ������ ����
        using (StreamWriter writer = new StreamWriter(textFilePath))
        {
            // childCount�� ������ ���ۿ� ���
            writer.WriteLine("Child Count: " + childCount);

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

        Debug.Log("Child objects data exported to " + textFilePath);
        Debug.Log("���� ������Ʈ ��: " + childCount);

        // ���� ���Ͽ� ������ ����
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(binaryFilePath, FileMode.Create))
        {
            // ���� ���Ͽ� childCount ����
            formatter.Serialize(stream, childCount);

            foreach (Transform child in transform)
            {
                // ������Ʈ�� �̸�, ��ġ, ȸ����, ũ�⸦ ���� �������� ����ȭ�Ͽ� ���Ͽ� ���
                ObjectData data = new ObjectData(child.name, child.position, child.rotation.eulerAngles, child.localScale);
                formatter.Serialize(stream, data);
            }
        }

        Debug.Log("Child objects data exported to " + binaryFilePath);

        // ���� ������ �ֿܼ� ���
        PrintFileContents(textFilePath);

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

[System.Serializable]
class ObjectData
{
    public string name;
    public SerializableVector3 position;
    public SerializableVector3 rotation;
    public SerializableVector3 scale;

    public ObjectData(string name, Vector3 position, Vector3 rotation, Vector3 scale)
    {
        this.name = name;
        this.position = new SerializableVector3(position);
        this.rotation = new SerializableVector3(rotation);
        this.scale = new SerializableVector3(scale);
    }
}

[System.Serializable]
class SerializableVector3
{
    public float x, y, z;

    public SerializableVector3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ", " + z + ")";
    }
}
