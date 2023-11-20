using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CoffeeData", menuName = "ScriptableObjects/CoffeeData ", order = 1)]
    public class CoffeeData : ScriptableObject
    {
        public string coffee_name;
        public Sprite coffee_sprite;
        
        public float best_grind_size;
        public float best_dose_weight;
        public float best_temperature;
    }

}
