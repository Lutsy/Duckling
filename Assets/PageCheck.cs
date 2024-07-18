using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageCheck : MonoBehaviour
{

    public Book book;
    public GameObject buttonObj;

    public void PageFlip()
    {
        if(book.currentPage >= 8)
        {
            buttonObj.SetActive(true);
        }
        else
        {
            buttonObj.SetActive(false);
        }
    }
}
