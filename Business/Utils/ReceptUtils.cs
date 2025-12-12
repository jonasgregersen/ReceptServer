using DTORecept = DataTransferLayer.Models.Recept;
namespace Business.Utils
{
    public class ReceptUtils
    {
        public static bool AlleOrdinationerUdleveret(DTORecept recept)
        {
            if (recept == null)
            {
                throw new ArgumentException("Recept not found");
            }
            foreach (var ordination in recept.Ordinationer)
            {
                if (ordination.FortagneUdleveringer < ordination.Udleveringer)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
