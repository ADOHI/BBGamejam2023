using BBGamejam.Global.Ingame;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace RabbitResurrection
{
    public class EnemyManager : MonoBehaviour
    {
        private GameObject target;
        public float accumulatedTime;
        private Coroutine spawnCoroutine;
        private bool isTwice = false;

        private void Start()
        {
            Init();
            accumulatedTime = 0f;
        }

        private void Update()
        {
            accumulatedTime += Time.deltaTime * Time.timeScale;
        }

        private void Init()
        {
            spawnCoroutine = StartCoroutine(EnemySpawnRoutine());
        }

        public void SetData(GameObject target)
        {
            this.target = target;
        }

        public void SpawnEnemy(int index)
        {
            Enemy enemy = null;
            switch (index)
            {
                case 0:
                    enemy = Managers.Resource.Instantiate<Enemy>("Prefabs/Monster01");
                    break;
                case 1:
                    enemy = Managers.Resource.Instantiate<Enemy>("Prefabs/Monster02");
                    break;
                case 2:
                    enemy = Managers.Resource.Instantiate<Enemy>("Prefabs/Monster03");
                    break;
                case 3:
                    enemy = Managers.Resource.Instantiate<Enemy>("Prefabs/Monster04");
                    break;
                default:
                    break;
            }
            float x = Random.Range(-1f, 1f);
            enemy.gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(x, 1f, 20f));
            var position = enemy.gameObject.transform.position;
            position.z = 0f;
            enemy.gameObject.transform.position = position;
            enemy.SetData(target);
        }

        private IEnumerator EnemySpawnRoutine()
        {
            while (IngameManager.Instance.progress < 1f)
            {
                yield return new WaitForSeconds(1f);
                SpawnEnemyByCase();
            }

            yield break;
        }

        private void SpawnEnemyByCase()
        {
            if (IngameManager.Instance.progress < (1f/12f)) // ���� 1
            {
                SpawnEnemy(0);
            }
            else if (IngameManager.Instance.progress < (2f / 12f)) // ���� 2
            {
                SpawnEnemy(0);
                
                if (isTwice)
                {
                    SpawnEnemy(0);
                    isTwice = false;
                }
                else
                {
                    isTwice = true;
                }
            }
            else if (IngameManager.Instance.progress < (3f / 12f)) // ���� 3
            {
                SpawnEnemy(1);

                if (isTwice)
                {
                    SpawnEnemy(1);
                    isTwice = false;
                }
                else
                {
                    isTwice = true;
                }
            }
            else if (IngameManager.Instance.progress < (4f / 12f)) // ���� 4
            {
                SpawnEnemy(0);
                SpawnEnemy(1);

                if (isTwice)
                {
                    SpawnEnemy(1);
                    isTwice = false;
                }
                else
                {
                    isTwice = true;
                }
            }
            else if (IngameManager.Instance.progress < (5f / 12f)) // ���� 4
            {
                SpawnEnemy(0);
                SpawnEnemy(0);
                SpawnEnemy(1);
                SpawnEnemy(1);
            }
            else if (IngameManager.Instance.progress < (6f / 12f)) // ���� 5
            {
                SpawnEnemy(0);
                SpawnEnemy(0);
                SpawnEnemy(1);
                SpawnEnemy(1);

                if (isTwice)
                {
                    SpawnEnemy(0);
                    isTwice = false;
                }
                else
                {
                    SpawnEnemy(1);
                    isTwice = true;
                }
            }
            else if (IngameManager.Instance.progress < (7f / 12f)) // ��� ���� 1
            {
                SpawnEnemy(0);
                SpawnEnemy(1);
                SpawnEnemy(1);

                if (isTwice)
                {
                    SpawnEnemy(2);
                    isTwice = false;
                }
                else
                {
                    isTwice = true;
                }
            }
            else if (IngameManager.Instance.progress < (8f / 12f)) // ��� ���� 2
            {
                SpawnEnemy(0);
                SpawnEnemy(1);
                SpawnEnemy(2);
                SpawnEnemy(2);
            }
            else if (IngameManager.Instance.progress < (9f / 12f)) // ��� ���� 3
            {
                SpawnEnemy(0);
                SpawnEnemy(1);
                SpawnEnemy(2);
                SpawnEnemy(3);
            }
            else if (IngameManager.Instance.progress < (10f / 12f)) // ��� ���� 4
            {
                SpawnEnemy(0);
                SpawnEnemy(1);
                SpawnEnemy(1);
                SpawnEnemy(1);

                SpawnEnemy(2);
                SpawnEnemy(2);
                SpawnEnemy(3);
                SpawnEnemy(3);
            }
            else if (IngameManager.Instance.progress < (11f / 12f)) // ��� ���� 5
            {
                SpawnEnemy(0);
                SpawnEnemy(0);
                SpawnEnemy(1);
                SpawnEnemy(1);

                SpawnEnemy(1);
                SpawnEnemy(2);
                SpawnEnemy(2);
                SpawnEnemy(3);

                if (isTwice)
                {
                    SpawnEnemy(2);
                    isTwice = false;
                }
                else
                {
                    SpawnEnemy(3);
                    isTwice = true;
                }

            }
            else // if (IngameManager.Instance.progress < (12f / 12f)) // ��� ���� 6
            {
                SpawnEnemy(0);
                SpawnEnemy(0);
                SpawnEnemy(1);
                SpawnEnemy(1);
                SpawnEnemy(1);

                SpawnEnemy(2);
                SpawnEnemy(2);
                SpawnEnemy(2);
                SpawnEnemy(3);
                SpawnEnemy(3);
            }
            //else
            //{
            //    Debug.Log("Game ��");
            //}
        }
    }
}