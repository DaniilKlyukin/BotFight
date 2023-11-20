using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace BomberMansTCPFormsLibrary
{
    public class SendPlayerNameCommand : Command
    {
        public SendPlayerNameCommand(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override string ConvertToJson()
        {
            var json = new JObject();
            json["Command"] = ((int)Commands.SendPlayerName).ToString();
            json["Name"] = Name;

            return Regex.Replace(json.ToString(), @"\s", "");
        }

        public static SendPlayerNameCommand ParseFromJson(string jsonString, int maxNameLength = 30)
        {
            var j = JObject.Parse(jsonString);
            var name = j.Value<string>("Name");
            if (name.Length > maxNameLength)
                name = name.Substring(0, maxNameLength);

            return new SendPlayerNameCommand(name);
        }
    }
}