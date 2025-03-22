using UnityEngine;

public class GirlRobotAI : MonoBehaviour
{
    public float attackRange = 2f;
    public float moveSpeed = 3.5f;
    public Animator animator;

    private Transform target;
    private string currentAction;

    void Update()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > attackRange)
            {
                MoveTowardsTarget();
                animator.SetBool("walkFlag", true);
            }
            else
            {
                animator.SetBool("walkFlag", false);

                if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
                {
                    animator.SetTrigger(currentAction);
                    Debug.Log($"Robot performed {currentAction} on {target.name}!");
                    target = null; // Reset after attacking
                }
            }
            FaceTarget();
        }
    }

    public void PerformAction(string action, string enemyName)
    {
        target = FindEnemy(enemyName);
        currentAction = action;

        if (target == null)
        {
            Debug.LogWarning("Enemy not found: " + enemyName);
        }
    }

    Transform FindEnemy(string enemyName)
    {
        GameObject enemy = GameObject.Find(enemyName); // Using name instead of tag
        return enemy ? enemy.transform : null;
    }

    void MoveTowardsTarget()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    void FaceTarget()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
