using System;

namespace RocketMC.Events
{
    /// <summary>
    /// アセットやライブラリのインストール状況が変化したときのイベントハンドラ
    /// </summary>
    /// <param name="e">現在の状況</param>
    public delegate void MinecraftProgressEventHandler(MinecraftProgressEventArgs e);

    /// <summary>
    /// アセットやライブラリのインストール状況
    /// </summary>
    public class MinecraftProgressEventArgs : EventArgs
    {
        public int AllItemsCount { get; }
        public int ProcessingItemIndex { get; }
        public string ProcessingItem { get; }

        internal MinecraftProgressEventArgs(int allItems, int itemIndex, string processing)
        {
            AllItemsCount = allItems;
            ProcessingItemIndex = itemIndex;
            ProcessingItem = processing;
        }
    }
}