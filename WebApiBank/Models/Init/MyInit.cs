using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApiBank.Models.Context;
using WebApiBank.Models.Entities;

namespace WebApiBank.Models.Init
{
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            CardInfo cardInfo = new CardInfo()
            {
                CardUserName = "Erdal Göksen",
                CardNumber="1111 1111 1111 1111",
                CardExpiryYear=2024,
                CardExpiryMonth=12,
                SecurityNumber="222",
                Limit=50000,
                Balance=50000,
            };
            context.Cards.Add(cardInfo);
            context.SaveChanges();
            
        }
    }
}