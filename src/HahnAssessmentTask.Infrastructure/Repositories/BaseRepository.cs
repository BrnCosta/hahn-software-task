﻿using HahnAssessmentTask.Core.Interfaces;
using HahnAssessmentTask.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HahnAssessmentTask.Infrastructure.Repositories
{
  public class BaseRepository<T>(AppDbContext context) : IBaseRepository<T> where T : class
  {
    protected readonly AppDbContext _context = context;

    public void Create(T entity)
    {
      _context.Add(entity);
    }

    public void Delete(T entity)
    {
      _context.Remove(entity);
    }

    public void Update(T entity)
    {
      _context.Update(entity);
    }

    public IEnumerable<T> GetAll()
    {
      return _context.Set<T>().AsNoTracking();
    }
  }
}
