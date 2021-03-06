﻿using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CLAP.Validation
{
    /// <summary>
    /// Regex validation
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class RegexMatchesAttribute : ValidationAttribute
    {
        public string Pattern { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pattern"></param>
        public RegexMatchesAttribute(string pattern)
        {
            Pattern = pattern;
        }

        public override IParameterValidator GetValidator()
        {
            return new RegexMatchesValidator(Pattern);
        }

        public override string Description
        {
            get
            {
                return "Matches regex: '{0}'".FormatWith(Pattern);
            }
        }

        /// <summary>
        /// Regex validator
        /// </summary>
        private class RegexMatchesValidator : IParameterValidator
        {
            /// <summary>
            /// The regex pattern
            /// </summary>
            public string Pattern { get; private set; }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="pattern"></param>
            public RegexMatchesValidator(string pattern)
            {
                Pattern = pattern;
            }

            /// <summary>
            /// Validate
            /// </summary>
            public void Validate(ParameterInfo parameter, object value)
            {
                if (!Regex.IsMatch(value.ToString(), Pattern))
                {
                    throw new ValidationException("{0}: '{1}' does not match regex '{2}'".FormatWith(
                        parameter.Name,
                        value,
                        Pattern));
                }
            }
        }
    }
}