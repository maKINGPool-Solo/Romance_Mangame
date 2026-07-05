using UnityEngine;

public static class EndingData
{
    public static int ResultEndingId;
}

public class EndingCalculator : MonoBehaviour
{
    public static EndingCalculator instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


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
            EndingData.ResultEndingId = 0;
            return;
        }


        bool success_0 = Like_Manager.instance.GetLike(0) >= 100;
        bool success_1 = Like_Manager.instance.GetLike(1) >= 100;
        bool success_2 = Like_Manager.instance.GetLike(2) >= 100;

        int successCount = 0;
        if(success_0) successCount++;
        if(success_1) successCount++;
        if(success_2) successCount++;

        int result;

        if(successCount == 0)
        {
            result = 0;
        }else if(successCount == 3)
        {
            result = 10;
        }else if (successCount == 1)
        {
            if (success_0) result = 1;
            else if (success_1) result = 2;
            else result = 3;
        } else
        {
            bool isGood = Random.Range(0, 2) == 0;
            if (success_0 && success_1)
            {
                result = isGood ? 4 : 5;
            }
            else if (success_0 && success_2)
            {
                result = isGood ? 8 : 9;
            }
            else
            {
                result = isGood ? 6 : 7;
            }
        }

        EndingData.ResultEndingId = result;
        Debug.Log(result);
    }
}
