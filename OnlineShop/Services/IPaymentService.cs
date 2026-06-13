using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FiveStadium.Dtos;
using FiveStadium.Enums;
using FiveStadium.Models;

namespace FiveStadium.Services
{
     public  interface IPaymentService
    {
        public Task<string> CreatePaymentSession(Order order);
        public Task<Order> UpdateOrderStatus(string sessionId, OrderStatus status);

    }
}
