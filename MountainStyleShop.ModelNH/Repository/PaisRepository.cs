﻿using MountainStyleShop.ModelNH.Model;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MountainStyleShop.ModelNH.Repository
{
    public class PaisRepository : RepositoryBase<Pais>
    {
        public PaisRepository(ISession session) : base(session)
        {
        }
    }
}