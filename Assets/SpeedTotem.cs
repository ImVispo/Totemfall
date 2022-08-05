using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedTotem : Totem
{
    [SerializeField] private int _speedBonus;

    protected override void UnitEntered(GameObject gameObject)
    {
        if (gameObject.tag == "Player")
        {
            updateSpeedBonus(gameObject.GetComponent<Player>(), _speedBonus);
        }
    }

    protected override void UnitExit(GameObject gameObject)
    {
        if (gameObject.tag == "Player")
        {
            updateSpeedBonus(gameObject.GetComponent<Player>(), (-_speedBonus));
        }
    }

    private void updateSpeedBonus(Player player, int speedBonus)
    {
        player.Speed += speedBonus;
    }
}

