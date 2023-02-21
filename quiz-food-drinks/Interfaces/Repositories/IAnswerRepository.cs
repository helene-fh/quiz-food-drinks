using quiz_food_drinks.Entities;
using quiz_food_drinks.ViewModels.Answer.cs;

namespace quiz_food_drinks.Interfaces.Repositories;

	public interface IAnswerRepository
	{
		Task<List<Answer>> GetAnswersAsync();
        Task<List<Answer>> GetAnswers(Guid id);
        Task<Answer?> AddAsync(Answer answer);
        Task<Answer?> Delete(Guid id);
        Task<Answer?> EditAnswer(AnswerUpdateRequest answer);
	}





