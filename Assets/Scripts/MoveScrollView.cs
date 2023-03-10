using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoveScrollView : MonoBehaviour
{

    public GameObject scroll;
    // Start is called before the first frame update
    bool rightEnabled = false;
    bool leftEnabled = false;


    public void RightClick()
    {
        if(scroll.GetComponent<RectTransform>().position.x > -96f)
            scroll.GetComponent<RectTransform>().position = new Vector3(scroll.GetComponent<RectTransform>().position.x - 17.77f, scroll.GetComponent<RectTransform>().position.y, scroll.GetComponent<RectTransform>().position.z);

    }

    public void LeftClick()
    {
        if(scroll.GetComponent<RectTransform>().position.x < -9f)
            scroll.GetComponent<RectTransform>().position = new Vector3(scroll.GetComponent<RectTransform>().position.x + 17.77f, scroll.GetComponent<RectTransform>().position.y, scroll.GetComponent<RectTransform>().position.z);

    }



    /*  public void rightDown()
      {

          rightEnabled = true;
          leftEnabled = false;

      }

      public void leftDown() {
          leftEnabled = true;
          rightEnabled = false;

      }

      public void rightUp()
      {
          leftEnabled = false;
          rightEnabled = false;
      }

      public void leftUp() {
          leftEnabled = false;
          rightEnabled = false;
      }*/




    // Update is called once per frame
    /* void Update()
     {
         if (rightEnabled)
         {
             scroll.GetComponent<RectTransform>().position = new Vector3(scroll.GetComponent<RectTransform>().position.x - 5f * Time.deltaTime, scroll.GetComponent<RectTransform>().position.y, scroll.GetComponent<RectTransform>().position.z);
         }
         if (leftEnabled)
         {
             scroll.GetComponent<RectTransform>().position = new Vector3(scroll.GetComponent<RectTransform>().position.x + 5f * Time.deltaTime, scroll.GetComponent<RectTransform>().position.y, scroll.GetComponent<RectTransform>().position.z);
         }
     }*/
}
