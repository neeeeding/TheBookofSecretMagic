using _02Script.Obj.Character;
using _02Script.UI.School;
using UnityEngine;

namespace _02Script.Obj.Room
{
    public class ClassRoom : OneRoom
    {
        [Header("ClassRoom")]
        [SerializeField] private CountClassTime classManager;
        [SerializeField] private PlayerJob classType;
        
        
    }
}