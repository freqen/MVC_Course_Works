using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class QueryModel
    {
        public class CustomerQuery
        {
            public string 客戶名稱 { get; set; }
            public string 統一編號 { get; set; }
            public string 電話 { get; set; }
            public string 傳真 { get; set; }
            public string 地址 { get; set; }
            public string Email { get; set; }
            public string 客戶分類 { get; set; }
        }

        public class CustomerContactQuery
        {
            public int? 客戶Id { get; set; }
            public string 職稱 { get; set; }
            public string 姓名 { get; set; }
            public string Email { get; set; }
            public string 手機 { get; set; }
            public string 電話 { get; set; }
            public bool 刪除註記 { get; set; }
        }

        public class CustomerBankAccountQuery
        {
            public int 客戶Id { get; set; }
            public string 銀行名稱 { get; set; }
            public int 銀行代碼 { get; set; }
            public Nullable<int> 分行代碼 { get; set; }
            public string 帳戶名稱 { get; set; }
            public string 帳戶號碼 { get; set; }
        }

        public class ProductQM
        {
            public bool DoQuery { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
        }
    }
}