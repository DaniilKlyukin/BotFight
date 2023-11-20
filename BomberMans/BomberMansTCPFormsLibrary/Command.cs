using Newtonsoft.Json.Linq;

namespace BomberMansTCPFormsLibrary
{
    public abstract class Command
    {
        public abstract string ConvertToJson();

        public static Command ParseFromJson(string jsonString)
        {
            var json = JObject.Parse(jsonString);
            var command = (Commands)json.Value<int>("Command");

            switch (command)
            {
                case Commands.SendPlayerName:
                    {
                        return SendPlayerNameCommand.ParseFromJson(json.ToString());
                    }
                case Commands.SendMap:
                    {
                        return SendMapCommand.ParseFromJson(json.ToString());
                    }
                case Commands.SendPlayerAction:
                    {
                        return SendPlayerActionCommand.ParseFromJson(json.ToString());
                    }
            }

            throw new NotImplementedException();
        }
    }
}