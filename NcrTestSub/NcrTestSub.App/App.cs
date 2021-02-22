using M2Mqtt;
using M2Mqtt.Messages;
using Microsoft.Extensions.Configuration;
using NcrTestSub.Bo.Factory;
using NcrTestSub.Dto;
using System;
using System.Text;

namespace NcrTestSub.App
{
    public class App
    {
        private readonly IConfiguration config;


        public App(IConfiguration config)
        {
            this.config = config;
        }

        /// <summary>
        /// Run App
        /// </summary>
        public void Run()
        {
            FactoryBo factory = FactoryBo.GetInstance;

            //Connect to MQTT Server
            MqttClient client = factory.CreateInstanceCommandBo.processCommand(config);

            //Check connection an register callback event when message was received
            if(client.IsConnected)
            {
                client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

                //subscribe to the server with topci id
                client.Subscribe(new String[] { config.GetSection("topic").Value }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
            }

            Console.WriteLine("****** WAITING FOR COMMANDS *****");
            Console.ReadLine();
        }

        /// <summary>
        /// Event (callback) runs when a message was received
        /// </summary>
        /// <param name="sender">sender object that raised the event</param>
        /// <param name="e">contains all event data</param>
        private void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //Get Message
            string message = Encoding.UTF8.GetString(e.Message);


            FactoryBo factory = FactoryBo.GetInstance;
            //Process and parse message into object class
            CommandDto command = factory.CreateInstanceCommandBo.processMessage(message);

            //execute the received command
            factory.CreateInstanceCommandBo.executeCommand(command, config);



        }
    }
}
