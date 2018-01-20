using System.IO;
using System.Threading.Tasks;
using Satobot.Objects;
using Satobot.SatoshiMines;

namespace Satobot.Misc
{
    internal class StrategySerializer
    {
        public static async void Save(StreamWriter file, Strategy details)
        {
            await file.WriteLineAsync(Messages.GameFileWarning);
            await file.WriteAsync(JsonUtil.Serialize(details));
        }

        public static async Task<Strategy> Read(StreamReader file)
        {
            await file.ReadLineAsync();
            var data = await file.ReadLineAsync();

            try
            {
                return JsonUtil.Deserialize<Strategy>(data);
            }
            catch
            {
                return null;
            }
        }
    }
}
