using UnityEngine;

/// <summary>
/// �ѱ� ������
/// </summary>
[CreateAssetMenu(menuName = "Scriptable/GunData", fileName = "Gun Data")]
public class GunData : ScriptableObject
{
    /// <summary>
    /// �߻� �Ҹ�
    /// </summary>
    public AudioClip shotClip;

    /// <summary>
    /// ������ �Ҹ�
    /// </summary>
    public AudioClip reloadClip;

    /// <summary>
    /// ���ݷ�
    /// </summary>
    public float damage = 25;

    /// <summary>
    /// ó���� �־��� ��ü �Ѿ�
    /// </summary>
    public int startAmmoRemain = 100;

    /// <summary>
    /// źâ �뷮
    /// </summary>
    public int magCapacity = 25;

    /// <summary>
    /// �Ѿ� �߻� ����
    /// </summary>
    public float timeBetFire = 0.12f;

    /// <summary>
    /// ������ �ð�
    /// </summary>
    public float reloadTime = 1.8f;
}
