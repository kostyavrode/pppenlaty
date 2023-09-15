using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int currestScore;
    private int bid;
    private void Awake()
    {
        instance = this;
        bid = 10;
    }
    public void GoalScored()
    {
        currestScore++;
        CheckStage();
    }
    public void GoalNotScored()
    {
        ResetScore();
        ball.instance.ResetBall();
    }
    public void ResetScore()
    {
        currestScore = 0;
    }
    private void CheckStage()
    {
        if (currestScore==5)
        {
            StageComplete();
        }
    }
    public void RaiseBid()
    {
        bid += 5;
    }
    public void DegraiseBid()
    {
        if (bid>5)
        {
            bid -= 5;
        }
    }
    public int GetStage()
    {
        return currestScore;
    }
    public int GetCurrentScore()
    {
        return currestScore;
    }
    public void TakeBid()
    {
        Player.instance.RaiseMoney(GetCurrentWin());
        ResetScore();
    }
    public int GetCurrentWin()
    {
        int winning=0;
        if (currestScore==0)
        {
            winning = 0;
        }
        if (currestScore == 1)
        {
            winning = bid*2;
        }
        if (currestScore == 2)
        {
            winning = bid * 4;
        }
        if (currestScore == 3)
        {
            winning = bid * 8;
        }
        if (currestScore == 4)
        {
            winning = bid * 16;
        }
        if (currestScore == 5)
        {
            winning = bid * 32;
        }
        return winning;
    }
    public void SetBid(int tempbid)
    {
        if (currestScore==0)
        {
            bid = tempbid;
        }
    }
    public int GetCurrentBid()
    {
        return bid;
    }
    public void BidBeforeKick()
    {
        Player.instance.MinusMoney(bid);
    }
    private void StageComplete()
    {
        Player.instance.RaiseMoney(GetCurrentWin());
        ResetScore();
    }
}
