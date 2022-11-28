using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UnitsUX : MonoBehaviour
{
    [SerializeField] private UnitBase _unit;
    [SerializeField] private Transform firstIconPos;
    [SerializeField] private float distanceBetweenIcons;

    [SerializeField] private Slider healthPointsSlider;
    private List<AbilityBase> _abilities;
    private List<Image> _abilitiesIconsFill = new List<Image>();
    
    void Start()
    {
        healthPointsSlider.maxValue = _unit.MaxHealPoints;
        healthPointsSlider.value = _unit.CurrentHealPoints;
        
        _abilities = _unit.Abilites;
        for (int n = 0; n < _unit.Abilites.Count; n++)
        {
            GameObject newIconFill = Instantiate(_abilities[n].AbilityIcon, new Vector3(firstIconPos.position.x + distanceBetweenIcons * n, firstIconPos.position.y, firstIconPos.position.z), firstIconPos.rotation, firstIconPos );
            _abilitiesIconsFill.Add(newIconFill.GetComponentInChildren<Image>());
        }
    }

    void Update()
    {
        healthPointsSlider.value = _unit.CurrentHealPoints;
        
        for (int n = 0; n < _abilitiesIconsFill.Count; n++)
        {
            _abilitiesIconsFill[n].fillAmount = 1 - _abilities[n].CurrentTime/_abilities[n].ReloadTime;
        }
    }
}
