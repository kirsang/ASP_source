﻿using Microsoft.EntityFrameworkCore;
using MyFirstSite.Domain.Entities;
using MyFirstSite.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstSite.Domain.Repositories.EntityFramework
{
    public class EFServiceItemsRepository : IServiceItemsRepository
    {
        private readonly AppDbContext context;
        public EFServiceItemsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IQueryable<ServiceItem> GetServiceItems()
        {
            return context.ServiceItems;
        }
        public ServiceItem GetServiceItemById(Guid id)
        {
            return context.ServiceItems.FirstOrDefault(x => x.Id == id);
        }
        public void SaveServiceItem(ServiceItem entity)
        {
            if (entity == default)
            {
                context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            context.SaveChanges();
        }
        public void DeleteServiceItem(Guid id)
        {
            context.Remove(new ServiceItem() { Id = id});
            context.SaveChanges();
        }

        public IQueryable<ServiceItem> GetServiceItem()
        {
            throw new NotImplementedException();
        }

        public ServiceItem GetTextFieldById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
