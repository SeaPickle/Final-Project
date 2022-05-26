using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Crystal crystal;
    [SerializeField] GameObject spawnContainer;
    [SerializeField] Text waveText;
    [SerializeField] Text remainingText;
    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Transform enemyContainer;
    List<Transform> spawnpoints = new List<Transform>();
    AudioSource asrc;
    AudioClip pass;
    private void Start()
    {
        crystal = GameObject.Find("Crystal").GetComponent<Crystal>();

        asrc = GetComponent<AudioSource>();
        pass = Resources.Load<AudioClip>("Audio/Effects/correct-soft-beep-2_C_major");

        foreach (Transform child in spawnContainer.transform)
        {
            spawnpoints.Add(child);
        }
    }

    int wave = 0;
    int toSpawn = 0;
    float spawnCooldown = 1f;
    float spawnTimer = 0f;
    

    public void Update()
    {   
        if (enemyContainer.childCount==0 && toSpawn==0)
        {
            wave++;
            toSpawn = wave * 2 + 10;
            spawnTimer = 0f;
            if (wave != 1) asrc.PlayOneShot(pass);
        }
        spawnTimer += Time.deltaTime;
        if (toSpawn > 0 && spawnTimer > spawnCooldown)
        {
            toSpawn--;
            spawnTimer -= spawnCooldown;
            Vector2 spawnPos = spawnpoints[Random.Range(0, spawnpoints.Count - 1)].position;
            Enemy e = Instantiate(enemyPrefab, spawnPos, transform.rotation);
            e.transform.parent = enemyContainer;
        }
        
        UpdateUI();
    }

    public void UpdateUI()
    {
        waveText.text = "Wave " + wave.ToString();
        remainingText.text = "Remaining: " + enemyContainer.childCount.ToString();
    }
}
