using System;
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

		public List<Answer> GetAnswers() {

			return _context.Answers.ToList();

		}

		public Answer? Get(Guid Id) {

			throw new NotImplementedException();

		}


		public async Task<Answer> AddAsync(Answer answer)
		{
			await _context.Answers.AddAsync(answer);
			await _context.SaveChangesAsync();
			return answer;
		}

		public Answer? Put(Answer answer) {
			throw new NotImplementedException();
		}
		public bool Delete(Answer answer) {
			throw new NotImplementedException();
		}

	}


