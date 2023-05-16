using UnityEngine;

/// <summary>
/// 총기 데이터
/// </summary>
[CreateAssetMenu(menuName = "Scriptable/GunData", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    /// <summary>
    /// 발사 소리
    /// </summary>
    public AudioClip shotClip;

    /// <summary>
    /// 재장전 소리
    /// </summary>
    public AudioClip reloadClip;

    /// <summary>
    /// 공격력
    /// </summary>
    public float damage = 25;

    /// <summary>
    /// 처음에 주어질 전체 총알
    /// </summary>
    public int startAmmoRemain = 100;

    /// <summary>
    /// 탄창 용량
    /// </summary>
    public int magCapacity = 25;

    /// <summary>
    /// 총알 발사 간격
    /// </summary>
    public float timeBetFire = 0.12f;

    /// <summary>
    /// 재장전 시간
    /// </summary>
    public float reloadTime = 1.8f;
}
