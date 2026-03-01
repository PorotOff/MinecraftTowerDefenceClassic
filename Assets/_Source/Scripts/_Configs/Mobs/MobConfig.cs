using UnityEngine;

[CreateAssetMenu(fileName = "MobConfig", menuName = "Configs/Mobs/MobConfig", order = 0)]
public class MobConfig : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; }
}