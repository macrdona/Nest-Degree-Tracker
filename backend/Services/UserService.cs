﻿using Microsoft.Extensions.Options;
using backend.Authorization;
using backend.Entities;
using backend.Helpers;
using backend.Models;
using BCrypt.Net;
using AutoMapper;

namespace backend.Services
{

    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetById(int id);
        AuthenticateResponse Register(RegisterRequest model);
        void Update(int id, UpdateRequest model);
        void Delete(int id);
        EnrollmentResponse EnrollmentForm(EnrollmentFormRequest form);

        EnrollmentForm UserEnrollment(int id);
    }

    public class UserService : IUserService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // validate password
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            //user will be assigned a JWT token each time they log in
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public User GetById(int id)
        {
            return getUser(id);
        }

        public AuthenticateResponse Register(RegisterRequest model)
        {
            // validate username
            if (_context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            //copy current model to the user model whose data will be stored in the database
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save user
            _context.Users.Add(user);
            _context.SaveChanges();

            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);

            return response;
        }

        public void Update(int id, UpdateRequest model)
        {
            var user = getUser(id);

            // validate username
            if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
                throw new AppException("Username '" + model.Username + "' is already taken");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, user);
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = getUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        //helper method that returns users based on id
        private User getUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public EnrollmentResponse EnrollmentForm(EnrollmentFormRequest form)
        {
            var user = _context.Users.Find(form.UserId);

            if (user == null) throw new KeyNotFoundException();

            if ((bool)user.Completed) throw new AppException("User has already completed the form");
            user.Completed = true;

            var transfer_form = _mapper.Map<EnrollmentForm>(form);
            _context.Enrollments.Add(transfer_form);
            _context.SaveChanges();

            List<CompletedCourses> courses = new List<CompletedCourses>(); 
            var userId = form.UserId;
            foreach(String course in form.Courses)
            {
                courses.Add(new CompletedCourses(userId,course));
            }

            _context.CompletedCourses.AddRange(courses);
            _context.SaveChanges();

            return new EnrollmentResponse { Message = "Enrollment has been completed" };

        }

        public EnrollmentForm UserEnrollment(int userId)
        {
            var user = _context.Enrollments.Find(userId);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }
    }
}
