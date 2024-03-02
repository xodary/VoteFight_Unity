using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ChildObjectExporter : MonoBehaviour
{
    // 파일 경로 - Assets 폴더 내의 상대 경로
    public string textFilePath = "Assets/ChildObjectsData.txt";
    public string binaryFilePath = "Assets/ChildObjectsData.bin";

    void Start()
    {
        ExportChildObjectsData();
    }

    void ExportChildObjectsData()
    {
        // 하위 오브젝트의 수를 세는 변수
        int childCount = transform.childCount;

        // 텍스트 파일에 데이터 쓰기
        using (StreamWriter writer = new StreamWriter(textFilePath))
        {
            // childCount를 파일의 시작에 기록
            writer.WriteLine("Child Count: " + childCount);

            foreach (Transform child in transform)
            {
                // 오브젝트의 이름, 위치, 회전값, 크기를 파일에 기록
                writer.WriteLine("Object Name: " + child.name);
                writer.WriteLine("Position: " + child.position);
                writer.WriteLine("Rotation: " + child.rotation.eulerAngles);
                writer.WriteLine("Scale: " + child.localScale);
                writer.WriteLine(); // 구분을 위한 빈 줄 추가
            }
        }

        Debug.Log("Child objects data exported to " + textFilePath);
        Debug.Log("하위 오브젝트 수: " + childCount);

        // 이진 파일에 데이터 쓰기
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(binaryFilePath, FileMode.Create))
        {
            // 이진 파일에 childCount 저장
            formatter.Serialize(stream, childCount);

            foreach (Transform child in transform)
            {
                // 오브젝트의 이름, 위치, 회전값, 크기를 이진 형식으로 직렬화하여 파일에 기록
                ObjectData data = new ObjectData(child.name, child.position, child.rotation.eulerAngles, child.localScale);
                formatter.Serialize(stream, data);
            }
        }

        Debug.Log("Child objects data exported to " + binaryFilePath);

        // 파일 내용을 콘솔에 출력
        PrintFileContents(textFilePath);

        // "완료!" 메시지 출력
        Debug.Log("완료!");
    }

    // 파일 내용을 콘솔에 출력하는 함수
    void PrintFileContents(string path)
    {
        try
        {
            // 파일을 읽어서 콘솔에 출력
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                Debug.Log(line);
            }
        }
        catch (IOException e)
        {
            Debug.LogError("파일을 읽는 중 오류가 발생했습니다: " + e.Message);
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
