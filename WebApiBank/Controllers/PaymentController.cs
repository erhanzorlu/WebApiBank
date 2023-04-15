using System;
using System.Linq;
using System.Web.Http;
using WebApiBank.Models.Context;
using WebApiBank.Models.Entities;
using WebApiBank.RequestModel;

namespace WebApiBank.Controllers
{
    public class PaymentController : ApiController
    {
        MyContext _db;
        public PaymentController()
        {
            _db = new MyContext();
        }


        //[HttpGet]
        //public List<PaymentResponseModel> GetAll()
        //{
        //    return _db.Cards.Select(x => new PaymentResponseModel
        //    {
        //        CardExpiryMonth=x.CardExpiryMonth,
        //        CardExpiryYear=x.CardExpiryYear,
        //        CardNumber=x.CardNumber,

        //    }).ToList();
        //}

        [HttpPost]
        public IHttpActionResult ReceivePayment(PaymentRequestModel item)
        {
            CardInfo ci = _db.Cards.FirstOrDefault(x => x.CardNumber == item.CardNumber && x.SecurityNumber == item.SecurityNumber && x.CardUserName == item.CardUserName && x.CardExpiryYear == item.CardExpiryYear && x.CardExpiryMonth == item.CardExpiryYear);

            if (ci != null)
            {
                if (ci.CardExpiryYear<DateTime.Now.Year)
                {
                    return BadRequest("Expired Card");
                }
                else if (ci.CardExpiryYear==DateTime.Now.Year)

                {
                    if (ci.CardExpiryMonth<DateTime.Now.Month)
                    {
                        return BadRequest("Expired Card(Month)");
                    }
                    if (ci.Balance>=item.ShoppingPrice)
                    {
                        SetBalance(item, ci);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("Balance exceeded");
                    }
                }
                else if (ci.Balance>=item.ShoppingPrice)
                {
                    SetBalance(item, ci);
                    return Ok();
                }
                return BadRequest("Balance not enough");
            }
            return BadRequest("Card info wrong");
        }


        private void SetBalance(PaymentRequestModel item,CardInfo ci)
        {
            ci.Balance-=item.ShoppingPrice;
            
            _db.SaveChanges();
        }

    }
}
