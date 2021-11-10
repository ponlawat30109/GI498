using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCustomer : MonoBehaviour
{
    public PlayerAnimController pControl;
    public ModelScript.ModelComponent mCompo;
    private void OnGUI()
    {
        if(GUILayout.Button("Random Customer Skin"))
        {
            mCompo.RandomSkin();
        }
    }
}
