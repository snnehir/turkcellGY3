﻿using FluentValidation;

namespace SurveyApp.DataTransferObjects.Requests
{
    public class UserLoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        
    }
}