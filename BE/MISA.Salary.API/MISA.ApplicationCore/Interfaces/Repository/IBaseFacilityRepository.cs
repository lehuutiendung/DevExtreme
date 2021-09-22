using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Interfaces.Repository
{
    public interface IBaseFacilityRepository<FacilityEntity>
    {
        List<Object> GetAll();

        Object GetById(Guid facilityEntityId);
    }
}
