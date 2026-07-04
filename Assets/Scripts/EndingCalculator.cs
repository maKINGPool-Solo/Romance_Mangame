using UnityEngine;

public static class EndingData
{
    public static string ResultEndingId;
}

public class EndingCalculator : MonoBehaviour
{
    void OnEnable()
    {
        TimeManager.OnGameEnd += CalculateEnding;
    }

    void OnDisable()
    {
        TimeManager.OnGameEnd -= CalculateEnding;
    }

    void CalculateEnding()
    {
        if(Like_Manager.instance == null)
        {
            Debug.Log("LikeManager doesnt exist.");
            EndingData.ResultEndingId = "Bad";
            return;
        }


        bool success_0 = Like_Manager.instance.GetLike(0) >= 100;
        bool success_1 = Like_Manager.instance.GetLike(1) >= 100;
        bool success_2 = Like_Manager.instance.GetLike(2) >= 100;

        int successCount = 0;
        if(success_0) successCount++;
        if(success_1) successCount++;
        if(success_2) successCount++;

        string result;

        if(successCount == 0)
        {
            result = "Bad";
        }else if(successCount == 3)
        {
            result = "All_Good";
        }else if (successCount == 1)
        {
            if (success_0) result = "Ending_0";
            else if (success_1) result = "Ending_1";
            else result = "Ending_2";
        } else
        {
            string pair;
            if (success_0 && success_1) pair = "01";
            else if (success_0 && success_2) pair = "02";
            else pair = "12";

            bool isGood = Random.Range(0, 2) == 0;
            result = $"Ending_{pair}_{(isGood ? "Good" : "Bad")}";
        }

        EndingData.ResultEndingId = result;
        Debug.Log(result);
    }
}
