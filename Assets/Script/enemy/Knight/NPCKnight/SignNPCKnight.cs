//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SignNPCKnight : MonoBehaviour, IInteractable
//{
//    public GameObject TalkObject1;
//    public GameObject TalkObject2;
//    public void TriggerAction()
//    {
//        TalkObject1.SetActive(true);
//        StartCoroutine(Talk());
//        TalkObject1.SetActive(false);
//        TalkObject2.SetActive(true);
//        StartCoroutine (Talk());
//    }
//    private IEnumerator Talk()
//    {
//        yield return new WaitForSeconds(3f);
//    }
//    //private void OnTriggerExit2D(Collider2D other)
//    //{
//    //    if (TalkObject != null && TalkObject == true)
//    //    {
//    //        TalkObject.SetActive(false);
//    //    }

//    //}
//}
