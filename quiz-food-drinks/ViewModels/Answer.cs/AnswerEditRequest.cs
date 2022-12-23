using System;
namespace quiz_food_drinks.ViewModels.Answer.cs
{
	public class AnswerEditRequest : AnswerBase
	{
		public Guid AnswerId { get; set; }

		public AnswerEditRequest(string answerText) : base(answerText)
		{
		}
	}
}

