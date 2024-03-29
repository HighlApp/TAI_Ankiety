﻿using System;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Surveys.Infrastructure.Contexts;
using Surveys.Infrastructure.Repositories.Interfaces;

namespace Surveys.Infrastructure.Repositories
{
    public class QuestionsRepository : RepositoryBase<Question>, IQuestionsRepository
    {
        private readonly SurveysContext _context;

        public QuestionsRepository(SurveysContext context) : base(context)
        {
            _context = context;
        }

        public Task<Question> GetByIdWithOptions(Guid id) 
            => _context.Set<Question>()
                .Include(x => x.Options)
                .FirstOrDefaultAsync(x => x.Id == id);
    }
}
