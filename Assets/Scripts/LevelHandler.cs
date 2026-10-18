using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public float RequiredDrag=0,CurrentDragCount=0;
    public Rigidbody2D rb;
    
    public BoxCollider2D[] Dragables;
   
    public void DragCountCheck()
    {
        CurrentDragCount++;
        if(CurrentDragCount>=RequiredDrag)
        {
            FreeTheBall();
        }

    }
     public float ReqX,ReqY;
     public void FreeTheBall()
    {
        
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;

        foreach(var i in Dragables)
        {
            i.size = new Vector2(ReqX, ReqY);
        }
    }
}
