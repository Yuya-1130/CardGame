using UnityEngine;
using UnityEngine.EventSystems;

public class HandSort : MonoBehaviour
{
    [Header("配置の設定")]
  [SerializeField]  public float angleDifference = 8f;

    [Tooltip("扇の基準点")]
    public float radius = 250f;

     public void Update()
    {
        // いつでもInspectorで値をいじれるように(のちに削除)
        ArrangeCards();    
    }
    public void ArrangeCards()
    {
        int cardCount = transform.childCount;
        if (cardCount == 0) return;

        // 手札全体が中央を向くように、開始角度を計算する
        // 例：3枚の場合、-(3-1)*8 / 2 = -8度 からスタートして、-8度、0度、8度 と並べる
        float startAngle = -(cardCount - 1) * angleDifference / 2f;

        for (int i = 0; i < cardCount; i++)
        {
            Transform card = transform.GetChild(i);
            float currentAngle = startAngle + (i * angleDifference);

            // 1. 回転を適用（Z軸を回転させる）
            card.localRotation = Quaternion.Euler(0, 0, currentAngle);

            // 2. 位置を計算（回転させた方向に、半径分の距離だけ押し出す）
            // 三角関数を使って、角度からローカル座標(X, Y)を求める
            float rad = (currentAngle + 90f) * Mathf.Deg2Rad; // 90度足して上方向を基準にする
            float x = Mathf.Cos(rad) * radius;
            float y = Mathf.Sin(rad) * radius;

            // 基準点（HandManagerの位置）から少し下に下げた位置をベースにする
            card.localPosition = new Vector3(x, y - radius, 0);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedCard = eventData.pointerDrag;

        if (draggedCard != null)
        {
            // カードの親を自分の下（HandSort）にする
            draggedCard.transform.SetParent(transform);
            draggedCard.transform.localScale = Vector3.one; // スケールリセット

            // 綺麗に扇形に並べ直す
            ArrangeCards();
        }
    }
}
