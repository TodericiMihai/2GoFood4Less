using _2GoFood4Less.Server.Models.OrderObjects;
using _2GoFood4Less.Server.Models.Utils.Enums;

namespace _2GoFood4Less.Server.Services.CartServices.CartCommands.CartCommandsDto
{
    public class OrderLocationPayment
    {

        public PaymentMethod PaymentMethod { get; set; }
        public int Value { get; set; }
        public string Location { get; set; }
    }
}
