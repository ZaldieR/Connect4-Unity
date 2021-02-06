using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawn_pionts : MonoBehaviour
{
    [SerializeField] private GameObject piont = null;
    [HideInInspector] public GameObject[][] pionts;

    public void Awake()
    {
        Vector3 position;

        pionts = new GameObject[7][];
        for (int i = 0; i < 7; i++)
            pionts[i] = new GameObject[6];
        position.z = 0;
        for (int i = 0; i < 7; i++) {
            position.x = i * 143 + Screen.currentResolution.width / 12;
            for (int j = 0; j < 6; j++) {
                position.y = Screen.currentResolution.height + Screen.currentResolution.height / 3 +
                Screen.currentResolution.height / 2 - 145 * j;
                pionts[i][j] = Instantiate(piont, position, this.transform.rotation, this.transform);
            }
        }
    }
}
