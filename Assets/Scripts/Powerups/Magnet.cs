using UnityEngine;

[CreateAssetMenu(fileName = "Magnet", menuName = "Powerup/Magnet")]
public class Magnet : Powerup
{
    [SerializeField]
    private PowerupStats range;
    public float GetRange()
    {
        return range.GetValue(currentLevel);
    }

    [SerializeField]
    private PowerupStats speed;
    public float GetSpeed()
    {
        return speed.GetValue(currentLevel);
    }
}
