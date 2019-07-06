using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tes4._3_Tier.DTO;
using Tes4._3_Tier.DAL;

namespace Tes4._3_Tier.BUS
{
    class billItem_BUS
    {
        private billItem_DAL bill_ItemDAL;
        public billItem_BUS()
        {
            bill_ItemDAL = new billItem_DAL();
            bill_ItemDAL.InitializeDB();
        }
        public void Insert(int billId, String name, String un, int quan,String use, float pr)
        {
            bill_ItemDAL.Insert(billId, name, un, quan, use,pr);
        }
    }
}
