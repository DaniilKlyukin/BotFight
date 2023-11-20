using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace BomberMansTCPFormsLibrary
{
    public class SendPlayerActionCommand : Command
    {
        public PlayerAction Action { get; }

        public SendPlayerActionCommand(PlayerAction act)
        {
            Action = act;
        }

        public override string ConvertToJson()
        {
            var json = new JObject();
            json["Command"] = ((int)Commands.SendPlayerAction).ToString();
            json["Act"] = ((int)Action).ToString();

            return Regex.Replace(json.ToString(), @"\s", "");
        }

        public static SendPlayerActionCommand ParseFromJson(string jsonString)
        {
            var j = JObject.Parse(jsonString);
            var act = (PlayerAction)j.Value<int>("Act");

            return new SendPlayerActionCommand(act);
        }
    }
}
