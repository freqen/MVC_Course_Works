using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models.DateTypeAttributes
{
    public class ValidateTaiwanAddressAttribute : DataTypeAttribute
    {
        public ValidateTaiwanAddressAttribute() 
            : base(DataType.Text)
        {
            ErrorMessage = "地址必須出現【台灣】字樣!";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            string str = value as String;
            return str.Contains("台灣") || str.Contains("臺灣");
        }
    }
}