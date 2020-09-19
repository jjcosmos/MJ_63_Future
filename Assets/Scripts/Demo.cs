using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    private Coroutine instructionCoroutine;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && instructionCoroutine == null)
        {
            StartCoroutine(DoInstructionCoroutine());
        }
    }

    private IEnumerator DoInstructionCoroutine()
    {

        //you would set this through whatever input you are using
        List<Instruction> instructions = new List<Instruction>();
        for (int i = 0; i < instructions.Count; i++)
        {
            while (!instructions[i].CheckComplete())
            {
                instructions[i].ExecuteInstruction();
                yield return null;
            }
        }
        //all instrucitons done
    }

    private class Instruction
    {
        public bool CheckComplete()
        {
            //implementation
            return true;
        }

        public void ExecuteInstruction()
        {
            //Do the logic here
        }
    }
}
