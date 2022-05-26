using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Crystal crystal;
    public static Player player;
    [SerializeField] GameObject spawnContainer;
    [SerializeField] Text waveText;
    [SerializeField] Text remainingText;
    [SerializeField] Enemy[] enemyPrefabs;
    [SerializeField] Transform enemyContainer;
    List<Transform> spawnpoints = new List<Transform>();
    AudioSource asrc;
    AudioClip pass;
    AudioClip death;
    AudioClip hit;

    private void Start()
    {
        crystal = GameObject.Find("Crystal").GetComponent<Crystal>();
        player = GameObject.Find("Player").GetComponent<Player>();

        asrc = GetComponent<AudioSource>();
        pass = Resources.Load<AudioClip>("Audio/Effects/correct-soft-beep-2_C_major");
        death = Resources.Load<AudioClip>("Audio/Effects/Death");

        foreach (Transform child in spawnContainer.transform)
        {
            spawnpoints.Add(child);
        }
        if (wave != 1) asrc.PlayOneShot(pass);
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
            spawnTimer = -2f;
            if (wave != 1) asrc.PlayOneShot(pass);
        }
        spawnTimer += Time.deltaTime;
        if (toSpawn > 0 && spawnTimer > spawnCooldown)
        {
            toSpawn--;
            spawnTimer -= spawnCooldown;
            float rand = Random.Range(0f, 1f);
            int type;
            if (rand < 0.75f)
                type = 0;
            else
                type = 1;
            SpawnEnemy(type);
        }
        
        UpdateUI();
    }

    public void SpawnEnemy(int type) 
    {
        Vector2 spawnPos = spawnpoints[Random.Range(0, spawnpoints.Count - 1)].position;
        Enemy e = Instantiate(enemyPrefabs[type], spawnPos, transform.rotation);
        if (type == 0)
            e.target = crystal.transform;
        else
            e.target = player.transform;
        e.transform.parent = enemyContainer;
    }
    
    public void Death()
    {
        asrc.PlayOneShot(death);
    }

    public void UpdateUI()
    {
        waveText.text = "Wave " + wave.ToString();
        remainingText.text = "Remaining: " + enemyContainer.childCount.ToString();
    }
}
