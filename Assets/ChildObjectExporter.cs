using UnityEngine;
using System.IO;

public class ChildObjectExporter : MonoBehaviour
{
    // 파일 경로 - Assets 폴더 내의 상대 경로
    public string filePath = "Assets/ChildObjectsData.txt";

    void Start()
    {
        ExportChildObjectsData();
    }

    void ExportChildObjectsData()
    {
        // 파일에 데이터 쓰기
        using (StreamWriter writer = new StreamWriter(filePath))
        {
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

        Debug.Log("Child objects data exported to " + filePath);

        // 파일 내용을 콘솔에 출력
        PrintFileContents(filePath);

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
