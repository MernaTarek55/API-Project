using UnityEngine;

public class AttackFunctions {
    public static string AttackEnemy1()
    {
        Debug.Log("Attacking Enemy1!");
        return "Attack Enemy1";
    }

    public static string KickEnemy1()
    {
        Debug.Log("Kicking Enemy1!");
        return "Kick Enemy1";
    }

    public static string ShootEnemy1()
    {
        Debug.Log("Shooting Enemy1!");
        return "Shoot Enemy1";
    }

    public static string AttackEnemy2()
    {
        Debug.Log("Attacking Enemy2!");
        return "Attack Enemy2";
    }

    public static string KickEnemy2()
    {
        Debug.Log("Kicking Enemy2!");
        return "Kick Enemy2";
    }

    public static string ShootEnemy2()
    {
        Debug.Log("Shooting Enemy2!");
        return "Shoot Enemy2";
    }

    public static string NoAction()
    {
        Debug.Log("No valid action found.");
        return "";
    }
}
