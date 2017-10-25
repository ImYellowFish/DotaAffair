using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DotaAffair.Condition
{
    public delegate bool CardConditionOperation(int a, int b);

    public static class CardConditionRule
    {

        private static Dictionary<string, CardConditionOperation> rules = new Dictionary<string, CardConditionOperation>()
    {
        {"<", LT},
        {"<=", LE},
        {">", RT},
        {">=", RE},
        {"==", EQ},
        {"!=", NE},
    };

        public static CardConditionOperation GetOperation(string operationKey)
        {
            try
            {
                operationKey = operationKey.Trim();
                return rules[operationKey];
            }
            catch
            {
                throw new System.ArgumentException("Undefined operation: " + operationKey);
            }
        }



        private static bool LT(int a, int b)
        {
            return a < b;
        }

        private static bool LE(int a, int b)
        {
            return a <= b;
        }

        private static bool RT(int a, int b)
        {
            return a > b;
        }

        private static bool RE(int a, int b)
        {
            return a >= b;
        }

        private static bool EQ(int a, int b)
        {
            return a == b;
        }

        private static bool NE(int a, int b)
        {
            return a != b;
        }
    }

    [System.Serializable]
    public class CardCondition
    {
        public ConditionDataEntry data;
        public List<CardConditionElement> elements;

        public CardCondition(ConditionDataEntry data)
        {
            this.data = data;
            elements = new List<CardConditionElement>();
            for(int i = 0; i < data.conditions.Count; i++)
            {
                elements.Add(new CardConditionElement(data.conditions[i]));
            }
        }

        public bool IsTrue()
        {
            for(int i = 0; i < elements.Count; i++)
            {
                if (!elements[i].IsTrue())
                {
                    return false;
                }
            }
            return true;
        }
    }

    [System.Serializable]
    public class CardConditionElement
    {
        public GlobalVariable leftVariable;
        public int rightConstantValue;
        private CardConditionOperation operation;

        public ConditionDataEntry.conditions_struct data;
        
        public CardConditionElement(ConditionDataEntry.conditions_struct data)
        {
            this.data = data;
            leftVariable = GlobalVariableManager.GetVar(data.variable);
            rightConstantValue = data.value;
            operation = CardConditionRule.GetOperation(data._operator);
        }

        public bool IsTrue()
        {
            return operation.Invoke(leftVariable.value, rightConstantValue);
        }
    }
}