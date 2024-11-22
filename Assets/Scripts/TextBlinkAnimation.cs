// 21i-0408
// 21i-0425

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBlinkAnimation : MonoBehaviour
{
   public GameObject text;
   [SerializeField]
   private float delay;
   private bool isDisabled;

   private void Start()
   {
      isDisabled = false;
      InvokeRepeating("Blink", 0f, delay);
   }

   private void Blink()
   { 
      if(isDisabled == true)
      {
         isDisabled = false;
         text.SetActive(false);
      }
      else if(isDisabled == false)
      {
         isDisabled = true;
         text.SetActive(true);
      }
   }

}
