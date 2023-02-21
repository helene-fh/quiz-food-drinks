using Microsoft.EntityFrameworkCore;
using quiz_food_drinks.Entities;
using quiz_food_drinks.Interfaces.Repositories;
using quiz_food_drinks.Persistance;
using quiz_food_drinks.ViewModels.Answer.cs;

namespace quiz_food_drinks.Contexts.Entitites.Repositories;

	internal class AnswerRepository : IAnswerRepository
	{
		private readonly QuizDatabaseContext _context;

		public AnswerRepository(QuizDatabaseContext context)
		{
			_context = context;
		}

		public async Task<List<Answer>> GetAnswersAsync()
		{
			return await _context.Answers.ToListAsync();
		}

		public async Task<List<Answer>> GetAnswers(Guid id)
		{
			var answerData = _context.Answers.Where(y => y.QuestionId==id).ToList();
			return await Task.FromResult(answerData);
		}
		
		public async Task<Answer?> AddAsync(Answer answer)
		{
			if (answer is null)
			{
				return null;
			}
			else 
			{
				await _context.Answers.AddAsync(answer);
				await _context.SaveChangesAsync();
			}

			return answer;
		}

		public async Task<Answer?> Delete(Guid id)
		{	
			var answerToDelete = _context.Answers.Where(y => y.Id == id).FirstOrDefault();

			if (answerToDelete != null) {
				
				_context.Answers.Remove(answerToDelete);
				await _context.SaveChangesAsync();			
			}
			
			return answerToDelete;
		}


		public async Task<Answer?> EditAnswer(AnswerEditRequest answer) {

			var answerToEdit = _context.Answers.Where(x => x.Id == answer.AnswerId).FirstOrDefault();
		
			if (answerToEdit!=null)
			{
				answerToEdit.AnswerText = answer.AnswerText;
				answerToEdit.IsCorrectAnswer = answer.IsCorrectAnswer;
				_context.Update(answerToEdit);
				await _context.SaveChangesAsync();
			}
			
			return answerToEdit;
		
		}
	
	}


