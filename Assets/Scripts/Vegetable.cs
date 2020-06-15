using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vegetable : PickUpObject
{
    

public enum vegetables
    {
        carrot,
        broccoli,
        eggplant,
        corn,
        tomato,
        paprika
    }
public vegetables type;

public  Sprite carrotS;

public  Sprite broccoliS;

public  Sprite eggplantS;

public Sprite cornS;

public Sprite tomatoS;

public Sprite paprikaS;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        type = RandomVegetable();
        Sprite vegSprite = TypeToSprite(type);
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = vegSprite;
    }

    
    private vegetables RandomVegetable ()
    {
        int i = Random.Range(0, 5);
        switch (i)
        {
            case 0:
                return vegetables.carrot;
                
            case 1:
                return vegetables.broccoli;
               
            case 2:
                return vegetables.eggplant;
                
            case 3:
                return vegetables.corn;
                
            case 4:
                return vegetables.tomato;
               
            case 5:
                return vegetables.paprika;
               
            default:
                return vegetables.carrot;
              
        }
    }

   private Sprite TypeToSprite(vegetables veg)
   {
       switch (veg)
       {
           case vegetables.carrot:
               return carrotS;
           case vegetables.broccoli:
               return broccoliS;
           case vegetables.eggplant:
               return eggplantS;
           case vegetables.corn:
               return cornS;
           case vegetables.tomato:
               return tomatoS;
           case vegetables.paprika:
               return paprikaS;

           default:
               return null;
       }
   }
}
