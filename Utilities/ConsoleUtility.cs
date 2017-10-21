using System;
using System.Runtime.InteropServices;
using System.Security;

namespace RocketMC.Utilities
{
    /// <summary>
    /// 対話式コンソールのためのユーティリティ
    /// </summary>
    public static class ConsoleUtility
    {
        /// <summary>
        /// SecureStringを使用してコンソールからパスワードを読み取ります。
        /// </summary>
        /// <returns>読み取ったパスワード</returns>
        public static string ReadPassword()
        {
            using (var password = new SecureString())
            {
                var cursor = 0;
                var still = true;
                while (still)
                {
                    var key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Backspace:
                            if (cursor > 0 && password.Length > 0) password.RemoveAt(--cursor);
                            break;

                        case ConsoleKey.Delete:
                            if (cursor < password.Length && password.Length > 0) password.RemoveAt(cursor);
                            break;

                        case ConsoleKey.Enter:
                            still = false;
                            break;

                        case ConsoleKey.LeftArrow:
                            if (cursor > 0) cursor--;
                            break;

                        case ConsoleKey.RightArrow:
                            if (cursor < password.Length) cursor++;
                            break;

                        default:
                            password.InsertAt(cursor++, key.KeyChar);
                            break;
                    }
                }

                Console.WriteLine();
                return Marshal.PtrToStringUni(Marshal.SecureStringToGlobalAllocUnicode(password));
            }
        }

        /// <summary>
        /// 現在の行の文字列を更新します。
        /// カーソル位置は最後になります。
        /// </summary>
        /// <param name="str">新しい文字列</param>
        public static void UpdateLine(string str)
        {
            Console.CursorLeft = 0;
            Console.Write(str);
            FillBlank();
        }

        /// <summary>
        /// 現在の行の文字列を削除します。
        /// カーソル位置は最初に戻ります。
        /// </summary>
        public static void ClearLine()
        {
            Console.CursorLeft = 0;
            FillBlank();
            Console.CursorLeft = 0;
        }

        /// <summary>
        /// 現在のカーソル位置から右端までの文字列を削除します。
        /// カーソル位置は最後になります。
        /// </summary>
        public static void FillBlank()
        {
            Console.Write(new string(' ', Console.WindowWidth - Console.CursorLeft - 1));
        }
    }
}