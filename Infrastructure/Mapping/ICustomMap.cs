using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapping
{
    public interface ICustomMap
    {
        void CustomMappings(AutoMapper.Profile profile);
    }
}
