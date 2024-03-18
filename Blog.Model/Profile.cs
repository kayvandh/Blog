using AutoMapper;
using Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model
{
    public class DefaultProfile : AutoMapper.Profile
    {
        public DefaultProfile()
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();


            var mapsFrom = types.Where(t => !t.IsAbstract && !t.IsInterface).SelectMany(t =>
                            t.GetInterfaces().Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDefaultMapFrom<>))
                                .Select(
                                    i =>
                                        new
                                        {
                                            Source = i.GetGenericArguments()[0],
                                            Destination = t
                                        })).ToList();


            foreach (var map in mapsFrom)
            {
                CreateMap(map.Source, map.Destination);
            }

            var mapsTo = types.Where(t => !t.IsAbstract && !t.IsInterface).SelectMany(t =>
                        t.GetInterfaces()
                            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IDefaultMapTo<>))
                            .Select(
                                i =>
                                    new
                                    {
                                        Destination = i.GetGenericArguments()[0],
                                        Source = t
                                    })).ToList();


            foreach (var map in mapsTo)
            {
                CreateMap(map.Source, map.Destination);
            }

            var maps = types.Where(t => typeof(ICustomMap).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                .Select(t => (ICustomMap)Activator.CreateInstance(t)).ToList();

            foreach (var map in maps)
            {
                if (map != null) map.CustomMappings(this);
            }
        }
    }
}
