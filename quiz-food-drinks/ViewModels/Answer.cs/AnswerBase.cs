using System;
using quiz_food_drinks.Interfaces.Entitites;

namespace quiz_food_drinks.ViewModels.Answer.cs;

	public abstract class AnswerBase : IAnswer
	{

		public Guid QuestionId { get; set; }
		public string AnswerText { get; set; } = null!;
		public bool IsCorrectAnswer { get; set; }

		public AnswerBase(string answerText)
		{
			AnswerText = answerText;
			
		}
		protected AnswerBase()
		{

		}
	}


