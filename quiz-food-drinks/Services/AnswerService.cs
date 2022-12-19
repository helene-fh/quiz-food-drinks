using System;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.ViewModels.Answer.cs;

namespace quiz_food_drinks.Services;

	public class AnswerService : IAnswerService
	{
		private readonly IAnswerRepository _answerRepository;

		public AnswerService(IAnswerRepository answerRepository)
		{
			_answerRepository = answerRepository;
		}


		public List<Answer> Get() {

			return _answerRepository.GetAnswers();

		}

		public Task<Answer> AddAnswer(AnswerCreateRequest answer) {

			var newAnswer = new Answer(answer.AnswerText,answer.IsCorrectAnswer);
			return _answerRepository.AddAsync(newAnswer);

		}

	}


