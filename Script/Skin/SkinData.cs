using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�L���̏����Ǘ�����N���X
/// </summary>
[System.Serializable]
public class SkinData
{
    public int skinID;
    public string japaneseSkinName;
    public string englishSkinName;
    public Sprite icon;
    public GameObject gamePrefab;
    public GameObject titlePrefab;
    public SkinStageType skinStageType;
    public int duplicationCount;
}
 
// �X�L���̃X�e�[�W�^�C�v�Ɏ���ŃX�e�[�W���؂�ւ���
public enum SkinStageType
{
    Forest,
    Snow,
    Desert,
}