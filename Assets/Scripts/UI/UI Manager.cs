using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Referencias
    private Health healthRef;
    private Gun gunRef;
    private int enemyCount;
    [SerializeField] private int maxEnemyCount;

    // Interfaz
    [SerializeField] private TextMeshProUGUI healthTXT;
    [SerializeField] private TextMeshProUGUI ammoTXT;
    [SerializeField] private TextMeshProUGUI enemyTxt;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    private bool gameEnded = false; // Evita que se active la pantalla varias veces

    void Start()
    {
        InitialSettings();
        enemyCount = maxEnemyCount;
    }

    void Update()
    {
        UpdateUI();
    }

    public void EnemyKill()
    {
        enemyCount--;
        if (enemyCount <= 0 && !gameEnded)
        {
            gameEnded = true;
            StartCoroutine(ShowWinScreen()); 
        }
    }

    void InitialSettings()
    {
        healthRef = FindAnyObjectByType<Health>();
        gunRef = FindAnyObjectByType<Gun>();
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    private void UpdateUI()
    {
        if (healthRef == null) healthRef = FindAnyObjectByType<Health>();
        if (gunRef == null) gunRef = FindAnyObjectByType<Gun>();

        healthTXT.SetText(healthRef.GetHealth().ToString());
        if (gunRef != null)
        {
            ammoTXT.SetText(gunRef.GetAmmo().ToString() + " / " + gunRef.GetMaxAmmo().ToString());
        }

        enemyTxt.SetText(enemyCount.ToString() + " / " + maxEnemyCount.ToString());

        // Verifica si el jugador ha perdido
        if (healthRef.GetHealth() <= 0 && !gameEnded)
        {
            gameEnded = true;
            StartCoroutine(ShowLoseScreen()); 
        }
    }

    private IEnumerator ShowWinScreen()
    {
        yield return new WaitForSeconds(5f);
        Cursor.lockState = CursorLockMode.None;
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    private IEnumerator ShowLoseScreen()
    {
        yield return new WaitForSeconds(5f);
        Cursor.lockState = CursorLockMode.None;
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }


}