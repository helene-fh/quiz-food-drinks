using System;
using quiz_food_drinks.Interfaces.Entitites;

namespace quiz_food_drinks.ViewModels.Answer.cs;

	public class AnswerResponse : AnswerBase
	{
		public AnswerResponse(IAnswer answer)
		{
			base.AnswerText = answer.AnswerText;
			base.IsCorrectAnswer = answer.IsCorrectAnswer;
		}

    public AnswerResponse(string answerText) : base(answerText)
    {
    }
}


