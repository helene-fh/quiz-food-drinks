namespace quiz_food_drinks.ViewModels.Answer.cs
{
	public class AnswerUpdateRequest : AnswerBase
	{
		public Guid AnswerId { get; set; }

		public AnswerUpdateRequest(string answerText) : base(answerText)
		{
		}
	}
}

