using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RocketMC.Enums;
using RocketMC.Events;
using RocketMC.Objects;
using RocketMC.Utilities;

namespace RocketMC
{
    /// <summary>
    /// メインクラス
    /// </summary>
    public static class Minecraft
    {
        private const string AssetsUrlBase = "https://resources.download.minecraft.net/";
        private const string AuthServerUrlBase = "https://authserver.mojang.com/";
        private const string VersionManifestUrl = "https://launchermeta.mojang.com/mc/game/version_manifest.json";
        private const string AuthenticateUrl = AuthServerUrlBase + "authenticate";
        private const string ValidateUrl = AuthServerUrlBase + "validate";

        /// <summary>
        /// URLへGETリクエストを送信します。
        /// </summary>
        /// <param name="url">リクエストを送信するURL</param>
        /// <returns>サーバからの応答</returns>
        private static string Get(string url)
        {
            using (var wc = new WebClient())
            {
                return wc.DownloadString(url);
            }
        }

        /// <summary>
        /// URLへGETリクエストを送信します。
        /// </summary>
        /// <param name="url">リクエストを送信するURL</param>
        /// <returns>サーバからの応答</returns>
        private static async Task<string> GetAsync(string url)
        {
            using (var wc = new WebClient())
            {
                return await wc.DownloadStringTaskAsync(url);
            }
        }

        /// <summary>
        /// URLへPOSTリクエストを送信します。
        /// </summary>
        /// <param name="url">リクエストを送信するURL</param>
        /// <param name="data">リクエストに書き込むデータ</param>
        /// <param name="contentType">データのMIMEタイプ</param>
        /// <returns>サーバからの応答</returns>
        private static string Post(string url, string data, string contentType = "application/json")
        {
            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = contentType;
                return wc.UploadString(url, data);
            }
        }

        /// <summary>
        /// URLへPOSTリクエストを送信します。
        /// </summary>
        /// <param name="url">リクエストを送信するURL</param>
        /// <param name="data">リクエストに書き込むデータ</param>
        /// <param name="contentType">データのMIMEタイプ</param>
        /// <returns>サーバからの応答</returns>
        private static async Task<string> PostAsync(string url, string data, string contentType = "application/json")
        {
            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = contentType;
                return await wc.UploadStringTaskAsync(url, data);
            }
        }

        /// <summary>
        /// URLからファイルをダウンロードします。
        /// </summary>
        /// <param name="url">ダウンロードするURL</param>
        /// <param name="dest">ダウンロードを保存する場所</param>
        private static void DownloadFile(string url, string dest)
        {
            using (var wc = new WebClient())
            {
                wc.DownloadFile(url, dest);
            }
        }

        /// <summary>
        /// URLからファイルをダウンロードします。
        /// </summary>
        /// <param name="url">ダウンロードするURL</param>
        /// <param name="dest">ダウンロードを保存する場所</param>
        private static async Task DownloadFileAsync(string url, string dest)
        {
            using (var wc = new WebClient())
            {
                await wc.DownloadFileTaskAsync(url, dest);
            }
        }

        /// <summary>
        /// オブジェクトをJSONへシリアライズします。
        /// </summary>
        /// <param name="obj">シリアライズするオブジェクト</param>
        /// <returns>シリアライズされたJSON</returns>
        private static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// オブジェクトをJSONへシリアライズします。
        /// </summary>
        /// <param name="obj">シリアライズするオブジェクト</param>
        /// <returns>シリアライズされたJSON</returns>
        private static async Task<string> ToJsonAsync(this object obj)
        {
            return await Task.Run(() => JsonConvert.SerializeObject(obj));
        }

        /// <summary>
        /// JSONを指定された型のオブジェクトへデシリアライズします。
        /// </summary>
        /// <typeparam name="T">オブジェクトのデータ型</typeparam>
        /// <param name="json">デシリアライズするJSON</param>
        /// <returns>デシリアライズされたオブジェクト</returns>
        private static T ToObject<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// JSONを指定された型のオブジェクトへデシリアライズします。
        /// </summary>
        /// <typeparam name="T">オブジェクトのデータ型</typeparam>
        /// <param name="json">デシリアライズするJSON</param>
        /// <returns>デシリアライズされたオブジェクト</returns>
        private static async Task<T> ToObjectAsync<T>(this string json)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<T>(json));
        }

        /// <summary>
        /// サーバからバージョンマニフェストを取得します。
        /// </summary>
        /// <returns>取得したバージョンマニフェスト</returns>
        public static VersionManifest GetVersionManifest()
        {
            return Get(VersionManifestUrl).ToObject<VersionManifest>();
        }

        /// <summary>
        /// サーバからバージョンマニフェストを取得します。
        /// </summary>
        /// <returns>取得したバージョンマニフェスト</returns>
        public static async Task<VersionManifest> GetVersionManifestAsync()
        {
            var json = await GetAsync(VersionManifestUrl);
            return await json.ToObjectAsync<VersionManifest>();
        }

        /// <summary>
        /// サーバからバージョンの詳細を取得します。
        /// </summary>
        /// <param name="version">取得するバージョンID</param>
        /// <returns>取得したバージョンの詳細</returns>
        public static VersionDetail GetDetail(this Objects.Version version)
        {
            return Get(version.Url).ToObject<VersionDetail>();
        }

        /// <summary>
        /// サーバからバージョンの詳細を取得します。
        /// </summary>
        /// <param name="version">取得するバージョンID</param>
        /// <returns>取得したバージョンの詳細</returns>
        public static async Task<VersionDetail> GetDetailAsync(this Objects.Version version)
        {
            var json = await GetAsync(version.Url);
            return await json.ToObjectAsync<VersionDetail>();
        }

        /// <summary>
        /// サーバからアセットのリストを取得します。
        /// </summary>
        /// <param name="detail">アセットを取得するバージョンの詳細</param>
        /// <param name="assetsDirectory">アセットディレクトリの場所（リストを保存する場合）</param>
        /// <returns>アセットのリスト</returns>
        public static AssetIndex GetAssetIndex(this VersionDetail detail, string assetsDirectory = null)
        {
            var json = Get(detail.AssetIndex.Url);
            if (assetsDirectory != null)
            {
                var directory = assetsDirectory + "/indexes";
                var file = $"{directory}/{detail.AssetsId}.json";
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

                File.WriteAllText(file, json);
            }

            return json.ToObject<AssetIndex>();
        }

        /// <summary>
        /// サーバからアセットのリストを取得します。
        /// </summary>
        /// <param name="detail">アセットを取得するバージョンの詳細</param>
        /// <param name="assetsDirectory">アセットディレクトリの場所（リストを保存する場合）</param>
        /// <returns>アセットのリスト</returns>
        public static async Task<AssetIndex> GetAssetIndexAsync(this VersionDetail detail, string assetsDirectory = null)
        {
            var json = await GetAsync(detail.AssetIndex.Url);
            if (assetsDirectory != null)
            {
                var directory = assetsDirectory + "/indexes";
                var file = $"{directory}/{detail.AssetsId}.json";
                await Task.Run(() =>
                {
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    File.WriteAllText(file, json);
                });
            }

            return await json.ToObjectAsync<AssetIndex>();
        }

        /// <summary>
        /// リストからアセットをダウンロードします。
        /// </summary>
        /// <param name="index">ダウンロードするアセットのリスト</param>
        /// <param name="assetsDirectory">アセットディレクトリの場所</param>
        /// <param name="onProgressChanged">進捗状況が変化したときのイベントハンドラ</param>
        public static void DownloadAssets(this AssetIndex index, string assetsDirectory,
                                          MinecraftProgressEventHandler onProgressChanged = null)
        {
            var objects = index.Objects;
            var keys = objects.Keys.ToArray();
            for (var i = 0; i < objects.Count; i++)
            {
                var name = keys[i];
                var obj = objects[name];
                var hash = obj.Hash;

                onProgressChanged?.Invoke(
                    new MinecraftProgressEventArgs(
                        objects.Count,
                        i + 1,
                        name
                    )
                );

                var hashHeader = hash.Substring(0, 2);
                var path = hashHeader + "/" + hash;
                var directoryBase = assetsDirectory + "/objects/";
                var directory = directoryBase + hashHeader;
                var file = directoryBase + path;
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                if (File.Exists(file)) continue;

                DownloadFile(AssetsUrlBase + path, file);
            }
        }

        /// <summary>
        /// リストからアセットをダウンロードします。
        /// </summary>
        /// <param name="index">ダウンロードするアセットのリスト</param>
        /// <param name="assetsDirectory">アセットディレクトリの場所</param>
        /// <param name="onProgressChanged">進捗状況が変化したときのイベントハンドラ</param>
        public static async Task DownloadAssetsAsync(this AssetIndex index, string assetsDirectory,
                                                     MinecraftProgressEventHandler onProgressChanged = null)
        {
            var objects = index.Objects;
            var keys = objects.Keys.ToArray();
            for (var i = 0; i < objects.Count; i++)
            {
                var name = keys[i];
                var obj = objects[name];
                var hash = obj.Hash;

                onProgressChanged?.Invoke(
                    new MinecraftProgressEventArgs(
                        objects.Count,
                        i + 1,
                        name
                    )
                );

                var hashHeader = hash.Substring(0, 2);
                var path = hashHeader + "/" + hash;
                var directoryBase = assetsDirectory + "/objects/";
                var directory = directoryBase + hashHeader;
                var file = directoryBase + path;
                var isFileExists = await Task.Run(() =>
                {
                    if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                    return File.Exists(file);
                });

                if (isFileExists) continue;
                await DownloadFileAsync(AssetsUrlBase + path, file);
            }
        }

        /// <summary>
        /// ライブラリをダウンロードします。
        /// </summary>
        /// <param name="location">ライブラリのダウンロード元</param>
        /// <param name="librariesDirectory">ライブラリディレクトリの場所</param>
        /// <returns>ライブラリをダウンロードした場所</returns>
        public static string DownloadLibrary(this LibraryDownloadLocation location, string librariesDirectory)
        {
            var file = $"{librariesDirectory}/{location.Path}";
            var directory = Path.GetDirectoryName(file);
            if (directory == null)
            {
                throw new IOException("Failed to get directory from file path: " + file);
            }

            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            if (File.Exists(file)) return file;

            DownloadFile(location.Url, file);
            return file;
        }

        /// <summary>
        /// ライブラリをダウンロードします。
        /// </summary>
        /// <param name="location">ライブラリのダウンロード元</param>
        /// <param name="librariesDirectory">ライブラリディレクトリの場所</param>
        /// <returns>ライブラリをダウンロードした場所</returns>
        public static async Task<string> DownloadLibraryAsync(this LibraryDownloadLocation location, string librariesDirectory)
        {
            var file = $"{librariesDirectory}/{location.Path}";
            var directory = Path.GetDirectoryName(file);
            if (directory == null)
            {
                throw new IOException("Failed to get directory from file path: " + file);
            }

            var isFileExists = await Task.Run(() =>
            {
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                return File.Exists(file);
            });

            if (!isFileExists) await DownloadFileAsync(location.Url, file);
            return file;
        }

        /// <summary>
        /// バージョンの詳細からライブラリをダウンロードします。
        /// </summary>
        /// <param name="detail">ライブラリをダウンロードするバージョンの詳細</param>
        /// <param name="librariesDirectory">ライブラリディレクトリの場所</param>
        /// <param name="onProgressChanged">進捗状況が変化したときのイベントハンドラ</param>
        /// <returns>クラスパスなどを含むダウンロードの結果</returns>
        public static LibrariesDownloadResult DownloadLibraries(this VersionDetail detail, string librariesDirectory,
                                                                MinecraftProgressEventHandler onProgressChanged = null)
        {
            var classpath = new List<string>();
            var extracts = new Dictionary<string, ExtractRule>();
            var libraries = detail.Libraries;
            for (var i = 0; i < libraries.Length; i++)
            {
                var library = libraries[i];

                onProgressChanged?.Invoke(
                    new MinecraftProgressEventArgs(
                        libraries.Length,
                        i + 1,
                        library.Name
                    )
                );

                var location = library.Downloads.Artifact;
                if (location != null)
                {
                    classpath.Add(location.DownloadLibrary(librariesDirectory));
                }

                var rules = library.NativeRules;
                if (rules != null)
                {
                    if (rules.ContainsWhere(r => r.Action == NativeRuleAction.Disallow) &&
                        rules.Where(r => r.Action == NativeRuleAction.Disallow)
                             .ContainsWhere(r => r.OS.Type == NativeOSType.Windows))
                    {
                        continue;
                    }

                    if (!rules.ContainsWhere(r => r.Action == NativeRuleAction.Disallow) &&
                        !rules.Where(r => r.Action == NativeRuleAction.Allow)
                            .ContainsWhere(r => r.OS.Type == NativeOSType.Windows))
                    {
                        continue;
                    }
                }

                if (library.Natives == null ||
                    !library.Natives.ContainsKey(NativeOSType.Windows)) continue;

                var arch = Environment.Is64BitOperatingSystem ? "64" : "32";
                var nativeName = library.Natives[NativeOSType.Windows].Replace("${arch}", arch);
                var classifier = library.Downloads.Classifiers[nativeName];
                extracts.Add(classifier.DownloadLibrary(librariesDirectory), library.ExtractRule);
            }

            return new LibrariesDownloadResult
            {
                Classpath = classpath,
                Extracts = extracts
            };
        }

        /// <summary>
        /// バージョンの詳細からライブラリをダウンロードします。
        /// </summary>
        /// <param name="detail">ライブラリをダウンロードするバージョンの詳細</param>
        /// <param name="librariesDirectory">ライブラリディレクトリの場所</param>
        /// <param name="onProgressChanged">進捗状況が変化したときのイベントハンドラ</param>
        /// <returns>クラスパスなどを含むダウンロードの結果</returns>
        public static async Task<LibrariesDownloadResult> DownloadLibrariesAsync(this VersionDetail detail, string librariesDirectory,
                                                                                 MinecraftProgressEventHandler onProgressChanged = null)
        {
            var classpath = new List<string>();
            var extracts = new Dictionary<string, ExtractRule>();
            var libraries = detail.Libraries;
            for (var i = 0; i < libraries.Length; i++)
            {
                var library = libraries[i];

                onProgressChanged?.Invoke(
                    new MinecraftProgressEventArgs(
                        libraries.Length,
                        i + 1,
                        library.Name
                    )
                );

                var location = library.Downloads.Artifact;
                if (location != null)
                {
                    classpath.Add(await location.DownloadLibraryAsync(librariesDirectory));
                }

                var rules = library.NativeRules;
                if (rules != null)
                {
                    if (rules.ContainsWhere(r => r.Action == NativeRuleAction.Disallow) &&
                        rules.Where(r => r.Action == NativeRuleAction.Disallow)
                             .ContainsWhere(r => r.OS.Type == NativeOSType.Windows))
                    {
                        continue;
                    }

                    if (!rules.ContainsWhere(r => r.Action == NativeRuleAction.Disallow) &&
                        !rules.Where(r => r.Action == NativeRuleAction.Allow)
                            .ContainsWhere(r => r.OS.Type == NativeOSType.Windows))
                    {
                        continue;
                    }
                }

                if (library.Natives == null ||
                    !library.Natives.ContainsKey(NativeOSType.Windows)) continue;

                var arch = Environment.Is64BitOperatingSystem ? "64" : "32";
                var nativeName = library.Natives[NativeOSType.Windows].Replace("${arch}", arch);
                var classifier = library.Downloads.Classifiers[nativeName];
                extracts.Add(await classifier.DownloadLibraryAsync(librariesDirectory), library.ExtractRule);
            }

            return new LibrariesDownloadResult
            {
                Classpath = classpath,
                Extracts = extracts
            };
        }

        /// <summary>
        /// バージョンの詳細からクライアントをダウンロードします。
        /// </summary>
        /// <param name="detail">クライアントをダウンロードするバージョンの詳細</param>
        /// <param name="versionsDirectory">バージョンディレクトリの場所</param>
        /// <returns>ダウンロードしたクライアントの場所</returns>
        public static string DownloadClient(this VersionDetail detail, string versionsDirectory)
        {
            var location = detail.Downloads[DownloadType.Client];
            var directory = $"{versionsDirectory}/{detail.Id}";
            var file = $"{directory}/{detail.Id}.jar";
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
            if (!File.Exists(file))
            {
                DownloadFile(location.Url, file);
            }

            return file;
        }

        /// <summary>
        /// バージョンの詳細からクライアントをダウンロードします。
        /// </summary>
        /// <param name="detail">クライアントをダウンロードするバージョンの詳細</param>
        /// <param name="versionsDirectory">バージョンディレクトリの場所</param>
        /// <returns>ダウンロードしたクライアントの場所</returns>
        public static async Task<string> DownloadClientAsync(this VersionDetail detail, string versionsDirectory)
        {
            var location = detail.Downloads[DownloadType.Client];
            var directory = $"{versionsDirectory}/{detail.Id}";
            var file = $"{directory}/{detail.Id}.jar";
            var isFileExists = await Task.Run(() =>
            {
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                return File.Exists(file);
            });

            if (!isFileExists)
            {
                DownloadFile(location.Url, file);
            }

            return file;
        }

        /// <summary>
        /// ネイティブライブラリを展開して配置します。
        /// </summary>
        /// <param name="extracts">展開するネイティブライブラリの場所と展開ルールのディクショナリ</param>
        /// <param name="directory">展開先ディレクトリの場所</param>
        /// <param name="onProgressChanged">進捗状況が変化したときのイベントハンドラ</param>
        public static void ExtractNatives(Dictionary<string, ExtractRule> extracts, string directory,
                                          MinecraftProgressEventHandler onProgressChanged)
        {
            var keys = extracts.Keys.ToArray();
            for (var i = 0; i < extracts.Count; i++)
            {
                var path = keys[i];
                var excludes = extracts[path].Excludes;

                onProgressChanged?.Invoke(
                    new MinecraftProgressEventArgs(
                        extracts.Count,
                        i + 1,
                        Path.GetFileName(path)
                    )
                );

                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
                using (var archive = ZipFile.OpenRead(path))
                {
                    foreach (var entry in archive.Entries)
                    {
                        if (excludes.ContainsWhere(e => entry.FullName.StartsWith(e)))
                        {
                            continue;
                        }

                        entry.ExtractToFile($"{directory}/{entry.Name}", true);
                    }
                }
            }
        }

        /// <summary>
        /// ネイティブライブラリを展開して配置します。
        /// </summary>
        /// <param name="extracts">展開するネイティブライブラリの場所と展開ルールのディクショナリ</param>
        /// <param name="directory">展開先ディレクトリの場所</param>
        /// <param name="onProgressChanged">進捗状況が変化したときのイベントハンドラ</param>
        public static async Task ExtractNativesAsync(Dictionary<string, ExtractRule> extracts, string directory,
                                                     MinecraftProgressEventHandler onProgressChanged)
        {
            var keys = extracts.Keys.ToArray();
            for (var i = 0; i < extracts.Count; i++)
            {
                var path = keys[i];
                var excludes = extracts[path].Excludes;

                onProgressChanged?.Invoke(
                    new MinecraftProgressEventArgs(
                        extracts.Count,
                        i + 1,
                        Path.GetFileName(path)
                    )
                );

                await Task.Run(() =>
                {
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    using (var archive = ZipFile.OpenRead(path))
                    {
                        foreach (var entry in archive.Entries)
                        {
                            if (excludes.ContainsWhere(e => entry.FullName.StartsWith(e)))
                            {
                                continue;
                            }

                            entry.ExtractToFile($"{directory}/{entry.Name}", true);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Mojangアカウントを認証します。
        /// </summary>
        /// <param name="username">ユーザ名</param>
        /// <param name="password">パスワード</param>
        /// <returns>アカウントのトークン</returns>
        public static Credentials Authenticate(string username, string password)
        {
            var request = new AuthenticateRequest(username, password);
            var reqJson = request.ToJson();
            var resJson = Post(AuthenticateUrl, reqJson);

            return resJson.ToObject<Credentials>();
        }

        /// <summary>
        /// Mojangアカウントを認証します。
        /// </summary>
        /// <param name="username">ユーザ名</param>
        /// <param name="password">パスワード</param>
        /// <returns>アカウントのトークン</returns>
        public static async Task<Credentials> AuthenticateAsync(string username, string password)
        {
            var request = new AuthenticateRequest(username, password);
            var reqJson = await request.ToJsonAsync();
            var resJson = await PostAsync(AuthenticateUrl, reqJson);

            return await resJson.ToObjectAsync<Credentials>();
        }

        /// <summary>
        /// Mojangアカウントのアクセストークンを検証します。
        /// </summary>
        /// <param name="credentials">検証するアクセストークン</param>
        /// <returns>アクセストークンが有効かどうか</returns>
        public static bool Validate(this Credentials credentials)
        {
            try
            {
                var request = new ValidateRequest(credentials.AccessToken, credentials.ClientToken);
                var reqJson = request.ToJson();
                Post(ValidateUrl, reqJson);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Mojangアカウントのアクセストークンを検証します。
        /// </summary>
        /// <param name="credentials">検証するアクセストークン</param>
        /// <returns>アクセストークンが有効かどうか</returns>
        public static async Task<bool> ValidateAsync(this Credentials credentials)
        {
            try
            {
                var request = new ValidateRequest(credentials.AccessToken, credentials.ClientToken);
                var reqJson = await request.ToJsonAsync();
                await PostAsync(ValidateUrl, reqJson);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private static string[] GenerateArguments(this VersionDetail detail, Credentials credentials,
                                                  IEnumerable<string> classpath, string gameDirectory,
                                                  string nativesDirectory, string assetsDirectory)
        {
            return new[]
            {
                $"-Djava.library.path={nativesDirectory}",
                $"-cp {string.Join(";", classpath)}",
                detail.MainClass,
                detail.MinecraftArguments
                    .Replace("${version_name}", detail.Id)
                    .Replace("${game_directory}", gameDirectory)
                    .Replace("${assets_root}", assetsDirectory)
                    .Replace("${assets_index_name}", detail.AssetsId)
                    .Replace("${auth_access_token}", credentials.AccessToken)
                    .Replace("${auth_uuid}", credentials.SelectedProfile.Id)
                    .Replace("${auth_player_name}", credentials.SelectedProfile.Name)
                    .Replace("${user_type}", "mojang")
                    .Replace("${version_type}", detail.Type.ToString().ToLower())
                    .Replace("${user_properties}", "{}")
            };
        }

        /// <summary>
        /// Minecraftを起動します。
        /// </summary>
        /// <param name="detail">起動するバージョンの詳細</param>
        /// <param name="credentials">アカウントのトークン</param>
        /// <param name="java">Javaの場所</param>
        /// <param name="classpath">追加するクラスパスのリスト</param>
        /// <param name="waitForExit">終了を待つかどうか</param>
        /// <param name="onOutput">標準出力に書き込まれた時のイベントハンドラ</param>
        public static void Launch(this VersionDetail detail, Credentials credentials,
                                  string java, IEnumerable<string> classpath, string gameDirectory,
                                  string nativesDirectory, string assetsDirectory,
                                  bool waitForExit = true, DataReceivedEventHandler onOutput = null)
        {
            var arguments = detail.GenerateArguments(
                credentials,
                classpath,
                gameDirectory,
                nativesDirectory,
                assetsDirectory
            );
            
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = java,
                    Arguments = string.Join(" ", arguments),
                    CreateNoWindow = true,
                    UseShellExecute = false,
                }
            };

            if (onOutput != null)
            {
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.OutputDataReceived += onOutput;
                process.ErrorDataReceived += onOutput;
            }

            process.Start();

            if (onOutput != null)
            {
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
            }

            if (waitForExit)
            {
                process.WaitForExit();
            }
        }
    }
}