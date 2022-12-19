using System;
using System.ComponentModel.DataAnnotations;
using quiz_food_drinks.Entities;

namespace quiz_food_drinks.Interfaces.Entitites
{
	public interface IAnswer
	{
		[Required]
		public string AnswerText { get; set; }

		[Required]
		public bool IsCorrectAnswer { get; set; }
	}
}

