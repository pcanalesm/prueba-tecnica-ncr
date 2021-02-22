using M2Mqtt;
using Microsoft.Extensions.Configuration;
using NcrTestSub.Dto;

namespace NcrTestSub.Bo.IBo
{
    public interface ICommandBo
    {
        MqttClient processCommand(IConfiguration config);
        CommandDto processMessage(string jsonMessage);
        void executeCommand(CommandDto command, IConfiguration config);
    }
}
