using UnityEngine;

[System.Serializable]
public class UserData
{
    public Vector3 position;
}

public class SaveLoadSample : MonoBehaviour
{
    [SerializeField] GameObject player;

    private void Update()
    {
        transform.position = player.transform.position;

        // 位置とHealthの状態をコンソールに表示
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Position: " + transform.position);
        }

        /*
        if (Input.GetKeyDown(KeyCode.K))
        {
            UserData data = new UserData()
            {
                position = transform.position
            };
            string json = JsonUtility.ToJson(data, true);
            Debug.Log(json);

            PlayerPrefs.SetString("PlayerUserData", json);
            PlayerPrefs.Save();
        }
        */

        /*
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (PlayerPrefs.HasKey("PlayerUserData"))
            {
                string json = PlayerPrefs.GetString("PlayerUserData");
                UserData data = JsonUtility.FromJson<UserData>(json);
                transform.position = data.position;
            }
            else
            {
                Debug.Log("PlayerUserDataが存在しません");
            }
        }
        */
    }

    public void SaveData()
    {
        UserData data = new UserData()
        {
            position = transform.position
        };
        string json = JsonUtility.ToJson(data, true);
        Debug.Log(json);

        PlayerPrefs.SetString("PlayerUserData", json);
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("PlayerUserData"))
        {
            string json = PlayerPrefs.GetString("PlayerUserData");
            UserData data = JsonUtility.FromJson<UserData>(json);
            transform.position = data.position;
        }
        else
        {
            Debug.Log("PlayerUserDataが存在しません");
        }
    }
}