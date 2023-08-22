using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�L���̏�������ێ�����ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "ObtainedSkinDatabase", menuName = "Create Obtained Skin Database")]
public class PossessionSkinDatabase : ScriptableObject
{
    public List<SkinData> possSkins = new List<SkinData>();

    // �������Ă��邩�m�F����֐�
    public SkinData FindSkin(int skinID)
    {
        return possSkins.Find(skin => skin.skinID == skinID);
    }

    // �V�����X�L������肵�����ɒǉ��ۑ�����֐�
    public void AddObtainedSkin(SkinData skin)
    {
        possSkins.Add(skin);
    }

    // �����X�L���f�[�^���폜����(����N�����Ɏg�p)
    public void ClearPossessionSkins()
    {
        possSkins.Clear();
    }

    // �����X�L���f�[�^��ۑ�
    public void SavePossessionSkins()
    {
        // possSkins���X�g�̃f�[�^��Json�`���ŕۑ�
        string jsonData = JsonUtility.ToJson(this);
        Debug.Log(jsonData);
        PlayerPrefs.SetString("PossessionSkins", jsonData);
        PlayerPrefs.Save();
    }

    // �����X�L���f�[�^�𕜌�
    public void LoadPossessionSkins()
    {
        if (PlayerPrefs.HasKey("PossessionSkins"))
        {
            // �ۑ����ꂽ�f�[�^�𕜌�
            string jsonData = PlayerPrefs.GetString("PossessionSkins");
            Debug.Log(jsonData);
            JsonUtility.FromJsonOverwrite(jsonData, this);
        }
    }
}