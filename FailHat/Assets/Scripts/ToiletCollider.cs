using System;
using TMPro;
using UnityEngine;

public class ToiletCollider : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public AudioSource Audio; 
    
    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);

        var score = Convert.ToInt32(Score.text);
        score -= 100;

        Score.text = score.ToString();

        if (score <= 0 && Audio)
        {
            Audio.Stop();
        }
    }
}
