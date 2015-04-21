using UnityEngine;
using System.Collections;

public class NetworkPlayer : MonoBehaviour
{
    float lifepoints = 20f;
    float cutCooldown = 30f;
    float speedCooldown = 30f;
    float jumpCooldown = 30f;

    bool cut = false;
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
        if (cut)
        {
            lifepoints -= Time.deltaTime;
            cutCooldown -= Time.deltaTime;
            if (cutCooldown < 0)
                cut = false;
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

    void Cut()
    {
        cutCooldown = 30f;
        cut = true;
    }

    void SpeedBonus()
    {
        speedCooldown = 30f;
        cm.movement.maxForwardRunSpeed = defaultSpeed * 2;
    }

    void JumpBonus()
    {
        jumpCooldown = 30f;
        cm.jumping.baseHeight = defaultJump * 2;
    }

    public void HealthBonus(int health)
    {
        lifepoints = Mathf.Clamp(lifepoints + health, 0f, 20f);
        print(lifepoints);
    }
}
