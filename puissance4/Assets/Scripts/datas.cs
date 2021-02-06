using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class datas : MonoBehaviour
{
    public int current_turn = 0;
    public int player_turn = 1;
    public int winner = 0;
    public int piont_placed = 0;
    public Color color_1 = new Color(1,0,0,1);
    public Color color_2 = new Color(1,1,0,1);
    public Text win_text = null;
    public GameObject restart_button = null;
    private spawn_pionts pionts;

    private void Awake() {
        winner = 0;
        win_text.text = "";
        piont_placed = 0;
        restart_button.SetActive(false);
        player_turn = Random.Range(0, 2);
    }

    private void Start() {
        pionts = GameObject.FindGameObjectWithTag("Board").GetComponent<spawn_pionts>();
    }

    private void Update() {
        if (piont_placed >= 42) {
            win_text.text = "Nobody Wins ?!";
            restart_button.SetActive(true);
        }
        if (current_turn == player_turn || winner != 0 || piont_placed >= 42)
            return;
        int nb_pos = 0;
        int[] possibilities;
        int index1 = 0;
        int index2 = 0;

        for (int i = 0; i < 7; i++) {
            Color temp = pionts.pionts[i][0].transform.GetComponent<Image>().color;
            if (temp != color_1 && temp != color_2)
                nb_pos++;
        }
        possibilities = new int[nb_pos];
        for (int i = 0; i < 7; i++) {
            Color temp = pionts.pionts[i][0].transform.GetComponent<Image>().color;
            if (temp != color_1 && temp != color_2) {
                possibilities[index1] = i;
                index1++;
            }
        }
        index1 = possibilities[Random.Range(0, nb_pos)];
        for (int i = 0; i < 6; i++) {
            Color temp = pionts.pionts[index1][i].transform.GetComponent<Image>().color;
            if (temp != color_1 && temp != color_2)
                index2 = i;
        }
        pionts.pionts[index1][index2].transform.GetComponent<Image>().color = color_2;
        piont_placed++;
        winner = is_a_winner(index1, index2, color_2);
        if (winner == 1) {
            win_text.text = "Player " + (current_turn+1).ToString() + " Win";
            restart_button.SetActive(true);
        }
        current_turn = (current_turn + 1) % 2;
    }

    public int is_a_winner(int index1, int index2, Color color)
    {
        Color temp = color;
        int count = 0;

        for (int i = 1; temp == color && index1+i <= 7; i++) {
            if (index1+i < 7)
                temp = pionts.pionts[index1+i][index2].transform.GetComponent<Image>().color;
            count++;
        }
        temp = color;
        count--;
        for (int i = 1; temp == color && index1-i >= -1; i++) {
            if (index1-i >= 0)
                temp = pionts.pionts[index1-i][index2].transform.GetComponent<Image>().color;
            count++;
        }
        if (count >= 4)
            return (1);

        temp = color;
        count = 0;
        for (int i = 1; temp == color && index2+i <= 6; i++) {
            if (index2+i < 6)
                temp = pionts.pionts[index1][index2+i].transform.GetComponent<Image>().color;
            count++;
        }
        if (count >= 4)
            return (1);

        temp = color;
        count = 0;
        for (int i = 1; temp == color && index1+i <= 7 && index2+i <= 6; i++) {
            if (index1+i < 7 && index2+i < 6)
                temp = pionts.pionts[index1+i][index2+i].transform.GetComponent<Image>().color;
            count++;
        }
        temp = color;
        count--;
        for (int i = 1; temp == color && index1-i >= -1 && index2-i >= -1; i++) {
            if (index1-i >= 0 && index2-i >= 0)
                temp = pionts.pionts[index1-i][index2-i].transform.GetComponent<Image>().color;
            count++;
        }
        if (count >= 4)
            return (1);

        temp = color;
        count = 0;
        for (int i = 1; temp == color && index1+i <= 7 && index2-i >= -1; i++) {
            if (index1+i < 7 && index2-i >= 0)
                temp = pionts.pionts[index1+i][index2-i].transform.GetComponent<Image>().color;
            count++;
        }
        temp = color;
        count--;
        for (int i = 1; temp == color && index1-i >= -1 && index2+i <= 6; i++) {
            if (index1-i >= 0 && index2+i < 6)
                temp = pionts.pionts[index1-i][index2+i].transform.GetComponent<Image>().color;
            count++;
        }
        if (count >= 4)
            return (1);
        return (0);
    }

    public void restart()
    {
        Awake();
        pionts.Awake();
    }
}
