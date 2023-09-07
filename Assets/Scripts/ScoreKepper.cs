using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKepper : MonoBehaviour
{
    public int Blue_team = 0, Red_team = 0, Blue_self_goals = 0, Red_self_goals = 0;
    public Text TextScoreText;
    // Start is called before the first frame update
    void Start()
    {
        ScoreUpdate(0,0,false);
    }

    // Update is called once per frame
    public void ScoreUpdate(int blue, int red, bool self_goal)
    {
        if(blue > 0){
            Blue_team++;
            if(self_goal){Blue_self_goals++;}
        }
        if(red > 0){
            Red_team++;
            if(self_goal){Red_self_goals++;}
        }
        TextScoreText.text = "Blue : " +Blue_team+ "(self goals :"+Blue_self_goals+")\nRed : " + Red_team +"(self goals :"+Red_self_goals+")";
    }
}
