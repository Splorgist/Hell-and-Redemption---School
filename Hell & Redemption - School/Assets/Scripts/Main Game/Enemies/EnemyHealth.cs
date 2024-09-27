using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private bool _damageable = true;
    [SerializeField] public int _healthAmount = 100;
    [SerializeField] public float _invulnerabilityTime = 0.2f;

    private bool _hit;
    private int _currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _healthAmount;
    }

    public void Damage(int amount)
    {
        if (_damageable && !_hit && _currentHealth > 0){
            _hit = true;
            _currentHealth -= amount;

            if (_currentHealth <= 0){
                _currentHealth = 0;
                gameObject.SetActive(false);
            }

            else{
                StartCoroutine(TurnOffHit());
            }
        }
    }

    private IEnumerator TurnOffHit()
    {
        yield return new WaitForSeconds(_invulnerabilityTime);
        _hit = false;
    }
}
