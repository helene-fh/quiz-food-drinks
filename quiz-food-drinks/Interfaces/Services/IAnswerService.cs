using quiz_food_drinks.Entities;
using quiz_food_drinks.ViewModels.Answer.cs;

namespace quiz_food_drinks.Interfaces.Services;

	public interface IAnswerService
	{
		Task<List<Answer>> AllAnswers();
		Task<List<Answer?>> Get(Guid id);
		Task<Answer?> AddAnswer(AnswerCreateRequest answer);
		Task<Answer?> DeleteAnswer(Guid id);
		Task<Answer?> EditAnswer(AnswerEditRequest answer);	
	}






