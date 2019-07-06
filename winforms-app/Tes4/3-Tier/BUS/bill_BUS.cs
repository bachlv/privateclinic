using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tes4._3_Tier.DTO;
using Tes4._3_Tier.DAL;

namespace Tes4._3_Tier.BUS
{
    class bill_BUS
    {
        private billDAL billDAL;
        public bill_BUS()
        {
            billDAL = new billDAL();
            billDAL.InitializeDB();
        }
        public int Insert(int medre_id)
        {
            return billDAL.Insert(medre_id);
        }
        public void Delete(int bill_ID)
        {
            billDAL.Delete(bill_ID);
        }
    }
}
