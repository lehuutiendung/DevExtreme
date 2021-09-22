using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entities;
using MISA.ApplicationCore.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repository
{
    public class PolicyRepository : BaseRepository<Policy>, IPolicyRepository
    {
        public PolicyRepository(IConfiguration configuration) : base(configuration)
        {

        }

    }
}
