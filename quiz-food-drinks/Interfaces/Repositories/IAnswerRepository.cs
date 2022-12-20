using System;
using quiz_food_drinks.Entities;

namespace quiz_food_drinks.Interfaces.Repositories;

	public interface IAnswerRepository
	{
        public List<Answer> GetAnswers();
        public Answer? GetAnswer(Guid Id);

        Task<Answer> AddAsync(Answer answer);
        public Answer? Put(Answer answer);
        public async void Delete(Guid id);
    }


