using System;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Persistance;
using quiz_food_drinks.ViewModels.Answer.cs;
using quiz_food_drinks.ViewModels.Question.cs;

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

		public async Task<Answer> AddAnswer(AnswerCreateRequest? answer)
		{
			using (var context = new QuizDatabaseContext())
			{
				var questionAnswer = context.Questions.Where(q => q.Id == answer.QuestionId).FirstOrDefault();

				var newAnswer = new Answer()
					{
						Id = Guid.NewGuid(),
						QuestionId = questionAnswer.Id,
						AnswerText = answer.AnswerText,
						IsCorrectAnswer = answer.IsCorrectAnswer
					};
					return await _answerRepository.AddAsync(newAnswer);
				
			}

		}

}


