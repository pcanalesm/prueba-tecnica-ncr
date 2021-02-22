using System;

namespace NcrTestSub.Dto
{
    [Serializable]
    public class CommandDto
    {

        public string sender { get; set; }
        public string destination { get; set; }
        public string command { get; set; }


        public CommandDto()
        {

        }
        public CommandDto(string sender, string destination, string command)
        {

            this.sender = sender;
            this.destination = destination;
            this.command = command;

        }


        public override bool Equals(object obj)
        {
            CommandDto compareObject = (CommandDto)obj;

            return (this.command.Equals(compareObject.command) &&
                    this.destination.Equals(compareObject.destination) &&
                    this.sender.Equals(compareObject.sender));

        }
    }
}
