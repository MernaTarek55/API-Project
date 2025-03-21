using UnityEngine;

public class GameCommandProcessor : MonoBehaviour
{
    [SerializeField]private GirlRobotAI girlRobot;

    

    public void ProcessCommand(string command)
    {
        string action = "";
        string enemyName = "";

        // Check for known commands
        if (command.Contains("Attack Enemy1"))
        {
            action = "Attack";
            enemyName = "Enemy1";
            Debug.Log("aaaaaaaaaaaaaaaa");
        }
        else if (command.Contains("Kick Enemy1"))
        {
            action = "Kick";
            enemyName = "Enemy1";
        }
        else if (command.Contains("Shoot Enemy1"))
        {
            action = "Shoot";
            enemyName = "Enemy1";
        }
        else if (command.Contains("Attack Enemy2"))
        {
            action = "Attack";
            enemyName = "Enemy2";
        }
        else if (command.Contains("Kick Enemy2"))
        {
            action = "Kick";
            enemyName = "Enemy2";
        }
        else if (command.Contains("Shoot Enemy2"))
        {
            action = "Shoot";
            enemyName = "Enemy2";
        }

        // Execute the action if valid
        if (!string.IsNullOrEmpty(action) && !string.IsNullOrEmpty(enemyName))
        {
            girlRobot.PerformAction(action, enemyName);
        }
        else
        {
            Debug.LogWarning("Invalid command: " + command);
        }
    }
}
