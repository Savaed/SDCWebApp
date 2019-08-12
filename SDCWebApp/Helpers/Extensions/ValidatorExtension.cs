﻿using FluentValidation;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace SDCWebApp.Data.Validators
{
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Indicates whether email address is in valid format.
        /// Valid email adresses acording https://en.wikipedia.org/wiki/Email_address#Examples
        /// </summary>
        public static IRuleBuilderOptions<T, string> BeEmailAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(emailAddress =>
            {
                string localPart = emailAddress.Split("@")[0];

                // Local part of email (part before @) cannot be longer than 64 characters.
                // MailAddress class doesnt check this condition.
                if (localPart.Length > 64)
                    return false;
                try
                {
                    var email = new MailAddress(emailAddress);
                }
                catch (FormatException)
                {
                    return false;
                }
                return true;
            });
        }

        public static IRuleBuilderOptions<T, string> BeValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(password => new Regex(@"^(?=.*[A - Z])(?=.*[a - z])(?=.*\d)(?=.*[!#$%&'()*+,\-.\/:;<=>?@[\\\]^_`{|}~])(?=.{8,})").IsMatch(password));
        }
    }
}
