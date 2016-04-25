using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project.Models
{

    #region Модели
    [PropertiesMustMatch("NewPassword", "ConfirmPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Текущий пароль")]
        public string OldPassword { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("Новый пароль")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Подтверждение пароля")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [DisplayName("Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [DisplayName("Запомнить меня")]
        public bool RememberMe { get; set; }
    }

    [PropertiesMustMatch("Password", "ConfirmPassword", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
    public class RegisterModel
    {
        [Required]
        [DisplayName("Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Подтверждение пароля")]
        public string ConfirmPassword { get; set; }
    }
    #endregion

    #region Services
    // Тип FormsAuthentication запечатан и содержит статические члены, поэтому
    // вызов его членов затруднен из кода модульного теста. Следующий интерфейс и вспомогательный класс показывают,
    // как создавать абстрактную оболочку вокруг такого типа, чтобы сделать код AccountController
    // доступным для модульного тестирования.

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string email);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;

        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Значение не может быть равно NULL или быть пустым.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Значение не может быть неопределенным (null) или пустым.", "password");

            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string email)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Значение не может быть неопределенным (null) или пустым.", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("Значение не может быть неопределенным (null) или пустым.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Значение не может быть неопределенным (null) или пустым.", "email");

            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, email, null, null, true, null, out status);
            return status;
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Значение не может быть неопределенным (null) или пустым.", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("Значение не может быть неопределенным (null) или пустым.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("Значение не может быть неопределенным (null) или пустым.", "newPassword");

            // Следующий ChangePassword() запустит исключение
            // вместо возврата false в определенных сценариях ошибки.
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
    }

    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("Значение не может быть неопределенным (null) или пустым.", "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
    #endregion

    #region Validation
    public static class AccountValidation
    {
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // См. по ссылке http://go.microsoft.com/fwlink/?LinkID=177550
            // полный список кодов состояний.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Имя пользователя уже существует. Введите другое имя пользователя.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "Имя пользователя для данного адреса электронной почты уже существует. Введите другой адрес электронной почты.";

                case MembershipCreateStatus.InvalidPassword:
                    return "Предоставленный пароль недействителен. Введите действительный пароль.";

                case MembershipCreateStatus.InvalidEmail:
                    return "Недопустимый адрес электронной почты. Проверьте значение поля и повторите попытку.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "Предоставленный ответ извлечения пароля недействителен. Проверьте значение поля и повторите попытку.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "Предоставленный вопрос извлечения пароля недействителен. Проверьте значение поля и повторите попытку.";

                case MembershipCreateStatus.InvalidUserName:
                    return "Предоставленное имя пользователя недействительно. Проверьте значение поля и повторите попытку.";

                case MembershipCreateStatus.ProviderError:
                    return "Поставщик проверки подлинности вернул ошибку. Проверьте вашу запись и повторите попытку. Если неполадка продолжает возникать, обратитесь к системному администратору.";

                case MembershipCreateStatus.UserRejected:
                    return "Запрос на создание пользователя отменен. Проверьте вашу запись и повторите попытку. Если неполадка продолжает возникать, обратитесь к системному администратору.";

                default:
                    return "Неизвестная ошибка. Проверьте вашу запись и повторите попытку. Если неполадка продолжает возникать, обратитесь к системному администратору.";
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "Значения ''{0}'' и ''{1}'' не совпадают.";
        private readonly object _typeId = new object();

        public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
            : base(_defaultErrorMessage)
        {
            OriginalProperty = originalProperty;
            ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty { get; private set; }
        public string OriginalProperty { get; private set; }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                OriginalProperty, ConfirmProperty);
        }

        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
            object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
            return Object.Equals(originalValue, confirmValue);
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "Значение ''{0}'' должно содержать не менее {1} знаков.";
        private readonly int _minCharacters = Membership.Provider.MinRequiredPasswordLength;

        public ValidatePasswordLengthAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString,
                name, _minCharacters);
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            return (valueAsString != null && valueAsString.Length >= _minCharacters);
        }
    }
    #endregion

}
