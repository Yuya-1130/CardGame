using UnityEngine;
using UnityEngine.EventSystems;

public class DropCard : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Object Dropped on me");

        // eventData.pointerDrag には、ドラッグされていたオブジェクトが入っている
        GameObject draggedObject = eventData.pointerDrag;

        if (draggedObject != null)
        {
            // ドロップされたオブジェクトの親を自分（ドロップエリア）にする
            // これにより、UIの整列機能（LayoutGroupなど）が効くようになる
            draggedObject.transform.SetParent(transform);

            // 位置を中央に合わせる
            draggedObject.GetComponent<RectTransform>().localPosition = Vector3.zero;

            Debug.Log($"{draggedObject.name} を受け入れました");
        }
    }
}
