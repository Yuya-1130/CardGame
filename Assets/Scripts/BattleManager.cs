using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("対象の設定")]
    [SerializeField] private Health playerHealth;
    [SerializeField] private Health enemyHealth;

    private void OnEnable()
    {
        // 死亡イベントの購読
        if (playerHealth != null) playerHealth.onDeath.AddListener(OnPlayerDead);
        if (enemyHealth != null) enemyHealth.onDeath.AddListener(OnEnemyDead);
    }

    private void OnDisable()
    {
        // イベント解除
        if (playerHealth != null) playerHealth.onDeath.RemoveListener(OnPlayerDead);
        if (enemyHealth != null) enemyHealth.onDeath.RemoveListener(OnEnemyDead);
    }

    // プレイヤー死亡時の処理
    private void OnPlayerDead()
    {
        Debug.Log("GAME OVER... プレイヤーが敗北しました。");
        // ここにゲームオーバー画面の表示などを記述
    }

    // 敵死亡時の処理
    private void OnEnemyDead()
    {
        Debug.Log("VICTORY! 敵を倒しました！");
        // ここに勝利演出や撃破処理などを記述
        Destroy(enemyHealth.gameObject, 1.0f); // 1秒後に敵オブジェクトを削除
    }

    // テスト用（キー入力で攻撃してみる）
    private void Update()
    {
        // [A] キーで敵に20ダメージ
        if (Input.GetKeyDown(KeyCode.A))
        {
            enemyHealth.TakeDamage(20);
        }

        // [S] キーでプレイヤーに15ダメージ
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerHealth.TakeDamage(15);
        }
    }
}