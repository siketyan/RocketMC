using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RocketMC.Events;
using RocketMC.Objects;
using RocketMC.CUI.Utilities;

namespace RocketMC.CUI
{
    /// <summary>
    /// メインクラス
    /// </summary>
    public static class Program
    {
        private const string NestedHeader = "  ";
        private const string NestedResultHeader = NestedHeader + "-> ";
        private const string Success = NestedResultHeader + "Success";

        /// <summary>
        /// アプリケーションの起動メソッド
        /// </summary>
        /// <param name="args">引数の配列</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("> Authenticating your account");
            Credentials credentials;
            const string credentialsFile = "credentials.json";
            if (File.Exists(credentialsFile))
            {
                var json = File.ReadAllText(credentialsFile);
                credentials = JsonConvert.DeserializeObject<Credentials>(json);

                if (!credentials.Validate())
                {
                    WriteNestedResult("Saved token is invalid. Re-authenticate with your credentials.");
                    return;
                }
            }
            else
            {
                WriteNested("Username: ");
                var username = Console.ReadLine();
                
                UpdatePreviousNestedLine("Password: ");
                var password = ConsoleUtility.ReadPassword();

                try
                {
                    UpdatePreviousNestedLine("Sending request");
                    credentials = Minecraft.Authenticate(username, password);
                    WriteSuccess();
                }
                catch
                {
                    WriteNestedResult("Failed to authenticate. Check your credentials.");
                    return;
                }

                var json = JsonConvert.SerializeObject(credentials);
                File.WriteAllText(credentialsFile, json);
            }

            Console.WriteLine("> Downloading version manifest");
            var manifest = Minecraft.GetVersionManifest();

            Objects.Version version;
            var versions = manifest.Versions;
            if (args.Length > 0)
            {
                version = versions.FirstOrDefault(v => v.Id == args[0]);
                if (version != null)
                {
                    WriteNestedResult($"Specified: {version.Id} ({version.Type})");
                }
                else
                {
                    WriteNestedResult("Version not found");
                    return;
                }
            }
            else
            {
                version = versions.First(v => v.Id == manifest.Latest.Release);
                WriteNestedResult($"Latest: {version.Id} ({version.Type})");
            }

            Console.WriteLine("> Downloading version detail");
            var detail = version.GetDetail();

            Console.WriteLine("> Downloading assets index");
            var assets = detail.GetAssetIndex("assets");

            Console.WriteLine("> Downloading assets");
            assets.DownloadAssets("assets", OnProgressChanged);
            WriteSuccess();

            Console.WriteLine("> Downloading libraries");
            var result = detail.DownloadLibraries("libraries", OnProgressChanged);
            var classpath = result.Classpath;
            var extracts = result.Extracts;
            WriteSuccess();

            Console.WriteLine("> Downloading client");
            classpath.Add(detail.DownloadClient("versions"));

            Console.WriteLine("> Extracting natives");
            Minecraft.ExtractNatives(extracts, "natives", OnProgressChanged);
            WriteSuccess();

            Console.WriteLine("> Launching Minecraft");
            detail.Launch(credentials, "java", classpath, true, OnConsoleDataReceived);
        }

        /// <summary>
        /// ネストされた文字列をコンソールに出力します。
        /// </summary>
        /// <param name="str">文字列</param>
        private static void WriteNested(string str)
        {
            Console.Write(NestedHeader + str);
        }

        /// <summary>
        /// ネストされた結果の文字列をコンソールに出力します。
        /// </summary>
        /// <param name="str">文字列</param>
        private static void WriteNestedResult(string str)
        {
            Console.WriteLine(NestedResultHeader + str);
        }

        /// <summary>
        /// 完了を示す文字列をコンソールに出力します。
        /// </summary>
        private static void WriteSuccess()
        {
            ConsoleUtility.UpdateLine(Success);
            Console.WriteLine();
        }

        /// <summary>
        /// コンソール上の前の行を書き換え、ネストして出力します。
        /// </summary>
        /// <param name="str">文字列</param>
        private static void UpdatePreviousNestedLine(string str)
        {
            Console.CursorTop--;
            ConsoleUtility.ClearLine();
            WriteNested(str);
        }

        /// <summary>
        /// アセットやライブラリのインストール状況が変化したときのイベントハンドラ
        /// </summary>
        /// <param name="e">イベント引数</param>
        private static void OnProgressChanged(MinecraftProgressEventArgs e)
        {
            ConsoleUtility.UpdateLine(
                $"{NestedHeader}[{e.ProcessingItemIndex}/{e.AllItemsCount}] {e.ProcessingItem}"
            );
        }

        /// <summary>
        /// Minecraftの標準出力を受け取ったときのイベントハンドラ
        /// </summary>
        /// <param name="sender">イベントの発生オブジェクト</param>
        /// <param name="e">イベント引数</param>
        private static void OnConsoleDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null) return;
            WriteNestedResult(e.Data);
        }
    }
}