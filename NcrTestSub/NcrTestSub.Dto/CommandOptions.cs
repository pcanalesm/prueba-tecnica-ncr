using System;
using System.Collections.Generic;
using System.Text;

namespace NcrTestSub.Dto
{
    [Serializable]
    public class CommandOptions
    {
        public string name { get; set; }
        public string text { get; set; }
        public string args { get; set; }
        public CommandOptionsType type { get; set; }


        public CommandOptions()
        {

        }

        public CommandOptions(string name, string text, string args, CommandOptionsType type)
        {
            this.name = name;
            this.text = text;
            this.args = args;
            this.type = type;
        }
    }


    public enum CommandOptionsType
    {
        CONSOLE, PROCESS
    }
}
