using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.magnet.isActive) return;

        if(Vector3.Distance(transform.position, _player.position) < GameManager.instance.magnet.GetRange())
        {
            var direction = (_player.position - transform.position).normalized;

            transform.position += direction * GameManager.instance.magnet.GetSpeed();
        }
    }
}
