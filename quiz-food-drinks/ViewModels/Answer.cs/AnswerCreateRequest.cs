using System;
namespace quiz_food_drinks.ViewModels.Answer.cs;

	public class AnswerCreateRequest : AnswerBase
	{
		public AnswerCreateRequest()
		{
		}

		public AnswerCreateRequest(string answerText) : base(answerText)
		{
		}
		
	}


