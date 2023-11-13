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
        while (true)
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
    }
}
