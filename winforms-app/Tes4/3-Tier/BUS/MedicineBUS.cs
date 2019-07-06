using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tes4._3_Tier.DTO;
using Tes4._3_Tier.DAL;
namespace Tes4._3_Tier.BUS
{
    class MedicineBUS
    {
        private MedicineDAL medDAL;
        public MedicineBUS()
        {
            medDAL = new MedicineDAL();
            medDAL.InitializeDB();
        }

        public MedicineDTO Insert(String n, String quantity, float price)
        {
            return medDAL.Insert(n, quantity, price);
        }
        public void Delete(int id)
        {
            medDAL.Delete(id);
        }
        public void Update(int id, String n, String quantity, float price)
        {
            medDAL.Update(id, n, quantity, price);
        }
        public List<MedicineDTO> GetMedicines()
        {
            return medDAL.GetPatients();
        }
        public List<string> GetMedicines_Suggestion()
        {
            return medDAL.GetMedicines_Suggestion();
        }
        public String get_Quantity(String NameOfPill)
        {
            return medDAL.get_Quantity(NameOfPill);
        }
        public float get_price(String NameOfPill)
        {
            return medDAL.get_price(NameOfPill);
        }
        public List<string> GetMedicines_Owner()
        {
            return medDAL.GetMedicines_Owner();
        }
    }
}
