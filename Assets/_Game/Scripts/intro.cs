using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro : MonoBehaviour
{
    public List<GameObject> images;
    private int currentIndex = 0;

    void Start()
    {
        StartCoroutine(ChangeImageAfterDelay(4f));
    }

    private IEnumerator ChangeImageAfterDelay(float delay)
    {
        for (int i = 0; i < images.Count - 1; i++)
        {
            yield return new WaitForSeconds(delay);

            // Tắt hiển thị của đối tượng hiện tại

            images[currentIndex].SetActive(false);
            // Tăng chỉ số và kiểm tra điều kiện vượt quá kích thước của danh sách
            currentIndex++;
            if (currentIndex >= images.Count)
            {
                currentIndex = 0;
            }

            // Hiển thị đối tượng tiếp theo
            images[currentIndex].SetActive(true);
        }
        if (currentIndex == images.Count - 1)
        {
            Destroy(gameObject, 3f);
        }
    }
}
