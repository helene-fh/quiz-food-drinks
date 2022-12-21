using System;
using Microsoft.EntityFrameworkCore;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Persistance;

namespace quiz_food_drinks.Contexts.Entitites.Repositories;

	internal class AnswerRepository : IAnswerRepository
	{
		private readonly QuizDatabaseContext _context;

		public AnswerRepository(QuizDatabaseContext context)
		{
			_context = context;


		}


		public async Task<List<Answer>> GetAnswersAsync()
		{
			return await _context.Answers.ToListAsync();
		}

		public async Task<List<Answer?>> GetAnswers(Guid id) {

			var answerData = _context.Answers
									.Where(y => y.QuestionId==id)
									.ToList();

			return answerData;
		}

		public Answer? Get(Guid Id) {

			throw new NotImplementedException();

		}


		public async Task<Answer?> AddAsync(Answer? answer)
		{
			if (answer != null)
			{
				await _context.Answers.AddAsync(answer);
				await _context.SaveChangesAsync();
			}
			return answer;
		}

		public Answer? Put(Answer answer) {
			throw new NotImplementedException();
		}
		public bool Delete(Answer answer) {
			throw new NotImplementedException();
		}

	}


