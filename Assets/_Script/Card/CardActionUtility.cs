using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardActionUtility{
    public static void InitActions() {
        CardDataEntry.left_event_action_goto_if += GotoIf;
        CardDataEntry.left_event_action_goto_ifnot += GotoIfNot;
    }

    public static void GotoIf(string cardID, string conditionName) {
        if (ConditionManager.Check(conditionName)) {
            Table.Instance.cardDispatcher.Dispatch(cardID);
        }
    }

    public static void GotoIfNot(string cardID, string conditionName) {
        if (!ConditionManager.Check(conditionName)) {
            Table.Instance.cardDispatcher.Dispatch(cardID);
        }
    }
}
