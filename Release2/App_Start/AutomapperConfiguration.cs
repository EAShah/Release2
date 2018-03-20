using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project._1.Models;
using Release2.Models;
using Release2.ViewModels;

namespace Release2.App_Start
    {
        /// <summary>
        /// Configuration of AutoMapper class
        /// Add all the required mappings between model and view models here
        /// </summary>
        public static class AutoMapperConfig
        {
            public static void RegisterMappings()
            {
                AutoMapper.Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Colleague, ColleagueViewModel>().ReverseMap();
                    cfg.CreateMap<ProbationaryColleague, ProbationaryColleagueViewModel>().ReverseMap();
                });
            }
        }
    }

