using System.ComponentModel;

namespace General.Core.Entities
{
    public static class Enums
    {
        [System.Flags]
        public enum RequestTypes
        {
            [Description("Create")]
            Create = 1,
            [Description("Read")]
            Read = 2,
            [Description("Update")]
            Update = 3,
            [Description("Delete")]
            Delete = 4,
            [Description("List")]
            List = 5,
            [Description("Any")]
            Any = 7,
            [Description("Token")]
            Token = 8
        }

        public enum Status
        {
            [Description("Passive")]
            Passive = 0,
            [Description("Active")]
            Active = 1,
           
        }

        public enum ActionStatus
        {
            [Description("Wait")]
            Wait = 0,
            [Description("Close")]
            Close = 1,
            [Description("Send")]
            Send = 3,
            [Description("Reject")]
            Reject = 4,
        }
       
    }
}
