﻿namespace Resume.Bussines.Services.Interfaces;

public interface IEmailService
{
    public Task<bool> SendEmail(string to, string subject, string body);
}