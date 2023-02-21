using quiz_food_drinks.Interfaces.Entitites;

namespace quiz_food_drinks.ViewModels.Answer.cs;

	public class AnswerResponse : AnswerBase
	{
		public AnswerResponse(IAnswer answer)
		{
			AnswerText = answer.AnswerText;
			IsCorrectAnswer = answer.IsCorrectAnswer;
		}

    public AnswerResponse(string answerText) : base(answerText)
    {
    }
}


