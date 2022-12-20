using System;
using quiz_food_drinks.Entities;
using quiz_food_drinks.ViewModels.Answer.cs;

namespace quiz_food_drinks.Interfaces.Repositories;

	public interface IAnswerRepository
	{
        public Task<List<Answer>> GetAnswers(Guid id);
        public Answer? Get(Guid Id);

        public Task<Answer> AddAsync(Answer answer);
        public Answer? Put(Answer answer);
        public Task<Answer> Delete(Guid id);
        public Task<Answer?> EditAnswer(AnswerEditRequest answer);
    
    }


