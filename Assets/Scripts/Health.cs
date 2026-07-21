using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("ステータス設定")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    // 死亡時やダメージ時に他スクリプトから処理を呼び出せるイベント
    [Header("イベント設定")]
    public UnityEvent<int, int> onHealthChanged; // (現在のHP, 最大HP) を通知
    public UnityEvent onDeath;                   // 死亡時に通知

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        // 初期状態をUIなどに通知
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    /// <summary>
    /// ダメージを与える
    /// </summary>
    public void TakeDamage(int amount)
    {
        if (IsDead) return;

        currentHealth = Mathf.Max(currentHealth - amount, 0);
        onHealthChanged?.Invoke(currentHealth, maxHealth);

        Debug.Log($"{gameObject.name} は {amount} のダメージを受けた！（残HP: {currentHealth}）");

        if (currentHealth <= 0)
        { organizeDeath(); }
    }

    /// <summary>
    /// 回復する
    /// </summary>
    public void Heal(int amount)
    {
        if (IsDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private void organizeDeath()
    {
        Debug.Log($"{gameObject.name} は倒れた！");
        onDeath?.Invoke();
    }
}