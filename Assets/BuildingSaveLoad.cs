using UnityEngine;

public class BuildingSaveLoad : MonoBehaviour
{
    // ������ Ű��
    private const string PositionKey = "BuildingPosition";
    private const string RotationKey = "BuildingRotation";
    private const string NameKey = "BuildingName";

    // Start �Լ����� ������ �ҷ����� �õ�
    void Start()
    {
        LoadBuildingTransform();
    }

    // �� ������Ʈ�� ��ġ, ȸ����, �̸��� �����ϴ� �Լ�
    public void SaveBuildingTransform()
    {
        // ���� �� ������Ʈ�� ��ġ, ȸ����, �̸� ����
        PlayerPrefs.SetString(PositionKey, transform.position.ToString());
        PlayerPrefs.SetString(RotationKey, transform.rotation.eulerAngles.ToString());
        PlayerPrefs.SetString(NameKey, gameObject.name);

        // PlayerPrefs�� ����� �����͸� ��ũ�� ��� ���
        PlayerPrefs.Save();
    }

    // �� ������Ʈ�� ��ġ, ȸ����, �̸��� �ҷ����� �Լ�
    public void LoadBuildingTransform()
    {
        // ����� �����Ͱ� �ִ��� Ȯ��
        if (PlayerPrefs.HasKey(PositionKey) && PlayerPrefs.HasKey(RotationKey) && PlayerPrefs.HasKey(NameKey))
        {
            // ����� ��ġ, ȸ����, �̸��� ���ڿ����� Vector3, Quaternion, string���� ��ȯ
            Vector3 savedPosition = StringToVector3(PlayerPrefs.GetString(PositionKey));
            Quaternion savedRotation = Quaternion.Euler(StringToVector3(PlayerPrefs.GetString(RotationKey)));
            string savedName = PlayerPrefs.GetString(NameKey);

            // �� ������Ʈ�� ��ġ, ȸ����, �̸��� �ҷ��� ������ ����
            transform.position = savedPosition;
            transform.rotation = savedRotation;
            gameObject.name = savedName;
        }
    }

    // ���ڿ��� Vector3�� ��ȯ�ϴ� �Լ�
    private Vector3 StringToVector3(string s)
    {
        string[] split = s.Trim('(', ')').Split(',');

        float x = float.Parse(split[0]);
        float y = float.Parse(split[1]);
        float z = float.Parse(split[2]);

        return new Vector3(x, y, z);
    }
}
