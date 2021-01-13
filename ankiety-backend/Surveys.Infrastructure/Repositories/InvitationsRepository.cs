﻿using System;
using System.Linq;
using Surveys.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Surveys.Infrastructure.Contexts;
using Surveys.Infrastructure.Repositories.Interfaces;


namespace Surveys.Infrastructure.Repositories
{
    public class InvitationsRepository : RepositoryBase<Invitation>, IInvitationsRepository
    {
        private readonly SurveysContext _context;

        public InvitationsRepository(SurveysContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invitation>> GetInvitationWithUsers()
            => await _context.Invitations.Include(x => x.User).ToListAsync();

        public async Task<IEnumerable<Invitation>> GetUserInvitations(Guid userId)
            => await _context.Invitations
                .Where(x => x.UserId == userId.ToString() && x.FilledDate == null 
                    && x.ExpirationDate == null || x.ExpirationDate < DateTime.Now)
                .ToListAsync();
    }
}