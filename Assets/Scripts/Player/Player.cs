using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(playerStats.health);
    }

    // Update is called once per frame
    void Update()
    {
        playerStats.health -= Time.deltaTime * .1f;
    }
}
