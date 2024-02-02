using System.Collections;
using TMPro;
using UnityEngine;

public class ElevatorFloorCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI floorDisplayText;

    private readonly WaitForSeconds singleFloorDelay = new(0.5f);
    private int floorCount = 0;

    void Awake()
    {
        StartCoroutine(FloorsUp());
    }

    private IEnumerator FloorsUp()
    {
        while(true)
        {
            yield return singleFloorDelay;
            floorCount++;
            floorDisplayText.text = string.Format("↑ {0}", floorCount);
        }
    }
}
