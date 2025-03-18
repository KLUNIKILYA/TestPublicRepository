﻿using Lab5TestTask.Data;
using Lab5TestTask.Enums;
using Lab5TestTask.Models;
using Lab5TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab5TestTask.Services.Implementations;

/// <summary>
/// SessionService implementation.
/// Implement methods here.
/// </summary>
public class SessionService : ISessionService
{
    private readonly ApplicationDbContext _dbContext;

    public SessionService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Session> GetSessionAsync()
    {
        return await _dbContext.Sessions
            .Where(x => x.EndedAtUTC > x.StartedAtUTC)
            .OrderBy(x => x.StartedAtUTC)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Session>> GetSessionsAsync()
    {
        return await _dbContext.Sessions
            .Include(x => x.User)
            .Where(x => x.User.Status == UserStatus.Active && x.EndedAtUTC < new DateTime(2025, 1, 1))
            .ToListAsync();
    }
}
