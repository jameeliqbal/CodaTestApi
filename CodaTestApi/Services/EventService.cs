using AutoMapper;
using CodaTestApi.Entities;
using CodaTestApi.Helpers;
using CodaTestApi.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodaTestApi.Services
{
    public interface IEventService
    {
        IEnumerable<Event> GetAll();
        Event GetById(int id);
        void Create(CreateEventRequest model);
        void Update(int id, UpdateEventRequest model);
        void Delete(int id);
    }

    public class EventService : IEventService
    {
        private DataContext context;
        private readonly IMapper mapper;

        public EventService(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<Event> GetAll()
        {
            return context.Events;
        }

        public Event GetById(int id)
        {
            return getEvent(id);
        }       

        public void Create(CreateEventRequest model)
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

            context.Events.Add(newEvent);
            context.SaveChanges();
        }

        public void Update(int id, UpdateEventRequest model)
        {
            var existingEvent = getEvent(id);

            if (model.StartOn < DateTime.Now)
                throw new AppException("Start Date & Time should not be less than present time");

            if (model.EndOn < model.StartOn)
                throw new AppException("End Date & Time should not be less than Start Date & Time");

            mapper.Map(model, existingEvent);
            context.Events.Update(existingEvent);
            context.SaveChanges();

        }

        public void Delete(int id)
        {
            var selectedEvent = getEvent(id);
            context.Events.Remove(selectedEvent);
            context.SaveChanges();
        }

        #region private methods

        private Event getEvent(int id)
        {
            var @event = context.Events.Find(id);
            if (@event == null) throw new KeyNotFoundException("Event not found");
            return @event;
        }

        #endregion
    }
}
