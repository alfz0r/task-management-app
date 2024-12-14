﻿using Microsoft.EntityFrameworkCore;
using TaskManagement.Primitives;
using TaskManagement.Primitives.Interfaces.Repositories;

namespace TaskManagement.Data.Repositories;

public class UserRepository : IUserRepository
{
	private readonly ApplicationDbContext _context;

	public UserRepository(ApplicationDbContext context) {
		_context = context;
	}

	public async Task<User> GetUserByUsernameAsync(string username) {
		return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
	}

	public async Task AddUserAsync(User user) {
		_context.Users.Add(user);
		await _context.SaveChangesAsync();
	}

	public async Task<User> GetUserByIdAsync(int id) {
		return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
	}

	public async Task UpdateUserAsync(User user) {
		_context.Users.Update(user);
		await _context.SaveChangesAsync();
	}

	public async Task<IEnumerable<User>> GetUsersAsync() {
		return await _context.Users.ToListAsync();
	}
}