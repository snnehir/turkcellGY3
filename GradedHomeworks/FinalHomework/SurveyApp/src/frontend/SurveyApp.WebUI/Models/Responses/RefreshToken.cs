﻿namespace SurveyApp.WebUI.Models.Responses
{
    public class RefreshToken
    {
        public string TokenString { get; set; }
        public DateTime ExpireAt { get; set; }
        public string Email { get; set; }
    }
}
