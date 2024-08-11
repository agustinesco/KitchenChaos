using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private Player player;
    private float foostepsTimer;
    private float foostepsTimerMax = .1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.IsWalking())
        {
            foostepsTimer += Time.deltaTime;

            if (foostepsTimer > foostepsTimerMax)
            {
                foostepsTimer = 0f;

                SoundManager.Instance.PlayFootStepsSound(player.transform.position);
            }

        }
        else
        {
            foostepsTimer = 0f;
        }
    }
}
