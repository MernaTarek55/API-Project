using UnityEngine;
using UnityEngine.AI;
public class GirlRobotAI : MonoBehaviour
{
    public float attackRange = 2f;
    public float moveSpeed = 3.5f;
    public Animator animator; 
    private Transform target; 
    private NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    public void PerformAction(string action, string enemyName)
    {
        target = FindEnemy(enemyName);

        if (target != null)
        {
            StartCoroutine(GoToEnemyAndAttack(action));
        }
        else
        {
            Debug.LogWarning("Enemy not found: " + enemyName);
        }
    }

    Transform FindEnemy(string enemyName)
    {
        GameObject enemy = GameObject.FindGameObjectWithTag(enemyName);
        return enemy ? enemy.transform : null;
    }

    System.Collections.IEnumerator GoToEnemyAndAttack(string action)
    {
        while (Vector3.Distance(transform.position, target.position) >= attackRange)
        {
            agent.SetDestination(target.position);
            animator.SetBool("walkFlag", true);

            Debug.Log($"Moving towards {target.name} - Distance: {Vector3.Distance(transform.position, target.position)}");

            yield return null;
        }

        agent.ResetPath();
        animator.SetBool("walkFlag", false);

        yield return new WaitForSeconds(0.5f);

        animator.SetTrigger(action);

        Debug.Log($"Robot performed {action} on {target.name}!");
    }

}
