using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Satobot.Extensions;
using Satobot.SatoshiMines.Objects;

namespace Satobot.SatoshiMines
{
    public class SatoshiMinesSession : IDisposable
    {
        public string PlayerHash { get; }
        public bool Authenticated { get; set; }

        private readonly HttpClient Client;
        private readonly CultureInfo Culture;

        public SatoshiMinesSession(string playerHash)
        {
            if (string.IsNullOrWhiteSpace(playerHash))
                throw new ArgumentNullException(playerHash);

            PlayerHash = playerHash;
            Authenticated = false;

            Client = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                AllowAutoRedirect = true,
                UseCookies = true
            })
            {
                BaseAddress = new Uri("https://satoshimines.com/"),
                DefaultRequestHeaders =
                {
                    {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/61.0.3163.100 Safari/537.36"},
                    {"Referer", $"https://satoshimines.com/play/{PlayerHash}/"}
                }
            };

            Culture = CultureInfo.GetCultureInfo("en");
        }

        public async Task<bool> Login(string password)
        {
            var page = await Client.GetStringAsync($"play/{PlayerHash}/");

            if (page.Contains("start_game"))
            {
                return true;
            }
            else
            {
                var content = new FormUrlEncodedContent(new ValueCollection
                {
                    {"secret", PlayerHash},
                    {"pw", password}
                });

                var response = await Client.GetStringFromPostAsync("action/login.php", content);

                return response.Contains("start_game");
            }
        }

        public async Task<GameObject> NewGame(int bet, MineCount mines)
        {
            if (bet != 0 && bet < 30 || bet > 1000000)
                throw new ArgumentOutOfRangeException(nameof(bet));

            var content = new FormUrlEncodedContent(new ValueCollection
            {
                {"bd", "12"},
                {"player_hash", PlayerHash},
                {"bet", BitsToBtc(bet).ToString("0.000000", Culture)},
                {"num_mines", GetMinesFromEnum(mines)}
            });

            var json = await Client.GetStringFromPostAsync("action/newgame.php", content);

            return JsonUtil.Deserialize<GameObject>(json);
        }

        public async Task<TileObject> GuessTile(GameObject game, int tile)
        {
            if (string.IsNullOrWhiteSpace(game?.GameHash))
                throw new ArgumentNullException(nameof(game));
            if (tile < 1 || tile > 25)
                throw new ArgumentOutOfRangeException(nameof(tile));

            var content = new FormUrlEncodedContent(new ValueCollection
            {
                {"game_hash", game.GameHash},
                {"guess", tile.ToString(Culture)},
                {"v04", "1"}
            });

            var json = await Client.GetStringFromPostAsync("action/checkboard.php", content);

            return JsonUtil.Deserialize<TileObject>(json);
        }

        public async Task<CashoutGameObject> CashoutGame(GameObject game)
        {
            if (string.IsNullOrWhiteSpace(game?.GameHash))
                throw new ArgumentNullException(nameof(game));

            var content = new FormUrlEncodedContent(new ValueCollection
            {
                {"game_hash", game.GameHash}
            });

            var json = await Client.GetStringFromPostAsync("action/cashout.php", content);

            return JsonUtil.Deserialize<CashoutGameObject>(json);
        }

        // fix with csrf
        public async Task<BalanceObject> FullCashout(string address, int amount)
        {
            var content = new FormUrlEncodedContent(new ValueCollection
            {
                {"secret", PlayerHash},
                {"payto_address", address},
                {"amount", BitsToBtc(amount).ToString("0.000000", Culture)}
            });

            var json = await Client.GetStringFromPostAsync("action/full_cashout.php", content);

            return JsonUtil.Deserialize<BalanceObject>(json);
        }

        public async Task<BalanceObject> GetBalance()
        {
            var content = new FormUrlEncodedContent(new ValueCollection
            {
                {"secret", PlayerHash}
            });

            var json = await Client.GetStringFromPostAsync("action/refresh_balance.php", content);

            return JsonUtil.Deserialize<BalanceObject>(json);
        }

        public bool CheckFairGame(string mines, string randomString, string secret)
        {
            using (var sha = new SHA256Managed())
            {
                var hashBytes = sha.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(mines, '-', randomString)));
                var hashString = BitConverter.ToString(hashBytes).Replace("-", string.Empty).ToLowerInvariant();
                return hashString.Equals(secret);
            }
        }

        public string GetGameUrl(TileObject game)
        {
            return string.Format("https://satoshimines.com/s/{0}/{1}/", game.GameId, game.RandomString);
        }

        public string GetGameUrl(CashoutGameObject game)
        {
            return string.Format("https://satoshimines.com/s/{0}/{1}/", game.GameId, game.RandomString);
        }

        public bool Successful(RootObject obj)
        {
            return obj.Status.Equals("success");
        }

        public static double BitsToBtc(int bits)
        {
            return bits/1000000D;
        }

        public static double BtcToBits(double btc)
        {
            return btc*1000000D;
        }

        public static string GetMinesFromEnum(MineCount mines)
        {
            switch (mines)
            {
                default:
                    return "1";
                case MineCount.Three:
                    return "3";
                case MineCount.Five:
                    return "5";
                case MineCount.TwentyFour:
                    return "24";
            }
        }

        public void Dispose()
        {
            Client.Dispose();
        }
    }
}
