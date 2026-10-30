using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{

    public float damage = 25f;
    public int startAmmoRemain = 80;
    public int magCapacity = 25;
    public float timeBetFire = 0.12f;
}