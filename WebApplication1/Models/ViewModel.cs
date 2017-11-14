using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ViewModel
    {
        public class CustomerVM
        {
            public string 客戶名稱 { get; set; }
            public int 聯絡人數量 { get; set; }
            public int 銀行帳戶數量 { get; set; }
        }

        public class ProductCreationVM : IValidatableObject
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public Nullable<decimal> Price { get; set; }
            public int OrderLineCount { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                if (!this.ProductName.Contains("台灣"))
                {
                    yield return new ValidationResult("台灣只能有一個", new string[] { "ProductName" });
                }
            }
        }
    }
}