using UI;

namespace Gameplay.AI.EnemyType
{
    public class BasicEnemy : EnemyController
    {
        protected override void TakeDamage()
        {
            PlayableInterfaceController.AddScore(score);
            Destroy(gameObject);
        }
    }
}
