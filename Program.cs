using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RocketMC.Objects;
using RocketMC.Utilities;
using RocketMC.Events;

namespace RocketMC
{
    public static class Program
    {
        private const string NestedHeader = "  ";
        private const string NestedResultHeader = NestedHeader + "-> ";

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

        private static void WriteNested(string str)
        {
            Console.Write(NestedHeader + str);
        }

        private static void WriteNestedResult(string str)
        {
            Console.WriteLine(NestedResultHeader + str);
        }

        private static void WriteSuccess()
        {
            ConsoleUtility.UpdateLine(NestedResultHeader + "Success");
            Console.WriteLine();
        }

        private static void UpdatePreviousNestedLine(string str)
        {
            Console.CursorTop--;
            ConsoleUtility.ClearLine();
            WriteNested(str);
        }

        private static void OnProgressChanged(MinecraftProgressEventArgs e)
        {
            ConsoleUtility.UpdateLine(
                $"{NestedHeader}[{e.ProcessingItemIndex}/{e.AllItemsCount}] {e.ProcessingItem}"
            );
        }

        private static void OnConsoleDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null) return;
            WriteNestedResult(e.Data);
        }
    }
}