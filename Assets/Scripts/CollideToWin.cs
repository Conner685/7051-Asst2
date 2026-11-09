using UnityEngine;

public class CollideToWin : MonoBehaviour
{
    [SerializeField] GameObject winUI;
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Finish")
        {
            winUI.SetActive(true);
        }
    }
}
