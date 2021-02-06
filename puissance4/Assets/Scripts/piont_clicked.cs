using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class piont_clicked : MonoBehaviour
{
    private spawn_pionts pionts;
    private datas data;
    void Start()
    {
        pionts = GameObject.FindGameObjectWithTag("Board").GetComponent<spawn_pionts>();
        data = GameObject.FindGameObjectWithTag("Board").GetComponent<datas>();
    }
    public void clicked_piont()
    {
        int index1 = 0;
        int index2 = -1;

        if (data.current_turn != data.player_turn || data.winner != 0)
            return;
        for (int i = 0; i < 7; i++) {
            if (this.transform.position.x == pionts.pionts[i][0].transform.position.x)
                index1 = i;
        }
        for (int i = 0; i < 6; i++) {
            Color temp = pionts.pionts[index1][i].transform.GetComponent<Image>().color;
            if (temp != data.color_1 && temp != data.color_2)
                index2 = i;
        }
        if (index2 != -1) {
            pionts.pionts[index1][index2].transform.GetComponent<Image>().color = data.color_1;
            data.winner = data.is_a_winner(index1, index2, data.color_1);
            if (data.winner == 1) {
                data.win_text.text = "Player " + (data.current_turn+1).ToString() + " Win";
                data.restart_button.SetActive(true);
            }
            data.current_turn = (data.current_turn + 1) % 2;
            data.piont_placed++;
        }
    }
}
