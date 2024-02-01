using UnityEngine;

public class BuildingSaveLoad : MonoBehaviour
{
    // 저장할 키들
    private const string PositionKey = "BuildingPosition";
    private const string RotationKey = "BuildingRotation";
    private const string NameKey = "BuildingName";

    // Start 함수에서 데이터 불러오기 시도
    void Start()
    {
        LoadBuildingTransform();
    }

    // 빈 오브젝트의 위치, 회전값, 이름을 저장하는 함수
    public void SaveBuildingTransform()
    {
        // 현재 빈 오브젝트의 위치, 회전값, 이름 저장
        PlayerPrefs.SetString(PositionKey, transform.position.ToString());
        PlayerPrefs.SetString(RotationKey, transform.rotation.eulerAngles.ToString());
        PlayerPrefs.SetString(NameKey, gameObject.name);

        // PlayerPrefs에 저장된 데이터를 디스크에 즉시 기록
        PlayerPrefs.Save();
    }

    // 빈 오브젝트의 위치, 회전값, 이름을 불러오는 함수
    public void LoadBuildingTransform()
    {
        // 저장된 데이터가 있는지 확인
        if (PlayerPrefs.HasKey(PositionKey) && PlayerPrefs.HasKey(RotationKey) && PlayerPrefs.HasKey(NameKey))
        {
            // 저장된 위치, 회전값, 이름을 문자열에서 Vector3, Quaternion, string으로 변환
            Vector3 savedPosition = StringToVector3(PlayerPrefs.GetString(PositionKey));
            Quaternion savedRotation = Quaternion.Euler(StringToVector3(PlayerPrefs.GetString(RotationKey)));
            string savedName = PlayerPrefs.GetString(NameKey);

            // 빈 오브젝트의 위치, 회전값, 이름을 불러온 값으로 설정
            transform.position = savedPosition;
            transform.rotation = savedRotation;
            gameObject.name = savedName;
        }
    }

    // 문자열을 Vector3로 변환하는 함수
    private Vector3 StringToVector3(string s)
    {
        string[] split = s.Trim('(', ')').Split(',');

        float x = float.Parse(split[0]);
        float y = float.Parse(split[1]);
        float z = float.Parse(split[2]);

        return new Vector3(x, y, z);
    }
}
