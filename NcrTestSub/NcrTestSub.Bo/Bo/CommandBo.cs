using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using M2Mqtt;
using NcrTestSub.Bo.IBo;
using NcrTestSub.Dto;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using NLog;

namespace NcrTestSub.Bo.Bo
{
    internal sealed class CommandBo : ICommandBo
    {

        private string idClient;

        public static Logger logger = LogManager.GetCurrentClassLogger();

        #region Singleton

        private CommandBo()
        {

        }

        /// <summary>
        /// Lazy singleton
        /// </summary>
        private static readonly Lazy<ICommandBo> instance = new Lazy<ICommandBo>(() => new CommandBo());

        public static ICommandBo GetInstance => instance.Value;


        #endregion

        #region Interface Methods

        /// <summary>
        /// Create a new MQTT Server Connection using values in AppSetting.json
        /// </summary>
        /// <param name="config">Iconfiguration object initialized</param>
        /// <returns>Request a new MqttCient connected</returns>
        public MqttClient processCommand(IConfiguration config)
        {

            if(config == null)
            {
                throw new Exception("Configguration settings must be initialized");
            }

            MqttClient client = new MqttClient(config.GetSection("mqtt_uri").Value);
            this.idClient = Guid.NewGuid().ToString();

            client.Connect(idClient);

            return client;


        }

        /// <summary>
        /// Transform json String message into object class
        /// </summary>
        /// <param name="jsonMessage">Json string message</param>
        /// <returns>CommandDto Object from json string</returns>
        public CommandDto processMessage(string jsonMessage)
        {
            CommandDto command = JsonSerializer.Deserialize<CommandDto>(jsonMessage);

            return command;
        }


        /// <summary>
        /// Process and register commands 
        /// </summary>
        /// <param name="command">command object</param>
        /// <param name="config">Iconfiguration object initialized</param>
        public void executeCommand(CommandDto command, IConfiguration config)
        {


            if (config == null)
            {
                throw new Exception("Configguration settings must be initialized");
            }

            CommandOptions commandOptions = getCommandOptions(command, config);

            if(commandOptions == null)
            {
                Console.WriteLine(config.GetSection("invalid_command").Value);
                return;
            }

            registerLogCommand(command);

            System.Threading.Thread.Sleep(1000);

            switch (commandOptions.type)
            {
                case CommandOptionsType.CONSOLE:
                    Console.WriteLine(commandOptions.text);
                    break;
                case CommandOptionsType.PROCESS:
                    Process.Start(commandOptions.text, commandOptions.args);
                    break;
            }
        }





        #endregion

        /// <summary>
        /// Get CommandOptions object 
        /// </summary>
        /// <param name="command">CommandDto object</param>
        /// <param name="config">IConfiguration object initialized</param>
        /// <returns>new CommandOptions Object</returns>
        private CommandOptions getCommandOptions(CommandDto command, IConfiguration config)
        {

            if (config == null)
            {
                throw new Exception("Configguration settings must be initialized");
            }


            IList<IConfigurationSection> configs = config.GetSection("valid_commands").GetChildren().ToArray().Select(c => c).ToList();

            return configs.Where(c => c.GetSection("name").Value.Equals(command.command))
                          .Select(c => new CommandOptions(c.GetSection("name").Value,
                                                          c.GetSection("text").Value,
                                                          c.GetSection("args").Value,
                                                          getEnum(c.GetSection("type").Value)
                                                          )).FirstOrDefault();
        }

        /// <summary>
        /// Generate Enum from string value in AppSetting.json
        /// </summary>
        /// <param name="name">key value, must match with enum name (ignore case)</param>
        /// <returns></returns>
        private CommandOptionsType getEnum(string name)
        {
            return (CommandOptionsType)Enum.Parse(typeof(CommandOptionsType), name, true);
        }


        /// <summary>
        /// Register command into log file
        /// </summary>
        /// <param name="command">command to register</param>
        private void registerLogCommand(CommandDto command)
        {
            logger.Info(string.Format("command: {0} - id: {1} - sender: {2}", command.command, this.idClient, command.sender));
        }
    }
}
