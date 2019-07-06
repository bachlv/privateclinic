using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tes4._3_Tier.DTO;
using Tes4._3_Tier.DAL;
namespace Tes4._3_Tier.BUS
{
    class TreatmentBUS
    {
        private TreatmentDAL treatDAL;
        public TreatmentBUS()
        {
            treatDAL = new TreatmentDAL();
            treatDAL.InitializeDB();
        }

        public TreatmentDTO Insert(String n, String syn)
        {
            return treatDAL.Insert(n,syn);
        }
        public void Delete(int id)
        {
            treatDAL.Delete(id);
        }
        public void Update(int id, String n, String syn)
        {
            treatDAL.Update(id, n,syn);
        }
        public List<TreatmentDTO> GetTreatments()
        {
            return treatDAL.GetTreatments();
        }
        public List<string> GetNameDisease_Suggestion()
        {
            return treatDAL.GetNameDisease_Suggestion();
        }
        public List<string> GetSymptom_Suggestion()
        {
            return treatDAL.GetSymptom_Suggestion();
        }
        public string Get_Symtom(string dieukien)
        {
            return treatDAL.Get_Symtom(dieukien);
        }
        public List<string> GetNameDisease_Owner()
        {
            return treatDAL.GetNameDisease_Owner();
        }
        public List<string> GetSymptom_Owner()
        {
            return treatDAL.GetSymptom_Owner();
        }
    }
}
