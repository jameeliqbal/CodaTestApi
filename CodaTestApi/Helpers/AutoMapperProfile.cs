using AutoMapper;
using CodaTestApi.Entities;
using CodaTestApi.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodaTestApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateEventRequest, Event>();

            CreateMap<UpdateEventRequest, Event>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                {
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;
                    if (prop.GetType() == typeof(DateTime) &&   ((DateTime)prop).Date==DateTime.MinValue) return false;

                    return true;
                }
                ));
         }
    }
}
