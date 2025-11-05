using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Appointment_Manager.Controller.Utils
{
    public static class FormValidations
    {
        /// <summary>
        /// Validates a TextBox based on a specified type.
        /// </summary>
        /// <param name="textBox">The TextBox to validate.</param>
        /// <param name="type">The type of validation ("string", "phone", "int", etc.)</param>
        /// <param name="errorProvider">The ErrorProvider to set the error on.</param>
        /// <returns>True if valid, false otherwise.</returns>
        public static bool ValidateTextBox(TextBox textBox, string type, ErrorProvider errorProvider)
        {
            // Requirement A.2.a: Trim fields and check for non-empty
            bool valid = true;
            string text = textBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                errorProvider.SetError(textBox, "Field cannot be empty.");
                valid = false;
            }
            else if (!ValidateType(textBox, type,  errorProvider))
            {
                valid = false;
            }
            else
            {
                errorProvider.SetError(textBox, "");
            }

            textBox.BackColor = valid ? Color.White : Color.Salmon;
            return valid;
        }

        public static bool ValidateType(TextBox textBox, string type, ErrorProvider errorProvider)
        {
            bool valid = true;

            switch (type)
            {
                case "int":
                    if (!int.TryParse(textBox.Text, out int value))
                    {
                        errorProvider.SetError(textBox, "Invalid integer format.");
                        valid = false;
                    }
                    else
                    {
                        errorProvider.SetError(textBox, "");
                    }
                    break;

                case "decimal":
                    if (!decimal.TryParse(textBox.Text, out decimal decimalNumber))
                    {
                        errorProvider.SetError(textBox, "Invalid decimal format");
                        valid = false;
                    }
                    else
                    {
                        errorProvider.SetError(textBox, "");
                    }
                    break;

                case "string":
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        errorProvider.SetError(textBox, "This field cannot be empty");
                        valid = false;
                    }
                    else
                    {
                        errorProvider.SetError(textBox, "");
                    }
                    break;
                case "phone":
                    Regex phoneRegex = new Regex(@"^(\+?\d{1,2}\s?)?((\(\d{1,3}\))|\d{1,3})[-.\s]?\d{1,4}[-.\s]?\d{1,4}([-.\s]?\d{1,9})?$");
                    if (phoneRegex.IsMatch(textBox.Text))
                    {
                        errorProvider.SetError(textBox, "");
                    }
                    else
                    {
                        errorProvider.SetError(textBox, "Invalid phone number format");
                        valid = false;
                    }
                    break;
                default:
                    errorProvider.SetError(textBox, "Unknown type");
                    valid = false;
                    break;
            }
            textBox.BackColor = valid ? Color.White : Color.Salmon;
            return valid;
        }
    }

}
