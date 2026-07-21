using UnityEngine;
using UnityEngine.EventSystems;

public class DropArea : MonoBehaviour
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("【テスト】FieldAreaにドロップされました！");

        // ドラッグされているカードオブジェクトを取得
        GameObject draggedCard = eventData.pointerDrag;

        if (draggedCard != null)
        {
            // ★追加：CardMove を取得して「ドロップ成功」を伝える
            CardMove cardMove = draggedCard.GetComponent<CardMove>();
            if (cardMove != null)
            {
                cardMove.isDropped = true; // フラグをオンにする
            }

            // 親を「場」に変更
            draggedCard.transform.SetParent(transform);

            // 位置・回転・スケールをリセット
            draggedCard.transform.localPosition = Vector3.zero;
            draggedCard.transform.localRotation = Quaternion.identity;
            UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
        }
    
    }
}
