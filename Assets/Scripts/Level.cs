using UnityEngine;

public class Level : MonoBehaviour
{
    public int BlocksCount
    {
        get
        {
            if (blocksCount == 0)
            {
                blocksCount = GetComponentsInChildren<Block>(true).Length;
            }

            return blocksCount;
        }
    }

    private int blocksCount;
}