using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DotaAffair.Extensions
{
    public static class Extensions
    {
        public static bool IsLast(this CardDataEntry card)
        {
            return DataManager.Instance.cardIndexer.IsLast(card);
        }

        /// <summary>
        /// Get the next card in the card array
        /// </summary>
        public static CardDataEntry Succession(this CardDataEntry card)
        {
            if (card.IsLast())
            {
                throw new System.ArgumentOutOfRangeException("Reached last card when trying to get the next card!");
            }
            return DataManager.Instance.cardIndexer.Next(card);
        }
    }
}