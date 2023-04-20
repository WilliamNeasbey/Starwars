using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Projectile : MonoBehaviour
{
    public TMP_Text enemyText; // Reference to the TMP_Text object for displaying the number of enemies
    private int enemyCount; // Keep track of the number of enemies

   
public void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("droid").Length; // Count the number of enemies with the tag "droid"
        UpdateEnemyText(); // Update the TMP_Text object with the initial enemy count
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("projectile"))
        {
            Destroy(collision.gameObject); // Destroy the projectile object
            DecrementEnemyCount(); // Decrement the enemy count
            UpdateEnemyText(); // Update the TMP_Text object with the new enemy count
        }
    }

    private void DecrementEnemyCount()
    {
        enemyCount--;
    }

    private void UpdateEnemyText()
    {
        enemyText.text = "Enemies Left: " + enemyCount; // Update the TMP_Text object with the current enemy count
    }
    
}
