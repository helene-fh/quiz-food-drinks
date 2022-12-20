using System;
using quiz_food_drinks.Entities;
using quiz_food_drinks.ViewModels.Answer.cs;

namespace quiz_food_drinks.Interfaces.Services;

	public interface IAnswerService
	{

		Task<Answer> AddAnswer(AnswerCreateRequest answer);
	}


