using AutoMapper;
using CodaTestApi.Entities;
using CodaTestApi.Helpers;
using CodaTestApi.Models.Events;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodaTestApi.Services
{
    public interface IEventService
    {
        Task<PagedResponse<List<Event>>> GetAll(PaginationFilter filter, string route);
        Task<Event> GetById(int id);
        Task<Event> Create(CreateEventRequest model);
        Task Update(int id, UpdateEventRequest model);
        Task Delete(int id);
    }

    public class EventService : IEventService
    {
        private DataContext context;
        private readonly IMapper mapper;
        private IUriService uriService;

        public EventService(DataContext context, IMapper mapper, IUriService uriService)
        {
            this.context = context;
            this.mapper = mapper;
            this.uriService = uriService;
        }

        public async Task<PagedResponse<List<Event>>> GetAll(PaginationFilter filter, string route)
        {
            
            var validFilter = new PaginationFilter { PageNumber = filter.PageNumber, PageSize = filter.PageSize };
            var pagedData = await context.Events
                                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                                .Take(validFilter.PageSize)
                                .ToListAsync();
            var totalRecords = await context.Events.CountAsync();
            var pagedResponse = PaginationHelper.CreatePagedReponse<Event>(pagedData, validFilter, totalRecords, uriService, route);
            return pagedResponse;

            //return new PagedResponse<IEnumerable<Event>>(pagedData,validFilter.PageNumber,validFilter.PageSize);
        }

        public async Task<Event> GetById(int id)
        {
            return await getEvent(id);
        }       

        public async Task<Event> Create(CreateEventRequest model)
        {
            if (string.IsNullOrEmpty(model.Title))
                throw new AppException("Title Cannot be empty");

            if (context.Events.Any(e => e.Title == model.Title))
                throw new AppException($"Event with the title '{model.Title}' already exists!");

            if (model.StartOn < DateTime.Now)
                throw new AppException("Start Date & Time should not be less than present time");

            if (model.EndOn < model.StartOn)
                throw new AppException("End Date & Time should not be less than Start Date & Time");

            var newEvent = mapper.Map<Event>(model);

            await context.Events.AddAsync(newEvent);
            await context.SaveChangesAsync();
            return newEvent;
        }

        public async Task Update(int id, UpdateEventRequest model)
        {
            var existingEvent = await getEvent(id);

            if (model.StartOn > DateTime.MinValue && model.StartOn < DateTime.Now)
                throw new AppException("Start Date & Time should not be less than present time");

            if (model.StartOn > DateTime.MinValue && model.EndOn < model.StartOn)
                throw new AppException("End Date & Time should not be less than Start Date & Time");

            mapper.Map(model, existingEvent);
            context.Events.Update(existingEvent);
            await context.SaveChangesAsync();

        }

        public async Task Delete(int id)
        {
            var selectedEvent = await getEvent(id);
            context.Events.Remove(selectedEvent);
            await context.SaveChangesAsync();
        }

        #region private methods

        private async Task<Event> getEvent(int id)
        {
            var @event = await context.Events.FindAsync(id);
            if (@event == null) throw new KeyNotFoundException("Event not found");
            return @event;
        }

        #endregion
    }
}
