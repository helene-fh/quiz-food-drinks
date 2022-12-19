﻿using System;
using quiz_food_drinks.Entities;

namespace quiz_food_drinks.Interfaces.Repositories;

	public interface IAnswerRepository
	{
        public List<Answer> GetAnswers();
        public Answer? Get(Guid Id);

        Task<Answer> AddAsync(Answer answer);
        public Answer? Put(Answer answer);
        public bool Delete(Answer answer);
    }


