﻿using Microsoft.EntityFrameworkCore;
using Surveys.Core.Entities;
using Surveys.Infrastructure.Contexts;
using Surveys.Infrastructure.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Repositories
{
    public class SurveysRepository : RepositoryBase<Survey>, ISurveysRepository
    {
        private readonly SurveysContext _context;
        public SurveysRepository(SurveysContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Survey> GetByIdWithQuestionsAndAnwerOptionsAsync(Guid id)
            => await _context.Set<Survey>()
            .Include(x => x.Questions)
                .ThenInclude(x => x.Options)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}