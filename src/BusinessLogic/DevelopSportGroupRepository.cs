﻿using System;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class DevelopSportGroupRepository : ISportGroupRepository
    {
        public Task<SportGroup[]> ReadAllAsync()
        {
            return Task.FromResult(Array.Empty<SportGroup>());
        }
    }
}