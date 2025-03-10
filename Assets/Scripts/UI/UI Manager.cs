using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Variables
    private Health healthRef;
    private Gun gunRef;

    //Interfaz
    [SerializeField] TextMeshProUGUI healthTXT;
    [SerializeField] TextMeshProUGUI ammoTXT;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthRef = FindAnyObjectByType<Health>();
        gunRef = FindAnyObjectByType<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthRef == null) healthRef = FindAnyObjectByType<Health>();
        if (gunRef == null) gunRef = FindAnyObjectByType<Gun>();

        healthTXT.SetText( healthRef.GetHealth().ToString());
        if (gunRef == null) return;
        ammoTXT.SetText (gunRef.GetAmmo().ToString() + " / " + gunRef.GetMaxAmmo().ToString());

    }
}
