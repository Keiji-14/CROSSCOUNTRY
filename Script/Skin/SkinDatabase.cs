using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�L���̏���ێ�����ScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "SkinDatabase", menuName = "Create Skin Database")]
public class SkinDatabase : ScriptableObject
{
    public List<SkinData> skins = new List<SkinData>();
}