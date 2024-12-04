using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Core
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps("Project.Features");
            });

            return mapperConfig;
        }
    }
}