using System;
using _02Script.Manager;
using _02Script.Obj.Character;
using TMPro;
using UnityEngine;

namespace _02Script.UI.School
{
    public class CountClassTime : MonoBehaviour
    {
        [Header("Need")]
        [SerializeField] private TMP_Dropdown[] select; // 해당 드롭 다운
        private readonly float _studyTime = 50;
        private readonly float _restTime = 10;
        
        private PlayerJob curClass;
        private int curClassIndex;
        private float studyTime;
        private float restTime;
        private bool isStudy;

        private void Awake()
        {
            studyTime = 9 * 60;
            float gameHour = GameManager.Instance.PlayerStat.hour;
            gameHour = (gameHour == 24 ? 0
                : gameHour);
            curClassIndex = 0;
            
            while (gameHour > studyTime/60)
            {
                studyTime += _studyTime + _restTime;
                curClassIndex++;
            }
            restTime = studyTime;
            isStudy = false;
        }
        
        public (bool, PlayerJob) CheckClass()
        {
            return (isStudy, curClass);
        }


        private void Update()
        {
            if(curClassIndex >= select.Length) return; //모든 교시 종료
            float gameHour = GameManager.Instance.PlayerStat.hour;
            gameHour = (gameHour == 24 ? 0
                : gameHour);
            
            float gameTime = (gameHour * 60) +  GameManager.Instance.PlayerStat.minute;
            
            if (gameTime > studyTime && gameTime <= restTime && isStudy) //휴식시간
            {
                isStudy = !isStudy;
                studyTime += _restTime;
            }
            if (gameTime >= studyTime && !isStudy) //공부 시간
            {
                isStudy = !isStudy;
                curClass = (PlayerJob)select[curClassIndex++].value;
                studyTime += _studyTime;
                restTime += _restTime + _studyTime;
            }
        }
    }
}