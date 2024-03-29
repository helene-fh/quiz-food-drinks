﻿using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Interfaces.Services;
using quiz_food_drinks.Persistance;
using quiz_food_drinks.ViewModels.Answer.cs;

namespace quiz_food_drinks.Services;

	public class AnswerService : IAnswerService
	{
		private readonly IAnswerRepository _answerRepository;

		public AnswerService(IAnswerRepository answerRepository)
		{
			_answerRepository = answerRepository;
		}

		public async Task<List<Answer>> AllAnswers()
		{
			return await _answerRepository.GetAnswersAsync();
		}

		public async Task<List<Answer>> Get(Guid id)
		{
			return await _answerRepository.GetAnswers(id);
		}

		public async Task<Answer?> AddAnswer(AnswerCreateRequest answer)
		{
			using (var context = new QuizDatabaseContext())
			{
				var questionAnswer = context.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);

				if (questionAnswer != null)
				{
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
			return null;
		}

		public async Task<Answer?> DeleteAnswer(Guid id)
		{
			return await _answerRepository.Delete(id);
		}

		public async Task<Answer?> EditAnswer(AnswerUpdateRequest answer)
		{
			return await _answerRepository.EditAnswer(answer);
		}

	}


