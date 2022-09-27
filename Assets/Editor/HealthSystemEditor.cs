using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HealthSystem))]
public class HealthSystemEditor : Editor 
{
    private float oldMaxHP;




    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        

        GUILayout.BeginHorizontal();

        HealthSystem hs = (HealthSystem)target;
        if (oldMaxHP != hs.getMaxHP())
        {
            //Debug.Log("New Max HP: " + hs.getMaxHP());
            oldMaxHP = hs.getMaxHP();
        }

        if ( GUILayout.Button("Deal Damage"))
        {
            hs.DealDamage(10.0f);
            hs.DisplayHP();
        }

        if( GUILayout.Button("Heal Damage"))
        {
            hs.HealDamage(10.0f);
            hs.DisplayHP();
        }
        /*
        if( hs.getCurrentHP() <= 0f)
        {
            hs.HealDamage(hs.getMaxHP());
            hs.SetMaxHP(1000f);
        }
        */
        GUILayout.EndHorizontal();
    }
}
