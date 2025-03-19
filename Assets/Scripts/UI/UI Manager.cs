using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class UIManager : MonoBehaviour
{
    //Variables
    private Health healthRef;
    private Gun gunRef;
    [SerializeField] private int enemyCount;
    [SerializeField] private int maxEnemyCount;

    //Interfaz
    [SerializeField] TextMeshProUGUI healthTXT;
    [SerializeField] TextMeshProUGUI ammoTXT;
    [SerializeField] TextMeshProUGUI enemyTxt;
    [SerializeField] GameObject winScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialSettings();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthRef == null) healthRef = FindAnyObjectByType<Health>();
        if (gunRef == null) gunRef = FindAnyObjectByType<Gun>();

        healthTXT.SetText( healthRef.GetHealth().ToString());
        if (gunRef == null) return;
        ammoTXT.SetText (gunRef.GetAmmo().ToString() + " / " + gunRef.GetMaxAmmo().ToString());

        enemyTxt.SetText(enemyCount.ToString() + " / " + maxEnemyCount.ToString());

        if (enemyCount == 0)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void EnemyKill()
    {
        enemyCount--;
    }
    void InitialSettings()
    {
        healthRef = FindAnyObjectByType<Health>();
        gunRef = FindAnyObjectByType<Gun>();
        winScreen.SetActive(false);
        Time.timeScale = 1f;
    }
}
