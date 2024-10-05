using UnityEngine;

[System.Serializable]
public static class BaseCard
{

    public static int[,] cardValues = 
    {
        /* Last Column Is Prices */
        /* Rock */                    {   0, -1,  1,  1, -1,  1, -1,  1,  0,  0, -1,  1,  1,  1, -1,  1, -1, -1,  1, -1,  1,  1, -1,  2,  2, 2 },
        /* Paper */                   {   1,  0, -1, -1,  1,  1, -1, -1, -1,  1, -1, -1,  1, -1, -1, -1,  1,  1, -1, -1,  1, -1, -1,  2,  2, 2 },
        /* Scissors */                {  -1,  1,  0,  1, -1,  1, -1,  1, -1, -1,  1, -1,  1,  1,  1, -1, -1,  1,  1, -1,  1,  1, -1,  2,  2, 2 },
        /* Lizard */                  {  -1,  1, -1,  0,  1,  1, -1, -1,  1, -1,  1,  1, -1, -1, -1, -1, -1, -1, -1, -1,  1, -1, -1,  2,  2, 2 },
        /* Spock */                   {   1, -1,  1, -1,  0,  1, -1,  1,  1,  1, -1,  1, -1, -1,  1,  1,  1, -1,  1,  1, -1,  1,  1,  2,  2, 2 },
        /* Amputee */                 {  -1, -1, -1, -1, -1,  0,  1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,  2,  2, 1 },
        /* Bullcrap */                {   1,  1,  1,  1,  1, -1,  0,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  1,  2,  2, 8 },
        /* Baby Hand */               {  -1,  1, -1,  1, -1,  1, -1,  0, -1, -1,  0, -1,  1, -1,  0, -1,  1, -1, -1, -1,  1, -1,  1,  2,  2, 4 },
        /* Skeleton */                {   0,  1,  1, -1, -1,  1, -1,  1,  0,  0, -1, -1,  1, -1, -1,  1,  1, -1,  1,  1, -1,  1, -1,  2,  2, 4 },
        /* Rock 2 */                  {   0, -1,  1,  1, -1,  1, -1,  1,  0,  0, -1,  1,  1,  1, -1,  1, -1, -1,  1, -1,  1,  1, -1,  2,  2, 3 },
        /* Man Spider */              {   1,  1, -1, -1,  1,  1, -1,  0,  1, -1,  0, -1,  1, -1,  1,  1, -1,  1, -1, -1,  1,  1, -1,  2,  2, 5 },
        /* ok */                      {  -1,  1,  1, -1, -1,  1, -1,  1,  1, -1,  1,  0,  1,  1, -1,  1, -1,  1,  1, -1,  1, -1,  1,  2,  2, 2 },
        /* AI Generated */            {  -1, -1, -1,  1,  1,  1, -1, -1, -1, -1, -1, -1,  0, -1,  1, -1,  0,  1, -1, -1,  1,  1, -1,  2,  2, 4 },
        /* Bird */                    {  -1,  1, -1,  1,  1,  1, -1,  1,  1, -1,  1, -1,  1,  0,  0, -1,  1, -1,  1,  1, -1, -1, -1,  2,  2, 4 },
        /* Dog */                     {   1,  1, -1,  1, -1,  1, -1,  0,  1,  1, -1,  1, -1,  0,  0, -1,  1, -1, -1, -1,  1,  1, -1,  2,  2, 4 },
        /* Finger Gun */              {  -1,  1,  1,  1, -1,  1, -1,  1, -1, -1, -1, -1,  1,  1,  1,  0, -1, -1,  1,  1, -1, -1,  1,  2,  2, 6 },
        /* Man Of Iron */             {   1, -1,  1,  1, -1,  1, -1, -1, -1,  1,  1,  1,  0, -1, -1,  1,  0,  1,  1, -1,  1,  1, -1,  2,  2, 5 },
        /* Green Fist */              {   1, -1, -1,  1,  1,  1, -1,  1,  1,  1, -1, -1, -1,  1,  1,  1, -1,  0, -1,  1, -1, -1, -1,  2,  2, 5 },
        /* Point */                   {  -1,  1, -1,  1, -1,  1, -1,  1, -1, -1,  1, -1,  1, -1,  1, -1, -1,  1,  0, -1,  1,  1,  1,  2,  2, 4 },
        /* Thumbs Up */               {   1,  1,  1,  1, -1,  1, -1,  1, -1,  1,  1,  1,  1, -1,  1, -1,  1, -1,  1,  0,  1,  1,  1,  2,  2, 5 },
        /* Thumbs Down */             {  -1, -1, -1, -1,  1,  1, -1, -1,  1, -1, -1, -1, -1,  1, -1,  1, -1,  1, -1, -1,  0, -1,  1,  2,  2, 2 },
        /* Thumbs Side */             {  -1,  1, -1,  1, -1,  1, -1,  1, -1,  1, -1,  1, -1,  1, -1,  1, -1,  1, -1, -1,  1,  0,  1,  2,  2, 3 },
        /* Lego Hand */               {   1,  1,  1,  1, -1,  1, -1, -1,  1,  1,  1, -1,  1,  1,  1, -1,  1,  1, -1, -1, -1, -1,  0,  2,  2, 5 },
        /* Cross Fingers*/            {   2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  0,  2, 5 },
        /* Pray */                    {   2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  2,  0, 5 }
    };


}
