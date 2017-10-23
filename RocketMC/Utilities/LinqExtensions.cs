using System;
using System.Collections.Generic;
using System.Linq;

namespace RocketMC.Utilities
{
    /// <summary>
    /// LINQの拡張機能
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Enumerableなオブジェクトに一致するアイテムが存在するかを調べます。
        /// </summary>
        /// <typeparam name="T">アイテムの型</typeparam>
        /// <param name="enumerable">調べるオブジェクト</param>
        /// <param name="predicate">アイテムの条件</param>
        /// <returns>存在するかどうか</returns>
        public static bool ContainsWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return enumerable.Count(predicate) != 0;
        }
    }
}