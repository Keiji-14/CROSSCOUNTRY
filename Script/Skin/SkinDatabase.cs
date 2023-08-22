using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒXƒLƒ“‚Ìî•ñ‚ğ•Û‚·‚éScriptableObject
/// </summary>
[CreateAssetMenu(fileName = "SkinDatabase", menuName = "Create Skin Database")]
public class SkinDatabase : ScriptableObject
{
    public List<SkinData> skins = new List<SkinData>();
}