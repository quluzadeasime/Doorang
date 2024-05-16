﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryAbstracts
{
    public interface IGenericRepository<T> where T:BaseEntity,new()
    {
        void Add(T entity);
        void Delete(T entity);
        T GetCard(Func<T, bool> func = null);
        List<T> GetAllCard(Func<T, bool> func = null);
        int Commit();
    }
}
