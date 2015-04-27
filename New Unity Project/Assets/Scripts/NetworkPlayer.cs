using UnityEngine;
using System.Collections;

public class NetworkPlayer : MonoBehaviour
{
    float lifepoints = 20f;
    float cutCooldown = 0;
    float speedCooldown = 0;
    float jumpCooldown = 0;

    CharacterMotor cm;
    float defaultSpeed;
    float defaultJump;

    void Start()
    {
        cm = gameObject.GetComponentInChildren<CharacterMotor>();
        defaultSpeed = cm.movement.maxForwardRunSpeed;
        defaultJump = cm.jumping.baseHeight;
    }

    void Update()
    {
        if (cutCooldown > 0)
        {
            lifepoints -= Time.deltaTime;
            cutCooldown -= Time.deltaTime;
        }
        if (speedCooldown > 0)
        {
            speedCooldown -= Time.deltaTime;
            if (speedCooldown < 0)
                cm.movement.maxForwardRunSpeed = defaultSpeed;
        }
        if (jumpCooldown > 0)
        {
            jumpCooldown -= Time.deltaTime;
            if (jumpCooldown < 0)
                cm.jumping.baseHeight = defaultJump;
        }
    }

    public void Cut()
    {
        cutCooldown = 30f;
    }

    public void SpeedBonus()
    {
        speedCooldown = 30f;
        cm.movement.maxForwardRunSpeed = defaultSpeed * 2;
    }

    public void JumpBonus()
    {
        jumpCooldown = 30f;
        cm.jumping.baseHeight = defaultJump * 10;
    }

    public void HealthBonus(int health)
    {
        lifepoints = Mathf.Clamp(lifepoints + health, 0f, 20f);
    }
}
