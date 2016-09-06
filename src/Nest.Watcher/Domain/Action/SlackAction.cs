using System.Collections.Generic;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
    [JsonObject]
    [JsonConverter(typeof(ReadAsTypeConverter<SlackAction>))]
    public interface ISlackAction : IAction
    {
    }

    public class SlackAction : Action, ISlackAction
    {
        public string Account { get; set; }
        public Message Message { get; set; }
    }

    [JsonObject]
    public class Message
    {
        [JsonProperty("to")]
        public IList<string> To { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}