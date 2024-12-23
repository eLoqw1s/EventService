﻿using EventService.Models;
using Microsoft.EntityFrameworkCore;
using EventService.Interfaces;
using EventService.Models.DTO.MemorableDates;
using EventService.Exceptions;

namespace EventService.Repositories
{
    public class MemorabeDateRepository : IMemorabeDateRepository
	{
		private readonly EventServicesDbContext _context;

		public MemorabeDateRepository(EventServicesDbContext context)
		{
			_context = context;
		}

		public async Task<List<GetMemDateVm>> GetMemDateByDate(DateTime selectedDate)
		{
			var memDateEntities = await _context.MemorableDates
				.Include(m => m.Author)
				.Where(m => m.EventDate.Year == selectedDate.Year &&
                    m.EventDate.Month == selectedDate.Month &&
                    m.EventDate.Day == selectedDate.Day)
				.Select(m => new GetMemDateVm(
					m.Id,
					m.EventDate,
					m.TextNotification,
					m.InputTime,
					m.Author.Name))
				.AsNoTracking()
				.ToListAsync();

			return memDateEntities;
		}

		public async Task<List<GetMemDateVm>> GetAllMemDate()
		{
			var memDateEntities = await _context.MemorableDates
				.Include(m => m.Author)
				.Select(m => new GetMemDateVm(
					m.Id,
					m.EventDate,
					m.TextNotification,
					m.InputTime,
					m.Author.Name))
				.AsNoTracking()
				.ToListAsync();

			return memDateEntities;
		}

		public async Task<MemorableDate> GetMemDateById(Guid Id)
		{
			var memDateEntity = await _context.MemorableDates
				.FirstOrDefaultAsync(memDate => memDate.Id == Id);

			if (memDateEntity == null)
			{
				throw new CustomException("Memorable date not found", 404);
			}

			return memDateEntity;
		}

		public async Task<Guid> UpdateMemDate(Guid Id, DateTime EventDate, string TextNotification)
		{
			var memDateEntity = await _context.MemorableDates.FirstOrDefaultAsync(
				memDate => memDate.Id == Id);

			if (memDateEntity == null)
			{
				throw new CustomException("Memorable date not found", 404);
			}

			memDateEntity.EventDate = EventDate;
			memDateEntity.TextNotification = TextNotification;
			memDateEntity.InputTime = DateTime.Now;

			await _context.SaveChangesAsync();

			return memDateEntity.Id;
		}


		public async Task<Guid> DeleteMemDate(Guid Id)
		{
			var memDateEntity = await _context.MemorableDates
				.Where(b => b.Id == Id)
				.ExecuteDeleteAsync();

			if (memDateEntity == 0)
			{
				throw new CustomException("Memorable date not found", 404);
			}

			return Id;
		}

		public async Task<Guid> CreateMemDate(DateTime EventDate, string TextNotification,
			Guid AuthorId)
		{
			var memDateEntity = new MemorableDate
			{
				Id = Guid.NewGuid(),
				EventDate = EventDate,
				TextNotification = TextNotification,
				InputTime = DateTime.Now,
				AuthorId = AuthorId
			};

			await _context.MemorableDates.AddAsync(memDateEntity);
			await _context.SaveChangesAsync();

			return memDateEntity.Id;
		}
	}
}
