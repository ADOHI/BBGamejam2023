using UnityEngine;

namespace RabbitResurrection
{
    public class EnemyManager : MonoBehaviour
    {
        public void SpawnEnemy(GameObject target)
        {
            Enemy enemy = Managers.Resource.Instantiate<Enemy>("Prefabs/Enemy", transform);
            float x = Random.Range(0f, 1f);
            float y = Random.Range(0f, 1f);
            enemy.gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(-1f - x, 0.5f + y, 20f));
            var position = enemy.gameObject.transform.position;
            position.z = 0f;
            enemy.gameObject.transform.position = position;
            enemy.SetData(target);
            //_ = StartCoroutine(enemy.StartSwim());
        }
    }
}